//using Core.Interfaces;
//using Core.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Core.Specification;
//using Infrastructure.Repository;
//using AutoMapper;
//using Api.Dto;
//using Api.ApiResponse;
//using Api.Helper;
//using Microsoft.AspNetCore.Http.HttpResults;

//namespace Api.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ProductController : ControllerBase
//    {
//        private readonly IProductRepository _productRepository;
//        private readonly IGenericRepository<ProductType> _productTypeRepository;
//        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
//        private readonly IMapper _mapper;
//        protected APIResponse _response;


//        public ProductController(IProductRepository productRepository, IMapper mapper, IGenericRepository<ProductBrand> productBrandRepository, IGenericRepository<ProductType> productTypeRepository)
//        {
//            _productRepository = productRepository;
//            _mapper = mapper;
//            _productBrandRepository = productBrandRepository;
//            _productTypeRepository = productTypeRepository;
//            this._response = new APIResponse();
//        }

//        [HttpGet("GetProducts", Name = "GetProducts")]
//        public async Task<ActionResult<APIResponse>> GetProducts([FromQuery] ProductSpecParams productSpecParams)
//        {
//            try
//            {
//                var spec = new ProductsWithBrandsAndTypesSpecification(productSpecParams);

//                var products = await _productRepository.ListAsync(spec);
//                var productsResponse = _mapper.Map<List<ProductResponseDto>>(products);
//                var PaginatedProductsResponse = Pagination<ProductResponseDto>.Paginate(productsResponse, productSpecParams.PageIndex, productSpecParams.PageSize);

//                //var countSpec = new ProductsWithFiltersForCountSpecification(productSpecParams);
//                //var totalItems = await _productRepository.CountAsync(countSpec);
//                //var PaginatedProductsResponse = Pagination<ProductResponseDto>.CreateAsync(productsResponse, productSpecParams.PageIndex, productSpecParams.PageSize, totalItems);
//                var response = _response.OkResponse(PaginatedProductsResponse);
//                return Ok(response);
//            }
//            catch (Exception ex)
//            {
//                var response = _response.BadRequestResponse(ex.Message);
//                return BadRequest(response);
//            }

//        }

//        [HttpGet("GetProduct {id}", Name = "GetProduct")]
//        public async Task<ActionResult<ProductResponseDto>> GetProduct(int id)
//        {
//            try
//            {
//                var spec = new ProductsWithBrandsAndTypesSpecification(id);
//                var product = await _productRepository.GetEntityWithSpecAsync(spec);
//                var productResponse = _mapper.Map<ProductResponseDto>(product);

//                var response = _response.OkResponse(productResponse);
//                return Ok(response);
//            }
//            catch (Exception ex)
//            {
//                var response = _response.BadRequestResponse(ex.Message);
//                return BadRequest(response);
//            }

//        }
//    }
//}