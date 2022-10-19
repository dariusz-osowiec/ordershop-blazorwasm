namespace OrderShopWeb.Services;

/// <summary>
/// Usługa zwracająca ustawienia do serializacji JSONów.
/// </summary>
public class JsonSerializerOptionsService
{
    /// <summary>
    /// Opcje serializacji JSON.
    /// </summary>
    public JsonSerializerOptions Options { get; set; }

    /// <summary>
    /// Konstruktor.
    /// </summary>
    public JsonSerializerOptionsService()
    {
        Options = new JsonSerializerOptions()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
        };
    }
}
