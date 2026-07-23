using ECommerce.Domain.Enums;

namespace ECommerce.AdminPanel.Models;

public class OrderListViewModel
{
    public long Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }
    public int ItemCount { get; set; }
}

public class OrderDetailViewModel
{
    public long OrderId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public OrderStatus Status { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal SubTotal { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItemViewModel> Items { get; set; } = [];
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public string CustomerAddress { get; set; } = string.Empty;
    public string CustomerPostalCode { get; set; } = string.Empty;
}

public class OrderItemViewModel
{
    public string ProductName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}

public class OrderChangeStatusViewModel
{
    public long Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public OrderStatus CurrentStatus { get; set; }
    public OrderStatus NewStatus { get; set; }
}
