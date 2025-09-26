public class OrderDto
{
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }
    public Address? Address { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }
}
