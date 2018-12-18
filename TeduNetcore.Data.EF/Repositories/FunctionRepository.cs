using TeduNetcore.Data.Entities;
using TeduNetcore.Data.IRepositories;

namespace TeduNetcore.Data.EF.Repositories
{
    public class FunctionRepository : EFRepository<Function, string>, IFunctionRepository
    {
        public FunctionRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
