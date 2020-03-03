using System;
using System.Linq;
using System.Threading.Tasks;
using Element.Applicaion.IElementServices;
using Element.Applicaion.ViewModels;
using Element.Common.HttpComm;
using Element.Core.Notifications;
using Element.UI.PolicyRequirement;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Element.UI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IElementService _ElementService;
        private readonly DomainNotificationHandler _Notifications;
        private readonly IUserService _UserService;
        public UserController(IElementService ElementService, INotificationHandler<DomainNotification> notifications, IUserService UserService)
        {
            _ElementService = ElementService;
            _Notifications = (DomainNotificationHandler)notifications;
            _UserService = UserService;

        }

        [HttpPut("EditUser")]
        [Authorize(Permissions.Name)]
        public async Task<IActionResult> EditUser([FromBody]UserEditViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                ViewModel.Ip = Extension.GetClientUserIp(HttpContext);
                await _UserService.ChangePwd(ViewModel);
                if (!_Notifications.HasNotifications())
                {
                    return Ok(new
                    {
                        Sucess = true,
                        Message = "修改成功"
                    });
                }
            }
            return Ok(new
            {
                sucess = false,
                Message = _Notifications.GetErrorMessage()
            });
        }

        [HttpGet("GetHistory")]
        [Authorize(Permissions.Name)]
        public async Task<IActionResult> GetHistory(Guid id)
        {
            var data = await _UserService.GetAllHistory(id);
            return Ok(new
            {
                Sucess = true,
                data
            });
        }

        /// <summary>
        /// 用户名和密码登录
        /// </summary>
        /// <param name="ViewModel"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]UserLoginModel ViewModel)
        {
            await _UserService.Login(ViewModel);
            var list = _Notifications.GetNotifications();
            var sucess = Convert.ToBoolean((from item in list
                                            where item.Key == "Sucess"
                                            select item.Value.ToString())
                                            .FirstOrDefault());
            var data = (from item in list
                        where item.Key == "data"
                        select item.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(data))
            {
                data = _Notifications.GetErrorMessage();
            }
            return Ok(new
            {
                sucess,
                data
            });
        }

        /// <summary>
        /// 用户名和手机号码查询用户
        /// </summary>
        /// <returns></returns>
        [HttpGet("QueryAll")]
        [Authorize(Permissions.Name)]
        public async Task<IActionResult> QueryAll()
        {
            if (ModelState.IsValid)
            {
                var models = await _UserService.GetUserAll();
                var model = _UserService.GetDto(models);
                if (model != null)
                {
                    return Ok(new
                    {
                        sucess = true,
                        Message = "获取成功",
                        data = model
                    });
                }
            }
            return Ok(new
            {
                sucess = false,
                Message = "获取失败"
            });
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        [HttpPost("RegisterCard", Name = ("RegisterCard"))]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterCard([FromBody] RegisterViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                await _UserService.Register(userViewModel);
                if (!_Notifications.HasNotifications())
                {
                    return Ok(new
                    {
                        sucess = true,
                        Message = "注册成功"
                    });
                }
            }
            return Ok(new
            {
                Sucess = false,
                Message = _Notifications.GetErrorMessage()
            });
        }
    }
}