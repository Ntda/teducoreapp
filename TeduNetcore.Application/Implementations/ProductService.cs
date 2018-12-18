using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using TeduNetcore.Application.Interfaces;
using TeduNetcore.Application.ViewModels;
using TeduNetcore.Data.IRepositories;

namespace TeduNetcore.Application.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IList<ProductViewModel> GetAll()
        {
            IQueryable<ProductViewModel> products = _productRepository.FindAll(p => p.ProductCategory).ProjectTo<ProductViewModel>();
            return products.ToList();
        }
    }
}
