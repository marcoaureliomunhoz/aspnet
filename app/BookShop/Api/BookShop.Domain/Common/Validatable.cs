using Flunt.Notifications;
using Flunt.Validations;

namespace BookShop.Domain.Common
{
    public abstract class Validatable : Notifiable, IValidatable
    {
        public abstract void Validate();
    }
}