using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using TeduNetcore.Application.Interfaces;
using TeduNetcore.Application.ViewModels;
using TeduNetcore.Data.Enums;
using TeduNetcore.Data.IRepositories;
using TeduNetcore.Utilities.Dtos;

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

        public PageResult<ProductViewModel> GetAllPage(int? categoryId, string keyWord, int indexCurrentPage, int pageSize)
        {
            IQueryable<Data.Entities.Product> query = _productRepository.FindAll(q => q.Status.Equals(Status.Active));
            if (!string.IsNullOrWhiteSpace(keyWord))
            {
                query = query.Where(q => q.Name.Equals(keyWord));
            }
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }
            int totalRow = query.Count();
            query = query.OrderByDescending(q => q.DateCreated).Skip((indexCurrentPage - 1) * pageSize).Take(pageSize);
            List<ProductViewModel> products = query.ProjectTo<ProductViewModel>().ToList();
            return new PageResult<ProductViewModel>()
            {
                Result = products,
                TotalRow = totalRow
            };
        }
    }
}
