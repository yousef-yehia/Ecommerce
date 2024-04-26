using Api.ApiResponse;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly APIResponse _apiResponse;

        public BasketController(IBasketRepository basketRepository, APIResponse apiResponse)
        {
            _basketRepository = basketRepository;
            _apiResponse = apiResponse;
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetBasketById(string id) 
        {
            try 
            {
                var basket = await _basketRepository.GetBasketAsync(id);
                var response = _apiResponse.OkResponse(basket);
                return Ok(response);

            }
            catch(Exception ex) 
            {
                return BadRequest( _apiResponse.NotFoundResponse(ex.Message));
            }
           
        }

        [HttpPost]
        public async Task<ActionResult<APIResponse>> UpdateBasket(CustomerBasket basket)
        {
            try 
            {
                var updatedBasket = await _basketRepository.UpdateBasketAsync(basket);
                var result = _apiResponse.OkResponse(updatedBasket);
                return Ok(result);
            }
            catch(Exception ex) 
            {
                return BadRequest(_apiResponse.BadRequestResponse(ex.Message));
            }

        }

        [HttpDelete]
        public async Task<ActionResult<APIResponse>> DeleteBasketAsync(string id)
        {
            try 
            {
                var result = await _basketRepository.DeleteBasketAsync(id);
                return Ok(_apiResponse.OkResponse(result));

            }
            catch (Exception ex) 
            {
                return BadRequest(_apiResponse.BadRequestResponse(ex.Message));
            }
        }
    }
}
