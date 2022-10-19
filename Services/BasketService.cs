using Blazored.LocalStorage;
using OrderShopWeb.Interfaces;
using OrderShopWeb.Models;

namespace OrderShopWeb.Services;

/// <summary>
/// Obsługa koszyka.
/// </summary>
public class BasketService : IBasketOperable
{

    private readonly JsonSerializerOptions options;

    private readonly ILocalStorageService localStorage;

    private readonly IDbOperable db;

    /// <summary>
    /// Konstruktor.
    /// </summary>
    /// <param name="_localStorage">Instancja interfejsu obsługi LocalStorage.</param>
    /// <param name="_jsonOptions">Opcje serializacji JSON</param>
    /// <param name="_db">Instancja interfejsu komunikacji z bazą danych</param>
    public BasketService(ILocalStorageService _localStorage, JsonSerializerOptionsService _jsonOptions, IDbOperable _db)
    {
        localStorage = _localStorage;
        options = _jsonOptions.Options;
        db = _db;
    }

    /// <summary>
    /// Stworzenie pustego koszyka.
    /// </summary>
    /// <returns>Pustą listę zawartości koszyka.</returns>
    public async Task<List<Product>> CreateBasket()
    {
        try
        {
            await localStorage.SetItemAsStringAsync("basket", "[]");
            return new();
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.StackTrace);
            throw;
        }
    }

    /// <summary>
    /// Zapisanie zawartości koszyka. Podana lista jest przekształcana 
    /// </summary>
    /// <param name="basket"></param>
    public async Task SaveBasket(List<Product> basket)
    {
        try
        {
            IEnumerable<Item> items = from p in basket
                                      select new Item()
                                      {
                                          Id = p.Id,
                                          Qty = p.Qty,
                                      };
            string json = JsonSerializer.Serialize(items, options);
            await localStorage.SetItemAsStringAsync("basket", json);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.StackTrace);
            throw;
        }
    }

    /// <summary>
    /// Odczytanie koszyka.
    /// </summary>
    /// <returns>Zawartość koszyka jako lista produktów.</returns>
    public async Task<List<Product>?> ReadBasket()
    {
        try
        {
            string json = await localStorage.GetItemAsync<string>("basket");
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            List<Product> products = await db.ReadAll();
            List<Item> items = JsonSerializer.Deserialize<List<Item>>(json, options);
            IEnumerable<Product> basket = from i in items
                                          join p in products on i.Id equals p.Id into idp
                                          from pidp in idp.DefaultIfEmpty()
                                          select new Product()
                                          {
                                              Id = i.Id,
                                              Name = pidp.Name,
                                              CategoryId = pidp.CategoryId,
                                              ImageUrl = pidp.ImageUrl,
                                              Description = pidp.Description,
                                              ShortDescription = pidp.ShortDescription,
                                              Price = pidp.Price,
                                              Qty = i.Qty,
                                              Sum = i.Qty * pidp.Price
                                          };
            return basket.ToList();
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.StackTrace);
            throw;
        }
    }
}
