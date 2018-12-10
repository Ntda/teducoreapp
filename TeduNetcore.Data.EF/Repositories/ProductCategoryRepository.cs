using System.Linq;
using TeduNetcore.Data.Entities;
using TeduNetcore.Data.IRepositories;

namespace TeduNetcore.Data.EF.Repositories
{
    public class ProductCategoryRepository : EFRepository<ProductCategory, int>, IProductCategoryRepository
    {
        private readonly AppDbContext appDbContext;
        public ProductCategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IQueryable<ProductCategory> GetByAlias(string alias)
        {
            return appDbContext.ProductCategories.Where(x => x.SeoAlias.Equals(alias));
        }
    }
}
