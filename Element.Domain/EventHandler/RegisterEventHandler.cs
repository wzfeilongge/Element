using Element.Core.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Element.Domain.EventHandler
{
    public class RegisterEventHandler : INotificationHandler<MerchantRegisteredEvent>
    {
        public  Task Handle(MerchantRegisteredEvent notification, CancellationToken cancellationToken)
        {

         


            return Task.CompletedTask;
        }
    }
}
