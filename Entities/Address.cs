public class Address
{
    public int Id { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string FullAddress { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }
}
