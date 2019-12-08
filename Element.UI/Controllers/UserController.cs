using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Element.Applicaion.IElementServices;
using Element.Applicaion.ViewModels;
using Element.Core.Events;
using Element.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace Element.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IElementService _ElementService;

        private readonly IUserService _UserService;
        private readonly DomainNotificationHandler _Notifications;

        public UserController(IElementService ElementService, INotificationHandler<DomainNotification> notifications, IUserService UserService)
        {
            _ElementService = ElementService;
            _Notifications = (DomainNotificationHandler)notifications;
            _UserService = UserService;
        }

        [HttpPost("RegisterMerchant", Name = ("RegisterMerchant"))]
        [AllowAnonymous]
        public async Task<ActionResult> RegisterMerchant([FromBody] MerchantViewModel ViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _ElementService.ResiterMerchant(ViewModel);
                    if (!_Notifications.HasNotifications())
                    {
                        return Ok(new
                        {
                            Sucess = true,
                            Message = "注册成功"
                        });
                    }
                }
                return Ok(new
                {
                    Sucess = false,
                    Message = _Notifications.GetErrorMessage()
                }); ;
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    Sucess = false,
                    Message = e.Message.ToString()
                });
            }
        }

        [HttpPost("RegisterCard", Name = ("RegisterCard"))]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterCard([FromBody] UserViewModel userViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    await _UserService.Register(userViewModel);
                    if (!_Notifications.HasNotifications())
                    {
                        return Ok(new
                        {
                            Sucess = true,
                            Message = "注册成功"
                        });
                    }
                }
                return Ok(new
                {
                    Sucess = false,
                    Message = _Notifications.GetErrorMessage()
                }); ;

            }
            catch (Exception e)
            {

                return Ok(new
                {
                    sucess = false,
                    Message = e.Message.ToString()
                }); ;
            }
        }
    }
}