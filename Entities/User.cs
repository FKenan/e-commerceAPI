public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? PhoneNumber { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
    public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}
