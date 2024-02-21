using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalogService.Infrastructure.Data;
using ProductCatalogService.Core.Domain.Entities;
using Asp.Versioning;


namespace ProductCatalogService.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IApplicationDbContext _context;

        public ProductController(IApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.Products.ToListAsync();

            return products == null ? NotFound() : Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products.Where(a => a.Id == id).FirstOrDefaultAsync();

            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChanges();

            return Ok(product.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product productData)
        {
            var product = _context.Products.Where(a => a.Id == id).FirstOrDefault();

            if (product == null)
                return NotFound();
            else
            {
                product.CategoryId = productData.CategoryId;
                product.Title = productData.Title;
                product.Description = productData.Description;
                product.Price = productData.Price;
                product.PhotoLink = productData.PhotoLink;

                await _context.SaveChanges();

                return Ok(product.Id);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.Where(a => a.Id == id).FirstOrDefaultAsync();

            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChanges();

            return Ok(product.Id);
        }
    }
}