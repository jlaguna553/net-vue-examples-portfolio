using Backend.Api.Models;
using Backend.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")] // Ruta base: /api/products
public class ProductsController : ControllerBase
{
    private readonly ProductService _service;

    public ProductsController(ProductService service)
    {
        _service = service;
    }

    // GET: /api/products
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await _service.ObtenerTodosAsync();
        return Ok(products);
    }

    // POST: /api/products
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Product product)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var creado = await _service.AgregarAsync(product);
        return CreatedAtAction(nameof(Get), new { id = creado.Id }, creado);
    }

    // PUT: /api/products/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Product product)
    {
        var actualizado = await _service.ActualizarAsync(id, product);
        if (!actualizado) return NotFound(new { mensaje = "Producto no encontrado" });

        return NoContent(); // HTTP 204: Operación exitosa sin cuerpo de respuesta
    }
}