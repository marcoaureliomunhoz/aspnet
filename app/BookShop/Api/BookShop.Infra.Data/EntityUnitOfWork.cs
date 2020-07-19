using BookShop.Domain.Common;

namespace BookShop.Infra.Data
{
    public class EntityUnitOfWork : IEntityUnitOfWork
    {
        private readonly BookShopDbContext _context;

        public EntityUnitOfWork(BookShopDbContext context)
        {
            _context = context;
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }
    }
}