using Element.Core.Events;
using Element.Domain.Events.UserEvent;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Element.Domain.EventHandler
{
    public class UserEventHandler : INotificationHandler<UserRegisterEvent>
    {
        

        public UserEventHandler()
        {

        }

        public Task Handle(UserRegisterEvent notification, CancellationToken cancellationToken)
        {
           Console.WriteLine("注册成功");
           return Task.CompletedTask;      
        }
    }
}
