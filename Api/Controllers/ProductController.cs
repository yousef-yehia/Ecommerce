using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Specification;
using Infrastructure.Repository;
using AutoMapper;
using Api.Dto;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper, IGenericRepository<ProductBrand> productBrandRepository, IGenericRepository<ProductType> productTypeRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _productBrandRepository = productBrandRepository;
            _productTypeRepository = productTypeRepository;
        }

        [HttpGet ("GetProducts", Name = "GetProducts")]
        public async Task<ActionResult<IReadOnlyList<ProductResponseDto>>> GetProducts()
        {
            var spec = new ProductsWithBrandsAndTypesSpecification();
            var products = await _productRepository.ListAsync(spec);
            var productsResponse = _mapper.Map < IReadOnlyList <ProductResponseDto>>(products);
            return Ok(productsResponse);
        }

        [HttpGet ("GetProduct {id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductResponseDto>> GetProduct(int id)
        {
            var spec = new ProductsWithBrandsAndTypesSpecification(id);
            var product = await _productRepository.GetEntityWothSpecAsync(spec);
            var productResponse = _mapper.Map<ProductResponseDto>(product);
            return Ok(productResponse);
        }
    }
}
