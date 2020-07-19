using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop.Domain.Common;

namespace BookShop.Domain.Books.Repositories
{
    public interface IBookReadRepository
    {
         Task<IList<Book>> ListAll();
         Task<Book> GetById(int id);
    }
}