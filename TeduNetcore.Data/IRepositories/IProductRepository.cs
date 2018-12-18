using TeduNetcore.Data.Entities;
using TeduNetcore.Infrastructure.Intarfaces;

namespace TeduNetcore.Data.IRepositories
{
    public interface IProductRepository : IRepository<Product, int>
    {
    }
}
