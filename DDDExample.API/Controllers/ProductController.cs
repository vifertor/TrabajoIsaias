using Microsoft.AspNetCore.Mvc;
using DDDExample.Application.Services;
using DDDExample.Application.DTOs;

namespace DDDExample.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductController(ProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Post(ProductDto dto) =>
            Ok(await _service.AddAsync(dto));
    }
}
