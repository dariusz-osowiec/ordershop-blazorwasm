using Microsoft.Extensions.Options;
using OrderShopWeb.Interfaces;
using OrderShopWeb.Models;

namespace OrderShopWeb.Services;

/// <summary>
/// Obsługa bazy danych z API.
/// </summary>
public class HttpDbService : IDbOperable
{
    JsonSerializerOptions options;

    /// <summary>
    /// Lista produktów z bazy danych.
    /// </summary>
    public List<Product> Products { get; set; } = new();

    public HttpDbService(JsonSerializerOptionsService optionsService)
    {
        options = optionsService.Options;
    }

    /// <summary>
    /// Wyciągnięcie bazy danych.
    /// </summary>
    /// <returns></returns>
    public List<Product> GetDb()
    {
        return Products;
    }

    /// <summary>
    /// Dodanie produktu do bazy.
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    public async Task<bool> Create(Product product)
    {
        return true;
    }

    /// <summary>
    /// Wyciągnięcie wszystkich produktów z bazy.
    /// </summary>
    /// <returns></returns>
    public async Task<List<Product>> ReadAll()
    {
        List<Product> products = new();

        HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync($"{Statics.protocol}://{Statics.address}:{Statics.port}/db/readall");

        if (response.IsSuccessStatusCode)
        {
            string result = await response.Content.ReadAsStringAsync();
            products = JsonSerializer.Deserialize<List<Product>>(result, options);
            Products = products;
        }

        return products;
    }

    /// <summary>
    /// Wyciągnięcie promowanych produktów z bazy (one pokazują się na stronie głównej).
    /// </summary>
    /// <returns></returns>
    public async Task<List<Product>> ReadPromoted()
    {
        List<Product> products = new();

        HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync($"{Statics.protocol}://{Statics.address}:{Statics.port}/db/readpromoted");

        if (response.IsSuccessStatusCode)
        {
            string result = await response.Content.ReadAsStringAsync();
            products = JsonSerializer.Deserialize<List<Product>>(result, options);
            Products = products;
        }

        return products;
    }

    /// <summary>
    /// Wyciągnięcie konkretnego produktu z bazy.
    /// </summary>
    /// <param name="id">Id poszukiwanego produktu w bazie.</param>
    /// <returns></returns>
    public async Task<Product> Read(int id)
    {
        Product product = new Product();
        return product;
    }

    /// <summary>
    /// Zaktualizowanie produktu w bazie.
    /// </summary>
    /// <param name="product">Produkt do zaktualizowania.</param>
    /// <returns></returns>
    public async Task<bool> Update(Product product)
    {
        return true;
    }

    /// <summary>
    /// Usunięcie produktu w bazie.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> Delete(int id)
    {
        return true;
    }
}
