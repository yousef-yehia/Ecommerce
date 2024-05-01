﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task<bool> DoesProdectsExist(CustomerBasket customerBasket); 
    }
}
