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
        }

        [Authorize]
        public static async Task<Results<Ok<APIResponse>, BadRequest<APIResponse>>> CreateOrder(
            [FromServices] IHttpContextAccessor _httpContextAccessor,
            [FromServices] UserManager<AppUser> _userManager,
            [FromServices] IOrderService _orderService,
            [FromServices] APIResponse _apiResponse,
            [FromServices] IMapper _mapper,
            OrderDto orderDto)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            var email = httpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var address = _mapper.Map<OrderCustomerAddress>(orderDto.ShipToAddress);

            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

            if (order == null) return TypedResults.BadRequest(_apiResponse.BadRequestResponse("Problem creating order"));

            var result = _apiResponse.OkResponse(order);
            return TypedResults.Ok(result);
        }
    }
}
