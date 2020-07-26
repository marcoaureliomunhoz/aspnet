using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace BookShop.Domain.Common.Pipelines
{
    public class ValidateCommand<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
       where TResponse : class
    {
      public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
      {
         if (request is Validatable validatable)
         {
             validatable.Validate();
             if (validatable.Invalid)
             {
                 var result = new Result<TResponse>();
                 foreach(var notification in validatable.Notifications)
                 {
                     result.AddMessage(notification.Message);
                 }
                 return result as TResponse;
             }
         }

         return await next();
      }
    }
}