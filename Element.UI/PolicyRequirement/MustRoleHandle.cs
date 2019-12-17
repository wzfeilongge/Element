using Element.Domain.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Element.UI.PolicyRequirement.PolicyRole;

namespace Element.UI.PolicyRequirement
{
    public class MustRoleHandle : AuthorizationHandler<PolicyRole>
    {
        public IAuthenticationSchemeProvider _Schemes { get; set; }
        private readonly IHttpContextAccessor _accessor;
        private readonly IRoleManngeRepository _RoleManngeRepository;

        public MustRoleHandle(IAuthenticationSchemeProvider authenticationSchemeProvider, IHttpContextAccessor httpContextAccessor,
            IRoleManngeRepository roleManngeRepository
            )
        {
            _Schemes = authenticationSchemeProvider;
            _accessor = httpContextAccessor;
            _RoleManngeRepository = roleManngeRepository;


        }




        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PolicyRole requirement)
        {
          

            var data = _RoleManngeRepository.GetAll(u => u.Id != null&&u.IsTrueRold==true);
            var list = await (from item in data
                              orderby item.Id
                              select new UserPermission
                              {
                                  Policy = item.RoleName,                                                                  
                              }).ToListAsync();

            requirement.UserPermissions = list;

            var filterContext = (context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext);
            var httpContext = (context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext)?.HttpContext;
            if (httpContext == null)
            {
                httpContext = _accessor.HttpContext;
            }
            if (httpContext != null)
            {
                var questUrl = httpContext.Request.Path.Value.ToLower();
                //判断请求是否停止
                var handlers = httpContext.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
                foreach (var scheme in await _Schemes.GetRequestHandlerSchemesAsync())
                {
                    if (await handlers.GetHandlerAsync(httpContext, scheme.Name) is IAuthenticationRequestHandler handler && await handler.HandleRequestAsync())
                    {
                        context.Fail();
                        return;
                    }
                }
                //判断请求是否拥有凭据，即有没有登录
                var defaultAuthenticate = await _Schemes.GetDefaultAuthenticateSchemeAsync();
                if (defaultAuthenticate != null)
                {
                    var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
                    //result?.Principal不为空即登录成功
                    if (result?.Principal != null)
                    {
                        httpContext.User = result.Principal;
                        //权限中是否存在请求的角色类型
                        if (true)
                        {
                            // 获取当前用户的角色信息
                            var currentUserRoles = (from item in httpContext.User.Claims
                                                          where item.Type == requirement.ClaimType
                                                           && requirement.UserPermissions.Count>0                                                    
                                                    select item.Value).ToList();
                            //验证权限
                            if (currentUserRoles.Count <= 0)
                            {
                                httpContext.Response.Redirect(requirement.DeniedAction);
                                return;
                            }
                        }
                        context.Succeed(requirement);
                        return;
                    }
                    else
                    {                    
                        context.Fail();
                        return;
                    }
                }
                else
                {
                    //是登录的api请求
                    if (!questUrl.Equals(requirement.LoginPath.ToLower()))
                    {

                        context.Succeed(requirement);
                        return;
                    }
                    context.Fail();
                }

            }


            return;

        }
    }
}
