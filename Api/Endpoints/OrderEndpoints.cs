using Api.ApiResponse;
using Api.Dto;
using Carter;
using Core.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Core.Models.Identity;
using AutoMapper;
using Core.Models.OrderAggregate;
using Api.Extensions;
using System.Security.Claims;

namespace Api.Endpoints
{
    public class OrderEndpoints : ICarterModule
    {

        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/basket");
            group.MapPost("CreateOrder",  CreateOrder).RequireAuthorization(); ;
            group.MapGet("GetOrdersForUser",  GetOrdersForUser).RequireAuthorization(); ;
            group.MapGet("GetOrdersByIdForUser/{id}",  GetOrdersByIdForUser).RequireAuthorization(); ;
        }

        [Authorize]
        public static async Task<Results<Ok<APIResponse>, BadRequest<APIResponse>>> CreateOrder(
            [FromServices] IHttpContextAccessor _httpContextAccessor,
            [FromServices] IOrderService _orderService,
            [FromServices] APIResponse _apiResponse,
            [FromServices] IMapper _mapper,
            [FromBody] OrderDto orderDto)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            var email = httpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var address = _mapper.Map<OrderCustomerAddress>(orderDto.ShipToAddress);

            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

            if (order == null) return TypedResults.BadRequest(_apiResponse.BadRequestResponse("Problem creating order"));

            var result = _apiResponse.OkResponse(order);
            return TypedResults.Ok(result);
        }

        public static async Task<Results<Ok<APIResponse>, BadRequest<APIResponse>, NotFound<APIResponse>>> GetOrdersForUser(
            [FromServices] IHttpContextAccessor _httpContextAccessor,
            [FromServices] IOrderService _orderService,
            [FromServices] APIResponse _apiResponse,
            [FromServices] IMapper _mapper)
        {
            try 
            {
                var httpContext = _httpContextAccessor.HttpContext;

                var email = httpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

                var orders = await _orderService.GetOrdersForUserAsync(email);

                if (orders == null)
                {
                    return TypedResults.NotFound(_apiResponse.NotFoundResponse("there is no order for you"));
                }

                var result = _mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders);

                return TypedResults.Ok(_apiResponse.OkResponse(result));
            } 
            catch(Exception ex)
            {
                return TypedResults.BadRequest(_apiResponse.BadRequestResponse(ex.Message));
            }
        }

        public static async Task<Results<Ok<APIResponse>, BadRequest<APIResponse>, NotFound<APIResponse>>> GetOrdersByIdForUser(
            [FromServices] IHttpContextAccessor _httpContextAccessor,
            [FromServices] IOrderService _orderService,
            [FromServices] APIResponse _apiResponse,
            [FromServices] IMapper _mapper,
            int id)
        {
            try 
            {
                var httpContext = _httpContextAccessor.HttpContext;

                var email = httpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

                var orders = await _orderService.GetOrderByIdAsync(id, email);

                if (orders == null)
                {
                    return TypedResults.NotFound(_apiResponse.NotFoundResponse("there is no order for you"));
                }

                var result = _mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders);

                return TypedResults.Ok(_apiResponse.OkResponse(result));
            } 
            catch(Exception ex)
            {
                return TypedResults.BadRequest(_apiResponse.BadRequestResponse(ex.Message));
            }
        }
    }
}
