using Element.Common.MailHelper;
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
                                   , INotificationHandler<ChangePwd>
    {
        

        public UserEventHandler()
        {

        }

        public  async Task Handle(UserRegisterEvent registerEvent, CancellationToken cancellationToken)
        {
             //Console.WriteLine("注册成功");


             await MailHelp.SendMailAsync(registerEvent.Email,
                 $"欢迎{registerEvent.Name}注册服务,来自蔡徐坤的提示",$"蔡徐坤温馨提醒,{registerEvent.Name}注册成功,请牢记您的密码","蔡徐坤");



            return ;      
        }

        public  async Task Handle(ChangePwd changeModel, CancellationToken cancellationToken)
        {
            await MailHelp.SendMailAsync(changeModel.Email,
                 $"修改密码提示,来自蔡徐坤的提示", $"蔡徐坤温馨提醒,{changeModel.Name}密码已经成功修改,请牢记您的密码", "温州蔡徐坤");
        }
    }
}
