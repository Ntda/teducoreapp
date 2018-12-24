using AutoMapper;
using TeduNetcore.Application.ViewModels;
using TeduNetcore.Data.Entities;

namespace TeduNetcore.Application.AutoMapper
{
    internal class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProductCategoryViewModel, ProductCategory>()
                .ConstructUsing(productCategoryVM => InitProductCategory(productCategoryVM));
            CreateMap<ProductViewModel, Product>()
                .ConstructUsing(productVM => InitProduct(productVM));
        }

        private Product InitProduct(ProductViewModel productVM)
        {
            return new Product
            {
                Name = productVM.Name,
                CategoryId = productVM.CategoryId,
                Image = productVM.Image,
                Price = productVM.Price,
                OriginalPrice = productVM.OriginalPrice,
                PromotionPrice = productVM.PromotionPrice,
                Description = productVM.Description,
                Content = productVM.Content,
                HomeFlag = productVM.HomeFlag,
                HotFlag = productVM.HotFlag,
                Tags = productVM.Tags,
                Unit = productVM.Unit,
                Status = productVM.Status,
                SeoPageTitle = productVM.SeoPageTitle,
                SeoAlias = productVM.SeoAlias,
                SeoKeywords = productVM.SeoKeywords,
                SeoDescription = productVM.SeoDescription
            };
        }
        private ProductCategory InitProductCategory(ProductCategoryViewModel productCategoryVM)
        {
            return new ProductCategory()
            {
                Name = productCategoryVM.Name,
                Description = productCategoryVM.Description,
                ParentId = productCategoryVM.ParentId,
                HomeOrder = productCategoryVM.HomeOrder,
                Image = productCategoryVM.Image,
                HomeFlag = productCategoryVM.HomeFlag,
                SortOrder = productCategoryVM.SortOrder,
                Status = productCategoryVM.Status,
                SeoPageTitle = productCategoryVM.SeoPageTitle,
                SeoAlias = productCategoryVM.SeoAlias,
                SeoKeywords = productCategoryVM.SeoKeywords,
                SeoDescription = productCategoryVM.SeoDescription,
            };
        }
    }
}
