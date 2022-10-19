using OrderShopWeb.Models;

namespace OrderShopWeb.Interfaces;

/// <summary>
/// Obsługa koszyka.
/// </summary>
public interface IBasketOperable
{
    /// <summary>
    /// Utworzenie koszyka.
    /// </summary>
    /// <returns>Pusty koszyk</returns>
    Task<List<Product>> CreateBasket();

    /// <summary>
    /// Zapisanie koszyka.
    /// </summary>
    /// <param name="basket">Koszyk do zapisania.</param>
    /// <returns>Stan koszyka.</returns>
    Task SaveBasket(List<Product> basket);

    /// <summary>
    /// Odczytanie koszyka.
    /// </summary>
    /// <returns>Koszyk zapisany w LocalStorage.</returns>
    Task<List<Product>?> ReadBasket();
}
