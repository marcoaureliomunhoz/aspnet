using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Domain.Books;
using BookShop.Domain.Books.Repositories;
using BookShop.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Infra.Data.Repositories
{
    public class BookReadRepository : IBookReadRepository
    {
        private readonly BookShopDbContext _context;

        public BookReadRepository(BookShopDbContext context)
        {
            _context = context;
        }

        public async Task<Book> GetById(int id)
        {
            return await _context
                .Set<Book>()
                .FirstOrDefaultAsync(x => x.BookId == id);
        }

        public async Task<IList<Book>> ListAll()
        {
            return await _context
                .Set<Book>()
                .ToListAsync();
        }
    }
}