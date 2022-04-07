using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services.Contracts;
using Shopping.Aggregator.Services.Implementation;
using System.Net;

namespace Shopping.Aggregator.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        private readonly ICatalogService catalogService;
        private readonly IBasketService basketService;
        private readonly IOrderService orderService;

        public ShoppingController(ICatalogService catalogService, IBasketService basketService, IOrderService orderService)
        {
            this.catalogService = catalogService;
            this.basketService = basketService;
            this.orderService = orderService;
        }

        [HttpGet("{userName}", Name = "GetShopping")]
        [ProducesResponseType(typeof(ShoppingModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingModel>> GetShopping(string userName)
        {
            //getBasket with username (basket.API) which internally call Disocunt.grpc
            //iterate basket item for each prodcut & get the product details (catalog.API)
            //map product related numbers into dto BasketItemExtendedModel
            //consume ordering microservice to retrieve the order list (Ordering.API)
            // return new shopping model

            //getBasket with username (basket.API) which internally call Disocunt.grpc
            var basket = await this.basketService.GetBasket(userName);

            //iterate basket item for each prodcut & get the product details (catalog.API)
            foreach (var item in basket.Items)
            {
                var product = await this.catalogService.GetCatalog(item.ProductId);
                //map product related numbers into dto BasketItemExtendedModel 
                item.ProductName = product.Name;
                item.Category = product.Category;
                item.Summary = product.Summary;
                item.ImageFile = product.ImageFile;
                item.Description = product.Description;
            }
            //consume ordering microservice to retrieve the order list (Ordering.API)
            IEnumerable<OrderResponseModel> orders = await this.orderService.GetOrdersByUserName(userName);

            ShoppingModel shoppingModel = new ShoppingModel
            {
                UserName = userName,
                BasketWithProducts = basket,
                Orders = orders
            };
            // return new shopping model
            return Ok(shoppingModel);
        }
    }
}
