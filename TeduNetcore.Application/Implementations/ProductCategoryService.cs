using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Linq;
using TeduNetcore.Application.Interfaces;
using TeduNetcore.Application.ViewModels;
using TeduNetcore.Data.Entities;
using TeduNetcore.Data.IRepositories;
using TeduNetcore.Infrastructure.Intarfaces;

namespace TeduNetcore.Application.Implementations
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository,IUnitOfWork unitOfWork)
        {
            _productCategoryRepository = productCategoryRepository;
            _unitOfWork = unitOfWork;
        }
        public ProductCategoryViewModel Add(ProductCategoryViewModel productCategoryVm)
        {
            //return null;
            ProductCategory productCategory = Mapper.Map<ProductCategoryViewModel, ProductCategory>(productCategoryVm);
            _productCategoryRepository.Add(productCategory);
            return productCategoryVm;
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<ProductCategoryViewModel> GetAll()
        {
            return _productCategoryRepository.FindAll()?.OrderBy(p => p.ParentId)
                                                        .ProjectTo<ProductCategoryViewModel>()
                                                        .ToList();
        }

        public List<ProductCategoryViewModel> GetAll(string keyword)
        {
            return !string.IsNullOrWhiteSpace(keyword) ? _productCategoryRepository.FindAll(x => x.Name.Contains(keyword) || x.SeoDescription.Contains(keyword))
                                                                                 .OrderBy(p => p.ParentId)
                                                                                 .ProjectTo<ProductCategoryViewModel>()
                                                                                 .ToList() :
                                                         GetAll();
        }

        public List<ProductCategoryViewModel> GetAllByParentId(int parentId)
        {
            return _productCategoryRepository.FindAll(x => x.Status == Data.Enums.Status.Active && x.ParentId == parentId)
                                                                                 .OrderBy(p => p.ParentId)
                                                                                 .ProjectTo<ProductCategoryViewModel>()
                                                                                 .ToList();
        }

        public ProductCategoryViewModel GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<ProductCategoryViewModel> GetHomeCategories(int top)
        {
            return _productCategoryRepository.FindAll(x => x.HomeFlag, c => c.Products)
                                                     .OrderBy(x => x.HomeOrder)
                                                     .Take(top)
                                                     .ProjectTo<ProductCategoryViewModel>().
                                                     ToList();
        }

        public void ReOrder(int sourceId, int targetId)
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductCategoryViewModel productCategoryVm)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items)
        {
            throw new System.NotImplementedException();
        }
    }
}
