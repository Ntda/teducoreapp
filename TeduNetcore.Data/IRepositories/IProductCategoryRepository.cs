using System.Linq;
using TeduNetcore.Data.Entities;
using TeduNetcore.Infrastructure.Intarfaces;

namespace TeduNetcore.Data.IRepositories
{
    public interface IProductCategoryRepository : IRepository<ProductCategory, int>
    {
        IQueryable<ProductCategory> GetByAlias(string alias);
    }
}
