namespace BookShop.Domain.Common
{
    public interface IEntityUnitOfWork
    {
         int Commit();
    }
}