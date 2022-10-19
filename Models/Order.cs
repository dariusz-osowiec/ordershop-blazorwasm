namespace OrderShopWeb.Models;

public class Order
{
    public Customer? Customer { get; set; }
    public IEnumerable<Item>? Items { get; set; }
    public string? Note { get; set; }
}
