public class CartItemDto
{
    // Frontend'in ihtiyacına göre buraya başka ürün detayları (ImageUrl, CategoryName vb.) eklenebilir.
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}