using ECommerce.Application.Dtos.Orders;
using ECommerce.Domain.Entities.Order;
using ECommerce.Domain.Enums;
using Mapster;

namespace ECommerce.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(IUnitOfWorkTransactionHandler handler, ICartService cartService, ICurrentUserService currentUser) : IRequestHandler<CreateOrderCommand, Result<CreateOrderResponseDto>>
{
    public async Task<Result<CreateOrderResponseDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        => await handler.ExecuteAsync(async (unitOfWork, token) =>
        {
            if (currentUser.UserId is null)
                return new Error("Auth.Unauthorized", "کاربر احراز هویت نشده است.", ErrorType.Unauthorized);

            var userId = currentUser.UserId.Value;
            var cart = await cartService.GetCartAsync(userId);
            if (cart.Items.Count == 0)
                return new Error("Cart.Empty", "سبد خرید خالی است.", ErrorType.Validation);

            var productIds = cart.Items.Select(i => i.ProductId).ToList();
            var products = await unitOfWork.ProductRepository.GetAllWithTrackingAsync(
                expression: p => productIds.Contains(p.Id),
                cancellationToken: token
            );
            foreach (var cartItem in cart.Items)
            {
                var product = products.FirstOrDefault(p => p.Id == cartItem.ProductId);
                if (product is null)
                    return new Error("Product.NotFound", "محصول مورد نطر یافت نشد", ErrorType.NotFound);

                if (product.StockQuantity < cartItem.Quantity)
                    return new Error("Product.NotAvailable", "محصول به تعداد درخواستی شما موجود نیست.", ErrorType.Validation);
            }

            // TODO: Create order
            decimal subTotal = cart.Items.Sum(i => i.UnitPrice * i.Quantity);
            string orderNumber = $"ORD-{DateTime.UtcNow:yyyyMMdd-HHmmss}";

            var order = new Order
            {
                OrderNumber = orderNumber,
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                SubTotal = subTotal,
                ShippingCost = 0
            };
            foreach (var cartItem in cart.Items)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = cartItem.ProductId,
                    ProductName = cartItem.ProductName,
                    UnitPrice = cartItem.UnitPrice,
                    Quantity = cartItem.Quantity,
                    TotalPrice = cartItem.TotalPrice
                };
                order.Items.Add(orderItem);
            }

            var shipping = request.Adapt<OrderShipping>();

            var history = new OrderHistory
            {
                OrderId = order.Id,
                Status = OrderStatus.Pending,
                Note = "سفارش ثبت شد.",
                ChangedAt = DateTime.UtcNow
            };
            order.Histories.Add(history);

            // In Toman
            const decimal freeShippingThreshold = 1000000m;
            const decimal defaultShippingCost = 150000m;

            if (!string.IsNullOrEmpty(request.CouponCode))
            {
                var coupon = await unitOfWork.CouponRepository.GetAsync(
                    expression: c => c.Code == request.CouponCode,
                    cancellationToken: token
                );
                if (coupon is null)
                    return new Error("Coupon.NotFound", "کد تخفیف یافت نشد.", ErrorType.NotFound);

                if (!coupon.IsActive)
                    return new Error("Coupon.Inactive", "کد تخفیف غیرفعال است.", ErrorType.Validation);

                if (coupon.EndDate < DateTime.UtcNow)
                    return new Error("Coupon.Expired", "کد تخفیف منقضی شده است.", ErrorType.Validation);

                if (coupon.UsageLimit.HasValue && coupon.UsedCount >= coupon.UsageLimit.Value)
                    return new Error("Coupon.UsageLimit", "کد تخفیف به حد مصرف رسیده است.", ErrorType.Validation);

                if (coupon.MinOrderAmount.HasValue && subTotal < coupon.MinOrderAmount.Value)
                    return new Error("Coupon.MinOrder", $"حداقل سفارش برای این کد {coupon.MinOrderAmount.Value} تومان است.", ErrorType.Validation);

                decimal discount = coupon.Type == CouponType.Percentage
                    ? subTotal * (coupon.Value / 100)
                    : coupon.Value;

                order.DiscountAmount = discount;
                order.Coupon = coupon;
                coupon.UsedCount++;
                unitOfWork.CouponRepository.Update(coupon);
            }

            order.ShippingCost = subTotal >= freeShippingThreshold ? 0 : defaultShippingCost;
            order.TotalAmount = subTotal - order.DiscountAmount + order.ShippingCost;

            foreach (var cartItem in cart.Items)
            {
                var product = products.First(i => i.Id == cartItem.ProductId);
                product.StockQuantity -= cartItem.Quantity;
                unitOfWork.ProductRepository.Update(product);
            }

            await unitOfWork.OrderRepository.AddAsync(order, token);
            await unitOfWork.SaveAsync(token);

            shipping.OrderId = order.Id;
            await unitOfWork.OrderShippingRepository.AddAsync(shipping, token);
            await cartService.ClearCart(userId);
            var result = new CreateOrderResponseDto(order.Id, order.OrderNumber, order.TotalAmount, order.OrderDate);

            var saveResult = await unitOfWork.SaveAsync(token);
            return saveResult.IsFailure ? 
                new Error("Order.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected) : 
                Result<CreateOrderResponseDto>.Success(result);
        }, cancellationToken);
}