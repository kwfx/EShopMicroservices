using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService(DiscountDbContext discountDbContext) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var newCoupon = request.Coupon.Adapt<Coupon>();
        discountDbContext.Coupons.Add(newCoupon);
        await discountDbContext.SaveChangesAsync();
        return newCoupon.Adapt<CouponModel>();
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await discountDbContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
        if (coupon == null) return new DeleteDiscountResponse() { Success = false };
        discountDbContext.Coupons.Remove(coupon);
        await discountDbContext.SaveChangesAsync();
        return new DeleteDiscountResponse() { Success = true };
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        string productName = request.ProductName;
        var coupon = await discountDbContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == productName)
            ?? throw new RpcException(new Status(StatusCode.InvalidArgument, $"No coupon found for product {productName}"));
        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var dbCoupon = await discountDbContext.Coupons.FirstOrDefaultAsync(c => c.Id == request.Coupon.Id)
            ?? throw new RpcException(new Status(StatusCode.InvalidArgument, $"No coupon with ID {request.Coupon.Id}"));
        request.Coupon.Adapt(dbCoupon);
        discountDbContext.Update(dbCoupon);
        await discountDbContext.SaveChangesAsync();
        return dbCoupon.Adapt<CouponModel>();
    }
}