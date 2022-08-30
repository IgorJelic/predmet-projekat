using AutoMapper;
using ProductsService.DbInfrastructure;
using ProductsService.Dto;
using ProductsService.Models;
using ProductsService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsService.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductService(ProductDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public ProductDto AddProduct(ProductDto newProduct)
        {
            try
            {
                _dbContext.Products.Add(_mapper.Map<Product>(newProduct));
                _dbContext.SaveChanges();

                return newProduct;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ProductDto> GetAllProducts()
        {
            return _mapper.Map<List<ProductDto>>(_dbContext.Products);
        }

        public ProductDto GetProduct(long id)
        {
            var product = _dbContext.Products.Find(id);

            if (product == null)
            {
                return null;
            }

            return _mapper.Map<ProductDto>(product);
        }
    }
}
