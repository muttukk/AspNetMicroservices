using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository discountRepository;
        public DiscountController(IDiscountRepository _discountRepository)
        {
            discountRepository = _discountRepository;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public ActionResult<Coupon> GetDiscount()
        {
            string coupon = discountRepository.HelloWorld();
            return Ok(coupon);
        }

        //[HttpGet("{productName},name=GetDiscount")]
        //[ProducesResponseType(typeof(Coupon),(int) HttpStatusCode.OK)]
        //public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        //{ 
        //    Coupon coupon=await discountRepository.GetDiscount(productName);            
        //    return Ok(coupon);
        //}

        //[HttpPost]
        //[ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
        //{
        //    return Ok(await discountRepository.CreateDiscount(coupon));
        //}

        //[HttpPut]
        //[ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<Coupon>> UpdateDiscount(Coupon coupon)
        //{
        //    return Ok(await discountRepository.CreateDiscount(coupon));
        //}

        //[HttpDelete("{productName}", Name = "DeleteDiscount")]
        //[ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<bool>> DeleteDiscount(string productName)
        //{
        //    return Ok(await discountRepository.DeleteDiscount(productName));
        //}
    }
}
