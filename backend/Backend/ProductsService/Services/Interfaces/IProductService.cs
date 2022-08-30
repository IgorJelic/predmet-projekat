using ProductsService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsService.Services.Interfaces
{
    public interface IProductService
    {
        List<ProductDto> GetAllProducts();
        ProductDto AddProduct(ProductDto newProduct);
        ProductDto GetProduct(long id);
    }
}
