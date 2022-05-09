using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        [HttpGet]
        // Task yazmayı unutursak DbContext dispose olur.
        public async Task Get()
        {
            await _productWriteRepository.AddRangeAsync(new()
            {
                new() { Id = Guid.NewGuid(), Name ="Product 1",Price =100F,CreatedDate=DateTime.UtcNow,Stock=10},
                new() { Id = Guid.NewGuid(), Name = "Product 2", Price = 200F, CreatedDate = DateTime.UtcNow, Stock = 20 },
                new() { Id = Guid.NewGuid(), Name = "Product 3", Price = 300F, CreatedDate = DateTime.UtcNow, Stock = 30 },

            });
            var count = await _productWriteRepository.SaveAsync();

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
           Product product= await _productReadRepository.GetByIdAsync(id);
            return Ok(product);
        }

    }
}
