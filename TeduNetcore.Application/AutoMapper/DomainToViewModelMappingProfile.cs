using AutoMapper;
using TeduNetcore.Application.ViewModels;
using TeduNetcore.Data.Entities;

namespace TeduNetcore.Application.AutoMapper
{
    internal class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<ProductCategory, ProductCategoryViewModel>();
            CreateMap<Function, FunctionViewModel>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<AppRole, RoleViewModel>();
        }
    }
}
