using AutoMapper;
using Element.Applicaion.AutoMapper;
using Element.Applicaion.ElementServices;
using Element.Applicaion.EventSource;
using Element.Applicaion.IElementServices;
using Element.Common;
using Element.Core.Bus;
using Element.Core.Events;
using Element.Core.Notifications;
using Element.Data;
using Element.Data.Bus;
using Element.Data.EntityFrameworkCores;
using Element.Data.UnitOfWork;
using Element.Domain.CommandHandler;
using Element.Domain.Commands;
using Element.Domain.EventHandler;
using Element.Domain.Events.UserEvent;
using Element.Domain.Interface;
using Element.Infra.Data;
using Element.Infra.Data.UnitofWorkDB;
using Element.UI.Log;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Element.UI.Extensions
{
    public class NativeInjectorBootStrapper
    {
        public static void InitServices(IServiceCollection services)
        {
            
            services.AddScoped<DbcontextRepository>();  //主

            services.AddScoped<BackDbcontextRepository>(); //从

            services.AddAutoMapper(typeof(AutoMapperConfig));

            services.AddSingleton<IJwtInterface, JwtHelpers>();

            AutoMapperConfig.RegisterMappings();

            services.AddMediatR(typeof(Startup));

            services.AddScoped<IElementService, ElementService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #region 主数据库
            services.AddScoped<IMerchantRepository, MerchantRepository>(); //主

            services.AddScoped<IUserRepository, UserRepositiory>();//主

            services.AddScoped<IEventStoreRepository, EventStoreRepository>(); //主

            services.AddScoped<IRoleManngeRepository, RoleMannageRepository>();

            #endregion

            services.AddSingleton<ILoggerHelper, LogHelper>();
          
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // 领域通知
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            services.AddScoped<IEventStoreService, EventStoreService>();

            //领域事件
            services.AddScoped<INotificationHandler<MerchantRegisteredEvent>, RegisterEventHandler>();

            services.AddScoped<INotificationHandler<UserRegisterEvent>, UserEventHandler>();

            services.AddScoped<INotificationHandler<ChangePwdEvent>, UserEventHandler>();

           


            //领域命令
            services.AddScoped<IRequestHandler<MerchantCommands, Unit>, MerchantCommandsHandlers>();

            services.AddScoped<IRequestHandler<UserCommand, Unit>, UserCommandHandlers>();

            services.AddScoped<IRequestHandler<UserChangePwdCommand, Unit>, UserCommandHandlers>();

            //UserLoginCommand   没有领域事件
            services.AddScoped<IRequestHandler<UserLoginCommand,Unit>,UserCommandHandlers>();

            services.AddScoped<IMediatorHandler, InMemoryBus>();

          

        }
    }
}
