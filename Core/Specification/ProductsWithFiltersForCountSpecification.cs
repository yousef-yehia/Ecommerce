//using Core.Models;
//using Core.Specification;

//namespace Core.Specifications
//{
//    public class ProductsWithFiltersForCountSpecification : BaseSpecification<Product>
//    {
//        public ProductsWithFiltersForCountSpecification(ProductSpecParams productParams) : base(x =>
//            (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
//            (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId))
//        {

//        }
//    }
//}