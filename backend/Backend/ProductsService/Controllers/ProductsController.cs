using Microsoft.AspNetCore.Mvc;
using ProductsService.Dto;
using ProductsService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService service)
        {
            this._productService = service;
        }

        // GET: api/Products
        // ROLE: logged
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_productService.GetAllProducts());
        }

        // GET api/Products/5
        // ROLE: logged
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var product = _productService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST api/Products
        // ROLE: admin
        [HttpPost]
        public IActionResult Post([FromBody] ProductDto product)
        {
            try
            {
                var addedProduct = _productService.AddProduct(product);
                return Ok(addedProduct);
            }
            catch (Exception e)
            {
                return base.Conflict(e.Message);
            }
        }
    }
}
