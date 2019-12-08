using System;
using System.Collections.Generic;
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
using Element.UI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog.Extensions.Logging;
using NLog.Web;

namespace Element.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

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

            NativeInjectorBootStrapper.InitServices(services);

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            app.UseStaticFiles();
            //使用NLog作为日志记录工具
            loggerFactory.AddNLog();
            //引入Nlog配置文件
            env.ConfigureNLog("NIog.config");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseCors("LimitRequests");

            app.UseMvc();
        }
    }
}
