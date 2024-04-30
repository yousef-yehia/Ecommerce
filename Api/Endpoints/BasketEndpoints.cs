using System.Threading.Tasks;
using Api.ApiResponse;
using Carter;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints
{
    public class BasketEndpoints : ICarterModule
    {

        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/basket");
            group.MapGet("GetBasketById/{id}", GetBasketById);
            group.MapPut("UpdateBasket", UpdateBasket);
            group.MapDelete("DeleteBasket/{id}", DeleteBasket);
            group.MapDelete("RemoveItemFromBasket", RemoveItemFromBasket);
            group.MapPost("AddItemToBasket", AddItemToBasket);


        }

        public async Task<Results<Ok<APIResponse>, BadRequest<APIResponse>, NotFound<APIResponse>>> GetBasketById(
            string id, [FromServices] APIResponse _apiResponse, [FromServices] IBasketRepository _basketRepository)
        {
            try
            {
                var basket = await _basketRepository.GetBasketAsync(id);
                var response = _apiResponse.OkResponse(basket);
                return TypedResults.Ok(response);

            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(_apiResponse.NotFoundResponse(ex.Message));
            }

        }

        public async Task<Results<Ok<APIResponse>, BadRequest<APIResponse>>> UpdateBasket(
           CustomerBasket basket, [FromServices] APIResponse _apiResponse, [FromServices] IBasketRepository _basketRepository)
        {
            try
            {
                var updatedBasket = await _basketRepository.UpdateBasketAsync(basket);
                var result = _apiResponse.OkResponse(updatedBasket);
                return TypedResults.Ok(result);
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(_apiResponse.BadRequestResponse(ex.Message));
            }
        }
        public async Task<Results<Ok<APIResponse>, BadRequest<APIResponse>>> AddItemToBasket(
            [AsParameters] CustomerBasket basket, [FromServices] APIResponse _apiResponse, [FromServices] IBasketRepository _basketRepository)
        {
            try
            {
                var updatedBasket = await _basketRepository.AddItemToBasketAsync(basket);
                var result = _apiResponse.OkResponse(updatedBasket);
                return TypedResults.Ok(result);
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(_apiResponse.BadRequestResponse(ex.Message));
            }
        }

        public async Task<Results<Ok<APIResponse>, BadRequest<APIResponse>>> DeleteBasket(
            string id, [FromServices] APIResponse _apiResponse, [FromServices] IBasketRepository _basketRepository)
        {
            try
            {
                var result = await _basketRepository.DeleteBasketAsync(id);
                return TypedResults.Ok(_apiResponse.OkResponse(result));

            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(_apiResponse.BadRequestResponse(ex.Message));
            }
        }
        public async Task<Results<Ok<APIResponse>, BadRequest<APIResponse>>> RemoveItemFromBasket(
            string basketId, int productId, [FromServices] APIResponse _apiResponse, [FromServices] IBasketRepository _basketRepository)
        {
            try
            {
                var result = await _basketRepository.RemoveItemFromBasketAsync(basketId, productId);
                return TypedResults.Ok(_apiResponse.OkResponse(result));

            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(_apiResponse.BadRequestResponse(ex.Message));
            }
        }
    }
}
