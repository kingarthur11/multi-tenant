using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyProject.DTOs;
using MyProject.Repository;

namespace MyProject.Controller
{
    [Route("api/product")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService; 

        public ProductsController(IProductService productService)
        {
            _productService = productService; // inject the products service
        }
   
        // Get list of products
        [HttpGet]
        public IActionResult Get()
        {   
            var list = _productService.GetAllProducts(); 
            return Ok(list);
        }

        // Create a new product
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductRequest request)
        {
            var response = await _productService.CreateProduct(request);
            return Ok(response);
        }

        // Delete a product by id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _productService.DeleteProduct(id);
            return Ok(result);
        }

    }
}