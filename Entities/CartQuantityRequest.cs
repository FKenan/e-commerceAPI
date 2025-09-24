public class CartQuantityRequest
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int Amount { get; set; } = 1;
}
