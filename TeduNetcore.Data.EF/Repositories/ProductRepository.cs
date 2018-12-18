using TeduNetcore.Data.Entities;
using TeduNetcore.Data.IRepositories;

namespace TeduNetcore.Data.EF.Repositories
{
    public class ProductRepository : EFRepository<Product, int>, IProductRepository
    {
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
