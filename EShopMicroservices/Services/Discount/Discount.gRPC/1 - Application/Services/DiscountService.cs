using Discount.gRPC._2___Domain.Models;
using Discount.gRPC._3___Infra;
using Discount.Grpc;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC._1___Application.Services
{
    public class DiscountService(DiscountContext discountContext, 
        ILogger<DiscountService> logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await discountContext.Coupons
                .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon == null)
            {
                new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount" };
            }

            logger.LogInformation("Discount is retireved for ProductName: {productName}, " +
                "Amount: {amount}",coupon.ProductName,coupon.Amount);

            var couponModel = coupon.Adapt<CouponModel>();

            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();

            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
            }

            var existingCoupon = await discountContext.Coupons
               .FirstOrDefaultAsync(x => x.ProductName == request.Coupon.ProductName);

            if (existingCoupon == null)
            {
                discountContext.Coupons.Add(coupon);
                await discountContext.SaveChangesAsync();

                logger.LogInformation("Discount is created for ProductName: {productName}", coupon.ProductName);
            }

            var couponModel = coupon.Adapt<CouponModel>();  
            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();

            if (coupon is null || request.Coupon.Id == 0)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
            }

            var existingCoupon = await discountContext.Coupons
               .FirstOrDefaultAsync(x => x.Id == request.Coupon.Id);

            if (existingCoupon != null)
            {
                discountContext.ChangeTracker.Clear();
                discountContext.Update(coupon);
                await discountContext.SaveChangesAsync();

                logger.LogInformation("Discount is updated for ProductName: {productName}", coupon.ProductName);
            }

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {

            var coupon = await discountContext.Coupons
               .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"Discount with ProductName={request.ProductName} is not found."));
            }

            discountContext.ChangeTracker.Clear();
            discountContext.Remove(coupon);
            await discountContext.SaveChangesAsync();

            logger.LogInformation("Discount is deleted for ProductName: {productName}", coupon.ProductName);
                        
            return new DeleteDiscountResponse {  Success = true };
        }
    }
}
