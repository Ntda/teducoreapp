using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TeduNetcore.Application.ViewModels;
using TeduNetcore.Data.Entities;

namespace TeduNetcore.Application.AutoMapper
{
    class DomainToViewModelMappingProfile:Profile
    {
        public DomainToViewModelMappingProfile()
        {
            this.CreateMap<ProductCategory, ProductCategoryViewModel>();
        }
    }
}
