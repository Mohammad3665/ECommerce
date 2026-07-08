namespace ECommerce.Application.Dtos.Orders;

public record OrderShippingDto(
    string FullName,
    string PhoneNumber,
    string Address,
    string PostalCode
);