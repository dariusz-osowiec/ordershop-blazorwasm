using OrderShopWeb.Models;

namespace OrderShopWeb.Services;

/// <summary>
/// Obsługa formularza kontaktowego.
/// </summary>
public class SendMailContentService
{
    /// <summary>
    /// Opcje serializacji JSON.
    /// </summary>
    readonly JsonSerializerOptions options;

    public SendMailContentService(JsonSerializerOptionsService _jsonOptions)
    {
        options = _jsonOptions.Options;
    }

    /// <summary>
    /// Wysłanie zawartości maila do API.
    /// </summary>
    public async void SendMailContent(Question question)
    {
        HttpClient client = new();
        string json = JsonSerializer.Serialize(question, options);
        Console.WriteLine(json);
        StringContent content = new(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync($"{Statics.protocol}://{Statics.address}:{Statics.port}/questions/ask", content);
        Console.WriteLine(response.StatusCode);
    }
}
