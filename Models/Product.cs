using System.Text.Json.Serialization;

namespace OrderShopWeb.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public string ShortDescription { get; set; }
    public int Qty { get; set; } = 0;

    [JsonIgnore]
    public int TempQty { get; set; } = 1;

    [JsonIgnore]
    public decimal Sum { get; set; }
}
