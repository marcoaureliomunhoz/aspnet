using System.Threading;
using System.Threading.Tasks;
using BookShop.Domain.Books;
using BookShop.Domain.Books.Repositories;

namespace BookShop.Infra.Data.Repositories
{
    public class BookWriteRepository : IBookWriteRepository
    {
        private readonly BookShopDbContext _context;

        public BookWriteRepository(BookShopDbContext context)
        {
            _context = context;
        }

        public Task Insert(Book book, CancellationToken cancellationToken)
        {
            _context.Set<Book>().Add(book);
            return Task.CompletedTask;
        }
    }
}