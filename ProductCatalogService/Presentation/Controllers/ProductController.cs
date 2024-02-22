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
using AutoMapper;
using ProductCatalogService.Core.Business.Interfaces;
using ProductCatalogService.Presentation.DTOs.Product;


namespace ProductCatalogService.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductController(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        [HttpGet("GetAllProducts")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var products = await _productRepository.GetAllAsync();
 
            return Ok(products);
        }

        [HttpGet("GetProduct")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Product>> GetProduct(int productId)
        {
            var product = await _productRepository.GetAsync(productId);

            if (product == null)
            {
                throw new Exception($"ProductID {productId} is not found.");
            }

            return Ok(product);
        }

        [HttpPost("CreateProduct")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Product>> CreateCategory(Product product)
        {

            await _productRepository.CreateAsync(product);

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut("UpdateProduct")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> UpdateProduct(int productId, Product productForUpdate)
        {
            if (productId != productForUpdate.Id)
            {
                return BadRequest("Invalid Product Id");
            }

            var product = await _productRepository.GetAsync(productId);

            if (product == null)
            {
                throw new Exception($"ProductID {productId} is not found.");
            }

            try
            {
                await _productRepository.UpdateAsync(productForUpdate);
            }
            catch (Exception)
            {
                throw new Exception($"Error occured while updating ProductID {productId}.");
            }

            return NoContent();
        }

        [HttpDelete("DeleteProduct")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            await _productRepository.DeleteAsync(productId);

            return NoContent();
        }
    }
}