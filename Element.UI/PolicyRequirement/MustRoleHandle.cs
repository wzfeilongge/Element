using Element.Domain.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Element.UI.PolicyRequirement.PolicyRole;

namespace Element.UI.PolicyRequirement
{
    public class MustRoleHandle : AuthorizationHandler<PolicyRole>
    {
        public IAuthenticationSchemeProvider Schemes;
        private readonly IHttpContextAccessor _Accessor;
        private readonly IRoleManngeRepository _RoleManngeRepository;

        public MustRoleHandle(IAuthenticationSchemeProvider authenticationSchemeProvider, IHttpContextAccessor httpContextAccessor,
            IRoleManngeRepository roleManngeRepository
            )
        {
            Schemes = authenticationSchemeProvider;
            _Accessor = httpContextAccessor;
            _RoleManngeRepository = roleManngeRepository;
        }




        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PolicyRole requirement)
        {
            var data = _RoleManngeRepository.GetAll(u => u.Id != null && u.IsTrueRold == requirement.Istrue);
            var list = await (from item in data
                              orderby item.Id
                              select new UserPermission
                              {
                                  Policy = item.RoleName,
                                  Id = item.Id,
                                  IsEnabled = item.IsTrueRold
                              }).ToListAsync();

            requirement.UserPermissions = list;
            var filterContext = (context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext);
            var httpContext = (context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext)?.HttpContext;
            if (httpContext == null)
            {
                httpContext = _Accessor.HttpContext;
            }
            if (httpContext != null)
            {
                var questUrl = httpContext.Request.Path.Value.ToLower();
                //判断请求是否停止
                var handlers = httpContext.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
                foreach (var scheme in await Schemes.GetRequestHandlerSchemesAsync())
                {
                    if (await handlers.GetHandlerAsync(httpContext, scheme.Name) is IAuthenticationRequestHandler handler && await handler.HandleRequestAsync())
                    {
                        context.Fail();
                        return;
                    }
                }
                var defaultAuthenticate = await Schemes.GetDefaultAuthenticateSchemeAsync();
                if (defaultAuthenticate != null)
                {
                    var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
                    if (result?.Principal != null)
                    {
                        httpContext.User = result.Principal;
                        var currentUserRoles = (from item in httpContext.User.Claims
                                                where item.Type == "jti" || item.Type == requirement.ClaimType
                                                select item.Value.ToString()).ToList();
                        if (currentUserRoles.Count < 2)
                        {
                            httpContext.Response.Redirect(requirement.DeniedAction);
                            return;
                        }
                        var userPermission = new UserPermission();
                        foreach (var role in currentUserRoles)
                        {
                            if (string.IsNullOrEmpty(userPermission.Policy))
                            {
                                var permission = list.Where(x => ((x.Id.ToString().Equals(role)))).FirstOrDefault();

                                if (permission != null)
                                {
                                    userPermission = permission;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(userPermission.Policy))
                        {
                            context.Fail();
                            return;
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
                    //if (!questUrl.Equals(requirement.LoginPath.ToLower()))
                    //{

                    //    context.Succeed(requirement);
                    //    return;
                    //}
                    context.Fail();
                }
            }
            return;
        }
    }
}
