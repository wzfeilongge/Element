using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Element.Applicaion.IElementServices;
using Element.Applicaion.ViewModels;
using Element.Core.Events;
using Element.Core.Notifications;
using Element.UI.JwtHelper;
using Element.UI.PolicyRequirement;
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

        private readonly IJwtInterface _IJwtInterface;

        public UserController(IElementService ElementService, INotificationHandler<DomainNotification> notifications, IUserService UserService, IJwtInterface jwtInterface)
        {
            _ElementService = ElementService;
            _Notifications = (DomainNotificationHandler)notifications;
            _UserService = UserService;
            _IJwtInterface = jwtInterface;
        }

        /// <summary>
        /// 注册一个商户
        /// </summary>
        /// <param name="ViewModel"></param>
        /// <returns></returns>
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


        /// <summary>
        /// 身份证号码注册用户
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        [HttpPost("RegisterCard", Name = ("RegisterCard"))]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterCard([FromBody] UserViewModel userViewModel)
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


        /// <summary>
        /// 用户名和手机号码查询用户
        /// </summary>
        /// <param name="ViewModel"></param>
        /// <returns></returns>
        [HttpGet("QueryById")]
        [Authorize(Permissions.Name)]
        public async Task<IActionResult> QueryById([FromBody] UserViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                var model = await _UserService.GetUserById(ViewModel);
                if (model != null)
                {
                    return Ok(new
                    {
                        sucess = true,
                        Message = "获取成功",
                        model.Name,
                        model.Phone
                    });
                }
            }
            return Ok(new
            {
                sucess = false,
                Message = "获取失败",
            });
        }

        /// <summary>
        /// 用户名和身份证号登录
        /// </summary>
        /// <param name="ViewModel"></param>
        /// <returns></returns>
        [HttpGet("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]UserViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                var model = await _UserService.GetUserById(ViewModel);
                if (model != null)
                {
                    var role = await _ElementService.GetRoleModel(model.Id);
                    TokenModelJwt t = new TokenModelJwt
                    {
                        Role = role.RoleName,
                        Uid = ((role.Id)),
                        Name = model.Name
                    };
                    var token = _IJwtInterface.IssueJwt(t);

                    return Ok(new
                    {
                        sucess = true,
                        Message = "获取成功",
                        token
                    });
                }
            }
            return Ok(new
            {
                sucess = false,
                Message = "获取成功",
                token = "error"
            });
        }


































    }
}