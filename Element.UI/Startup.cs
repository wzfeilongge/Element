﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Element.Applicaion.AutoMapper;
using Element.Applicaion.ElementServices;
using Element.Applicaion.IElementServices;
using Element.Data;
using Element.Data.EntityFrameworkCores;
using Element.Data.UnitOfWork;
using Element.Domain.Interface;
using Element.Infra.Data.UnitofWorkDB;
using Element.UI.Aop.Fiter;
using Element.UI.Extensions;
using log4net;
using log4net.Config;
using log4net.Repository;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.Options;
using NLog.Extensions.Logging;
using NLog.Web;
using Element.UI.PolicyRequirement;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Element.Common.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Element.UI.ApiAuth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using StackExchange.Profiling.Storage;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerUI;
using AutoMapper.Configuration.Conventions;

namespace Element.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Repository = LogManager.CreateRepository("Web.Api");
            XmlConfigurator.Configure(Repository, new FileInfo("log4net.config"));
        }
        public static ILoggerRepository Repository { get; set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc(o =>
            {
                o.Filters.Add(typeof(GlobalExceptionFilter)); //注入全局异常过滤
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region Swagger

            services.AddMiniProfiler(options =>
            {
                options.RouteBasePath = "/profiler";//注意这个路径要和下边 index.html 脚本配置中的一致，
                (options.Storage as MemoryCacheStorage).CacheDuration = TimeSpan.FromMinutes(10);
            }

            );



            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v0.1.0",
                    Title = "Element.API",
                    Description = "框架说明文档",
                    
                });
                var basePaths = ApplicationEnvironment.ApplicationBasePath;
                var xmlPath = Path.Combine(basePaths, "ElemntUI.Api.xml");//这个就是刚刚配置的xml文件名
                c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改


                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });
            });

            #endregion

            #region  配置跨域

            services.AddCors(c =>
            {
                c.AddPolicy("LimitRequests", policy =>
                {
                    // 支持多个域名端口，注意端口号后不要带/斜杆：比如localhost:8000/，是错的
                    // 注意，http://127.0.0.1:1818 和 http://localhost:1818 是不一样的，尽量写两个
                    policy
                    .WithOrigins("http://127.0.0.1:1818", "http://localhost:8080", "http://localhost:8021"
                    , "http://localhost:8081", "http://localhost:1818"
                    , "http://localhost:9001", "http://localhost:1090"
                    , "http://localhost:5000", "http://localhost:5001"
                    )
                    .AllowAnyHeader()//Ensures that the policy allows any header.
                    .AllowAnyMethod();
                });
            });


            #endregion

            #region 授权认证
             
            //services.AddSingleton<IJwtInterface, JwtHelpers>(); //注入jwt
            services.AddScoped<IAuthorizationHandler, MustRoleHandle>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Permissions.Name,
                       policy => policy.Requirements.Add(new PolicyRole(ClaimTypes.Role,true))
              );
            });

            #endregion

            #region 给予权限，访问API
            var audienceConfig = Configuration.GetSection("Audience");
            var symmetricKeyAsBase64 = audienceConfig["Secret"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,//还是从 appsettings.json 拿到的
                ValidateIssuer = true,
                ValidIssuer = audienceConfig["Issuer"],//发行人
                ValidateAudience = true,

                ValidAudience = audienceConfig["Audience"],//订阅人
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true
            };
            services.AddAuthentication(o =>
            {

                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = nameof(ApiResponseHandler);
                o.DefaultForbidScheme = nameof(ApiResponseHandler);
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = tokenValidationParameters;
            })
            .AddScheme<AuthenticationSchemeOptions, ApiResponseHandler>(nameof(ApiResponseHandler),o=> 
            { 
           
            
            }); 

            #endregion

            NativeInjectorBootStrapper.InitServices(services);

        }

        public  void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            app.UseStaticFiles();
            //使用NLog作为日志记录工具
            loggerFactory.AddNLog();
            //引入Nlog配置文件
            env.ConfigureNLog("NIog.config");

            #region  Token 中间件
            app.UseAuthentication();
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseSwagger();
            app.UseMiniProfiler();
            app.UseSwaggerUI(c =>
            {
               c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
               c.RoutePrefix = "";//路径配置直接访问该文件，
               c.IndexStream =() => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("ElementManage.index.html");

            });

            app.UseCors("LimitRequests");

            app.UseMvc();
        }
    }
}
