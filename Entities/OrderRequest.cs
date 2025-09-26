public class OrderRequest
{
    public int UserId { get; set; }
    public int AddressId { get; set; }
    public int TotalAmount { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }
}
