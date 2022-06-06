using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.ViewModels.Products;
using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository,IWebHostEnvironment webHostEnvironment)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            this._webHostEnvironment = webHostEnvironment; // Projemizdeki enviroments'a kadar wwwroot'a kadar (staticfiles'lara) erişmemizi sağlayan servisdir..
        }

        [HttpGet]
        // Task yazmayı unutursak DbContext dispose olur.
        public async Task<IActionResult> Get()
        {
            return Ok(_productReadRepository.GetAll(false));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(_productReadRepository.GetByIdAsync(id,false)); 
        }

        [HttpPost]

        public async Task<IActionResult> Post(VM_Create_Product model)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock,
            });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {

            Product product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Stock = model.Stock;
            product.Price = model.Price;
            product.Name = model.Name;
            await _productWriteRepository.SaveAsync();


            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        
        {   
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
        [HttpPost("[action]")]

        public async Task<IActionResult> Upload()
        {   
            //wwwroot/resource/product-images
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath,"resource/product-images");

            if(!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

            // Form Data'dan gelen istekleri yakalamaya yardım ediyor.
            Random r = new();
            foreach (IFormFile file in Request.Form.Files)
            {
                string v = Path.GetExtension(
                                    file.FileName);
                string fullPath = Path.Combine(uploadPath, $"{r.Next()}{v}");

                using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None,1024*1024,useAsync:false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                
            }

            return Ok();
        }



    }
}
