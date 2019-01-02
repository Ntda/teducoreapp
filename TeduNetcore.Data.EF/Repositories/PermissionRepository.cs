using TeduNetcore.Data.Entities;
using TeduNetcore.Data.IRepositories;

namespace TeduNetcore.Data.EF.Repositories
{
    public class PermissionRepository : EFRepository<Permission, int>, IPermissionRepository
    {
        public PermissionRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
