using AutoMapper;
using Element.Applicaion.AutoMapper;
using Element.Applicaion.ElementServices;
using Element.Applicaion.EventSource;
using Element.Applicaion.IElementServices;
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
using Element.Domain.Interface;
using Element.Infra.Data;
using Element.Infra.Data.UnitofWorkDB;
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
        public static void InitServices(IServiceCollection services) {


            services.AddScoped<DbcontextRepository>();

            services.AddAutoMapper(typeof(AutoMapperConfig));
            AutoMapperConfig.RegisterMappings();

            services.AddMediatR(typeof(Startup));

            services.AddScoped<IElementService, ElementService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IMerchantRepository, MerchantRepository>();

            services.AddScoped<IUserRepository, UserRepositiory>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // 领域通知
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            services.AddScoped<IEventStoreService, EventStoreService>();

            //领域事件
            services.AddScoped<INotificationHandler<MerchantRegisteredEvent>, RegisterEventHandler>();

            //领域命令
            services.AddScoped<IRequestHandler<MerchantCommands, Unit>, MerchantCommandsHandlers>();

            services.AddScoped<IRequestHandler<UserCommand, Unit>, UserCommandHandlers>();

            services.AddScoped<IMediatorHandler, InMemoryBus>();

            services.AddScoped<IEventStoreRepository, EventStoreRepository>();

        }
    }
}
