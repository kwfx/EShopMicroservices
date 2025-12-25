using Discount.Grpc.Data;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService(DiscountDbContext discountDbContext) : DiscountProtoService.DiscountProtoServiceBase
{
    public override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        return base.CreateDiscount(request, context);
    }

    public override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        return base.DeleteDiscount(request, context);
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        string productName = request.ProductName;
        var coupon = await discountDbContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == productName)
            ?? throw new Exception($"No coupon found for product {productName}");
        return new CouponModel
        {
            Id = coupon.Id,
            ProductName = coupon.ProductName,
            Description = coupon.Description,
            Amount = coupon.Amount,
        };
    }

    public override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        return base.UpdateDiscount(request, context);
    }
}