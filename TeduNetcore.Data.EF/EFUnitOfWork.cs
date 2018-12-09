using TeduNetcore.Infrastructure.Intarfaces;

namespace TeduNetcore.Data.EF
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;

        public EFUnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public void Commit()
        {
            _appDbContext.SaveChanges();
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }
    }
}
