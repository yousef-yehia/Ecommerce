using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly StoreDbContext _storeDb;

        public ProductRepository(StoreDbContext appDb) : base(appDb)
        {
            _storeDb = appDb;

        }

        public async Task<bool> DoesProdectsExist(CustomerBasket customerBasket)
        {
            var products = await _storeDb.Products.ToListAsync();

            bool allItemsExistInProducts = customerBasket.Items.All(item => products.Any(p => p.Id == item.Id));
            return allItemsExistInProducts;
        }
    }
}
