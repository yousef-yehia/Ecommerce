using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Specification
{
    public class ProductsWithBrandsAndTypesSpecification : BaseSpecification<Product>
    {
        public ProductsWithBrandsAndTypesSpecification()
        {
            AddInclude(x=> x.ProductBrand);
            AddInclude(x=> x.ProductType);
        }
        public ProductsWithBrandsAndTypesSpecification(int id) : base(x=> x.Id == id)
        {
            AddInclude(x=> x.ProductBrand);
            AddInclude(x=> x.ProductType);
        }
    }
}
