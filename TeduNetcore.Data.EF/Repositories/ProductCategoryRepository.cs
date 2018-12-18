using System.Linq;
using TeduNetcore.Data.Entities;
using TeduNetcore.Data.IRepositories;

namespace TeduNetcore.Data.EF.Repositories
{
    public class ProductCategoryRepository : EFRepository<ProductCategory, int>, IProductCategoryRepository
    {
        public ProductCategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public IQueryable<ProductCategory> GetByAlias(string alias)
        {
            return _appDbContext.ProductCategories.Where(x => x.SeoAlias.Equals(alias));
        }
    }
}
