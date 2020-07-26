using System;
using System.Threading.Tasks;

namespace BookShop.Infra.Messaging.Interfaces
{
    public interface IMessageErrorHandler
    {
         Task Handle(Exception exception);
    }
}