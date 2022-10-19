using OrderShopWeb.Models;

namespace OrderShopWeb.Services;

/// <summary>
/// Serwis służący do przekazania danych o zamówieniu z strony podsumowania na stronę potwierdzającą.
/// Dodatkowo serwis wysyła zamówienie w postaci JSON do zewnętrznego API.
/// </summary>
public class OrderService
{
    /// <summary>
    /// Obiekt zamówienia.
    /// </summary>
    public Order? Order { get; set; }

    /// <summary>
    /// Opcje serializacji JSON.
    /// </summary>
    JsonSerializerOptions options;

    public OrderService(JsonSerializerOptionsService _jsonOptionsService)
    {
        options = _jsonOptionsService.Options;
    }

    /// <summary>
    /// Wysłanie zamówienia.
    /// </summary>
    /// <param name="order">Obiekt zamówienia.</param>
    /// <returns>True, jeżeli zwrotka jest 200, false jak inny.</returns>
    public async Task<bool> SendOrder(Order order)
    {
        bool orderReceived = false;
        if (order == null)
        {
            return orderReceived;
        }
        HttpClient client = new();
        string json = JsonSerializer.Serialize(order, options);
        Console.WriteLine(json);
        StringContent content = new(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync($"{Statics.protocol}://{Statics.address}:{Statics.port}/orders/place", content);
        Console.WriteLine(response.StatusCode);
        if (response.IsSuccessStatusCode)
        {
            orderReceived = true;
        }
        return orderReceived;
    }

}
