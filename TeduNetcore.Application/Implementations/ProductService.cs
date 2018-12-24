using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using TeduNetcore.Application.Interfaces;
using TeduNetcore.Application.ViewModels;
using TeduNetcore.Data.Entities;
using TeduNetcore.Data.Enums;
using TeduNetcore.Data.IRepositories;
using TeduNetcore.Infrastructure.Intarfaces;
using TeduNetcore.Utilities.Dtos;
using TeduNetcore.Utilities.Helpers;

namespace TeduNetcore.Application.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IProductTagRepository _productTagRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IProductRepository productRepository,
                                ITagRepository tagRepository,
                                    IProductTagRepository productTagRepository,
                                        IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _tagRepository = tagRepository;
            _productTagRepository = productTagRepository;
            _unitOfWork = unitOfWork;
        }

        public ProductViewModel Add(ProductViewModel productVM)
        {
            Product productEntity = Mapper.Map<ProductViewModel, Product>(productVM);
            if (!string.IsNullOrEmpty(productEntity.Tags))
            {
                string[] tags = productEntity.Tags.Split(',');
                List<ProductTag> productTags = new List<ProductTag>();
                foreach (string t in tags)
                {

                    string tagId = TextHelper.ToUnsignString(t);
                    if (!_tagRepository.FindAll(tag => tag.Id.Equals(tagId)).Any())
                    {
                        Tag tag = new Tag
                        {
                            Id = tagId,
                            Name = t,
                            Type = "Product"
                        };
                        _tagRepository.Add(tag);
                    }
                    ProductTag productTag = new ProductTag()
                    {
                        ProductId = productEntity.Id,
                        TagId = tagId
                    };
                    productTags.Add(productTag);
                }
                productEntity.ProductTags.AddRange(productTags);
            }
            _productRepository.Add(productEntity);
            return productVM;
        }

        public void Delete(int id)
        {
            _productRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IList<ProductViewModel> GetAll()
        {
            IQueryable<ProductViewModel> products = _productRepository.FindAll(p => p.ProductCategory)
                                                                                            .ProjectTo<ProductViewModel>()
                                                                                                .OrderBy(p => p.DateCreated);
            return products.ToList();
        }

        public PageResult<ProductViewModel> GetAllPage(int? categoryId, string keyWord, int indexCurrentPage, int pageSize)
        {
            IQueryable<Data.Entities.Product> query = _productRepository.FindAll(q => q.Status.Equals(Status.Active));
            if (!string.IsNullOrWhiteSpace(keyWord))
            {
                query = query.Where(q => q.Name.Contains(keyWord));
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

        public ProductViewModel GetById(int id)
        {
            Product product = _productRepository.FindById(id);
            ProductViewModel ProductViewModel = Mapper.Map<Product, ProductViewModel>(product);
            return ProductViewModel;
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductViewModel productVM)
        {
            Product productEntity = Mapper.Map<ProductViewModel, Product>(productVM);
            if (!string.IsNullOrEmpty(productEntity.Tags))
            {
                string[] tags = productEntity.Tags.Split(',');
                List<ProductTag> productTags = new List<ProductTag>();
                foreach (string t in tags)
                {
                    string tagId = TextHelper.ToUnsignString(t);
                    if (!_tagRepository.FindAll(tag => tag.Id.Equals(tagId)).Any())
                    {
                        Tag tag = new Tag
                        {
                            Id = tagId,
                            Name = t,
                            Type = "Product"
                        };
                        _tagRepository.Add(tag);
                    }
                    _productTagRepository.RemoveMultiple(_productTagRepository.FindAll(p => p.Id == productVM.Id).ToList());
                    ProductTag productTag = new ProductTag()
                    {
                        ProductId = productEntity.Id,
                        TagId = tagId
                    };

                    productTags.Add(productTag);
                }
                productEntity.ProductTags.AddRange(productTags);

            }
            _productRepository.Update(productEntity);
        }
    }
}
