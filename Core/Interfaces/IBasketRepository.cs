using Core.Models;

namespace Core.Interfaces
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string basketId);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string basketId);
        Task<CustomerBasket> RemoveItemFromBasketAsync(string basketId, int productId);
        //Task<CustomerBasket> AddItemToBasketAsync(string basketId, BasketItem item);
        Task<CustomerBasket> AddItemToBasketAsync(CustomerBasket basket);

    }
}
