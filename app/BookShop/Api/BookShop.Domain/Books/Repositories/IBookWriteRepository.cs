using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Domain.Books.Repositories
{
    public interface IBookWriteRepository
    {
         Task Insert(Book book, CancellationToken cancellationToken);
    }
}