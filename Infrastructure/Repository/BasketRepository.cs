using System.Text.Json;
using Core.Interfaces;
using Core.Models;
using StackExchange.Redis;

namespace Infrastructure.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var data = await _database.StringGetAsync(basketId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var created = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket),
                TimeSpan.FromDays(30));

            return await GetBasketAsync(basket.Id);
        }

        public async Task<CustomerBasket> AddItemToBasketAsync(CustomerBasket basket)
        {
            if (basket == null)
            {
                throw new ArgumentNullException(nameof(basket));
            }

            var existingBasket = await GetBasketAsync(basket.Id);

            if (basket.PaymentIntentId != null) { existingBasket.PaymentIntentId = basket.PaymentIntentId; }
            if (basket.DeliveryMethodId != null) { existingBasket.DeliveryMethodId = basket.DeliveryMethodId; }
            existingBasket.ShippingPrice += basket.ShippingPrice;

            if (existingBasket != null)
            {
                foreach (var item in basket.Items)
                {
                    var existingItem = existingBasket.Items.FirstOrDefault(i => i.Id == item.Id);
                    if (existingItem != null)
                    {
                        existingItem.Quantity += item.Quantity;
                    }
                    else
                    {
                        existingBasket.Items.Add(item);
                    }
                }

                basket = existingBasket;
            }

            await UpdateBasketAsync(basket);
            

            return basket;
        }

        public async Task<CustomerBasket> RemoveItemFromBasketAsync(string basketId, int productId)
        {
            var basket = await GetBasketAsync(basketId);

            if (basket == null)
            {
                throw new Exception($"Basket {basketId} not found");
            }

            var itemToRemove = basket.Items.Find(i => i.Id == productId);

            if (itemToRemove != null)
            {
                basket.Items.Remove(itemToRemove);
            }

            await UpdateBasketAsync(basket);

            return basket;
        }
    }
}

