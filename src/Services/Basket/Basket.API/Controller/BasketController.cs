using AutoMapper;
using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Discount.Grpc.Protos;
using EventBus.Message.Events;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository basketRepository;
        private readonly DiscountGrpcService discountGrpcService;
        private readonly IPublishEndpoint publishEndpoint;
        private readonly IMapper mapper;

        public BasketController(IBasketRepository _basketRepository, DiscountGrpcService _discountGrpcService,
                                IPublishEndpoint _publishEndpoint,IMapper _mapper)
        {
            basketRepository = _basketRepository;
            discountGrpcService = _discountGrpcService;
            publishEndpoint = _publishEndpoint;
            mapper = _mapper;
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            ShoppingCart? shoppingCart = await basketRepository.GetBasket(userName);
            return Ok(shoppingCart ?? new ShoppingCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart shoppingCart)
        {
            // Communicate with Discount.Grpc and calcualte the latest price of the product into the shopping cart 
            foreach (var item in shoppingCart.Items)
            {
                CouponModel coupon = await discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }
            return Ok(await basketRepository.UpdateBasket(shoppingCart));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteBasket(string userName)
        {
            await basketRepository.DeleteBasket(userName);
            return Ok();
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<BasketCheckout>> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            // get existing basket with total price
            // Create basketCheckoutEvent -- Set TotalPrice on basketCheckout eventMessage
            // send checkout event to rabbitmq
            // remove the basket

            // get existing basket with total price
            ShoppingCart basket = await basketRepository.GetBasket(basketCheckout.UserName);
            if (basket==null)
            {
                return BadRequest();
            }
            // Create basketCheckoutEvent -- Set TotalPrice on basketCheckout eventMessage
            BasketCheckoutEvent eventMessage = mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.TotalPrice = basket.TotalPrice;
            // send checkout event to rabbitmq
            publishEndpoint.Publish(eventMessage);
            // remove the basket
            await basketRepository.DeleteBasket(basketCheckout.UserName);
            return Accepted();
        }
    }
}
