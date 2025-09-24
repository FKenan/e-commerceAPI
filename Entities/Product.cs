namespace Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();

    }
}
