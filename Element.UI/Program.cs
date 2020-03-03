using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Element.Common.Common;
using Element.Common.SeedData;
using Element.Core.ObjectCore;
using Element.Domain.Interface;
using Element.Domain.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Element.UI
{
    public class Program
    {
      //  private  static IUserRepository _UserRepository { get; set; }

    //    private  static IRoleManngeRepository _RoleManngeRepository { get; set; }
        public Program()
        {

        }
        public   static void Main(string[] args)
        {

            var host = CreateWebHostBuilder(args).Build();


            using (var scope =host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var _UserRepository = services.GetRequiredService<IUserRepository>();
                var _RoleMannngeRepositiory = services.GetRequiredService<IRoleManngeRepository>();
                string[] str = new string[] {
                 "IsSeedDefaultData"
            };

                if (Appsettings.app(str).ObjToBool())
                {
                    var flag = SeedData(_RoleMannngeRepositiory, _UserRepository).Result;
                    var result = flag == true ? "成功" : "失败,数据已存在";
                         Console.WriteLine($"配置种子数据{result}");
                }
            }

       

            
            host.Run();

            
           

        }

        public  static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
           return WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
        
           

        }
    
       public async static Task<bool> SeedData(IRoleManngeRepository _RoleMannageRepository, IUserRepository _UserRepository)
        {
            User user1 = new User(new Guid("9af7f46a-ea52-4aa3-b8c3-9fd484c2af12"), "110101199003076894", "test", "北京市东城区", "13666666666", Encrypt.EncryptPassword("123456"), "lbfeilongge@163.com");
            if (await _UserRepository.GetModelAsync(x => x.Id.Equals(user1.Id)) != null)
            {
                return false;
            }
            RoleMannage roleMannage = new RoleMannage(new Guid("9af7f46a-ea52-4aa3-b8c3-9fd484c2af12"), "Permission", true);
            await _UserRepository.AddModel(user1);
            await _RoleMannageRepository.AddModel(roleMannage);
            return true;

        }
    
    
    
    
    }
}
