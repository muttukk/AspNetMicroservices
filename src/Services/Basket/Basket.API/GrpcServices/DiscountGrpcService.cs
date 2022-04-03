using Discount.Grpc.Protos;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient discountProtoService;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient _discountProtoService)
        {
            this.discountProtoService = _discountProtoService;
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            GetDiscountRequest discountRequest = new GetDiscountRequest { ProductName = productName };
            return await discountProtoService.GetDiscountAsync(discountRequest);
        }
    }
}
