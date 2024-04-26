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
        public ProductsWithBrandsAndTypesSpecification(ProductSpecParams productSpecParams)
        {
            AddInclude(x=> x.ProductBrand);
            AddInclude(x=> x.ProductType);

            if (productSpecParams.Search != null)
            {
                AddCriteria(x => x.Name.ToLower().Contains(productSpecParams.Search.ToLower()));
            }

            if (productSpecParams.BrandId.HasValue)
            {
                AddCriteria(x=> x.ProductBrandId == productSpecParams.BrandId);
            }

            if (productSpecParams.TypeId.HasValue)
            {
                AddCriteria(x=> x.ProductTypeId == productSpecParams.TypeId);
            }

            if (productSpecParams.Sort != null)
            {
                productSpecParams.Sort = productSpecParams.Sort.ToLower();
                switch (productSpecParams.Sort) 
                {
                    case "nameasc":
                        AddOrderBy(x=> x.Name);
                        break;

                    case "namedesc":
                        AddOrderByDesc(x=> x.Name);
                        break;

                    case "priceasc":
                        AddOrderBy(x=> x.Price);
                        break;
                    case "pricedsc":
                        AddOrderByDesc(x=> x.Price);
                        break;
                }
            }

            ApplyPaging(productSpecParams.PageSize * (productSpecParams.PageIndex - 1), productSpecParams.PageSize);
        }
        public ProductsWithBrandsAndTypesSpecification(int id) : base(x=> x.Id == id)
        {
            AddInclude(x=> x.ProductBrand);
            AddInclude(x=> x.ProductType);
        }
    }
}
