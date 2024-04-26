//using Core.Interfaces;
//using Core.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Core.Specification;
//using Infrastructure.Repository;
//using AutoMapper;
//using Api.Dto;
//using Api.ApiResponse;
//using Carter;
//using Microsoft.AspNetCore.Http.HttpResults;
//using Api.Helper;

//namespace Api.Endpoints
//{
//    public class ProductEndpoints : ICarterModule
//    {

//        public void AddRoutes(IEndpointRouteBuilder app)
//        {
//            var group = app.MapGroup("api/products");
//            group.MapGet("GetProducts", GetProducts).WithName(nameof(GetProduct));
//            group.MapGet("GetProduct/{id}", GetProduct).WithName(nameof(GetProducts));
//        }

//        public async Task<Results<Ok<APIResponse>, BadRequest<APIResponse>>> GetProducts(
//            [FromQuery] ProductSpecParams productSpecParams, 
//            [FromServices] IProductRepository _productRepository, [FromServices] APIResponse _response, [FromServices] IMapper _mapper)
//        {
//            try
//            {
//                var spec = new ProductsWithBrandsAndTypesSpecification(productSpecParams);
//                var products = await _productRepository.ListAsync(spec);
//                var pagedProducts = await Pagination<Product>.Paginate(products.AsQueryable(), productSpecParams.PageIndex, productSpecParams.PageSize);
//                var productsResponse = _mapper.Map<IReadOnlyList<ProductResponseDto>>(pagedProducts);
//                var pagedProductResponse = await Pagination<Product>.CreateAsync(products.AsQueryable(), productSpecParams.PageIndex, productSpecParams.PageSize);

//                var response = _response.OkResponse(pagedProductResponse);
//                return TypedResults.Ok(response);
//            }
//            catch (Exception ex)
//            {
//                var response = _response.BadRequestResponse(ex.Message);
//                return TypedResults.BadRequest(response);
//            }

//        }

//        public async Task<Results<Ok<APIResponse>, BadRequest<APIResponse>>> GetProduct(int id, [FromServices] IProductRepository _productRepository, [FromServices] APIResponse _response, [FromServices] IMapper _mapper)
//        {
//            try
//            {
//                var spec = new ProductsWithBrandsAndTypesSpecification(id);
//                var product = await _productRepository.GetEntityWithSpecAsync(spec);
//                var productResponse = _mapper.Map<ProductResponseDto>(product);

//                var response = _response.OkResponse(productResponse);
//                return TypedResults.Ok(response);
//            }
//            catch (Exception ex)
//            {
//                var response = _response.BadRequestResponse(ex.Message);
//                return TypedResults.BadRequest(response);
//            }

//        }


//    }
//}
