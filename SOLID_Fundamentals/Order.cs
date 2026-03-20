namespace SOLID_Fundamentals;

public class Order
{
    public int Id { get; set; }
    public decimal TotalAmount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public List<string> Items { get; set; } = new();
    public string CustomerEmail { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
}
