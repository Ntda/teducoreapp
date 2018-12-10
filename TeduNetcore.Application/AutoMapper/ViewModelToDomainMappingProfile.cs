using AutoMapper;
using TeduNetcore.Application.ViewModels;
using TeduNetcore.Data.Entities;

namespace TeduNetcore.Application.AutoMapper
{
    internal class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProductCategoryViewModel, ProductCategory>().ConstructUsing(productCategoryVM => InitProductCategory(productCategoryVM));
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
