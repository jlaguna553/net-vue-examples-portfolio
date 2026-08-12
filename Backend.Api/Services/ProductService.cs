using System.Text.Json;
using Backend.Api.Models;

namespace Backend.Api.Services;

public class ProductService
{
    private readonly string _filePath;

    // Inyectamos IWebHostEnvironment para obtener la ruta absoluta física del proyecto
    public ProductService(IWebHostEnvironment env)
    {
        _filePath = Path.Combine(env.ContentRootPath, "products.json");
    }

    // LECTURA: Lee el archivo JSON y lo mapea a objetos C#
    public async Task<List<Product>> ObtenerTodosAsync()
    {
        if (!File.Exists(_filePath))
            return new List<Product >();

        var json = await File.ReadAllTextAsync(_filePath);
        
        return JsonSerializer.Deserialize<List<Product>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true // Permite leer minúsculas/mayúsculas sin problemas
        }) ?? new List<Product>();
    }

    // ESCRITURA: Sobrescribe el archivo JSON con la lista actualizada
    private async Task GuardarTodosAsync(List<Product> productos)
    {
        var json = JsonSerializer.Serialize(productos, new JsonSerializerOptions 
        { 
            WriteIndented = true // Formatea el JSON bonito con sangrías
        });
        await File.WriteAllTextAsync(_filePath, json);
    }

    // POST: Agrega un nuevo producto calculando el ID autoincrementable
    public async Task<Product> AgregarAsync(Product nuevo)
    {
        var productos = await ObtenerTodosAsync();
        
        nuevo.Id = productos.Any() ? productos.Max(p => p.Id) + 1 : 1;
        productos.Add(nuevo);

        await GuardarTodosAsync(productos);
        return nuevo;
    }

    // PUT: Busca el elemento por ID y actualiza sus valores
    public async Task<bool> ActualizarAsync(int id, Product actualizado)
    {
        var products = await ObtenerTodosAsync();
        var index = products.FindIndex(p => p.Id == id);

        if (index == -1) return false;

        actualizado.Id = id; // Mantiene el ID original
        products[index] = actualizado;

        await GuardarTodosAsync(products);
        return true;
    }
}