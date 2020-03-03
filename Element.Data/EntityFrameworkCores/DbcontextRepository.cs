using Element.Common.Common;
using Element.Common.SeedData;
using Element.Core.Events;
using Element.Data.EntityFrameworkCores.CodeFirst;
using Element.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Data.EntityFrameworkCores
{
    public class DbcontextRepository : DbContext
    {

        public DbcontextRepository()
        {

        }
        public static DbcontextRepository Context
        {
            get
            {
                return new DbcontextRepository();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var sqltype = "Sql:sqlType";
            var sqlstr = "Sql:str";
            var sql = JsonHelper.GetDbConnection(sqltype, sqlstr);
            if (sql.Item1 == "1")
            {
                optionsBuilder.UseSqlServer(sql.Item2);
            }
            else if (sql.Item1 == "2")
            {
                optionsBuilder.UseOracle(sql.Item2);
            }
            else if (sql.Item1 == "3")
            {
                optionsBuilder.UseMySql(sql.Item2);
            }
            else if (sql.Item1 == "4")
            {
                optionsBuilder.UseSqlite(sql.Item2);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MerchantMap());
            base.OnModelCreating(modelBuilder);

            //User user1 = new User(new Guid("9af7f46a-ea52-4aa3-b8c3-9fd484c2af12"), "110101199003076894", "test", "北京市东城区", "13666666666", Encrypt.EncryptPassword("123456"), "lbfeilongge@163.com");
            //modelBuilder.Entity<User>().HasData(user1);
            //RoleMannage roleMannage = new RoleMannage(new Guid("9af7f46a-ea52-4aa3-b8c3-9fd484c2af12"), "Permission", true);
            //modelBuilder.Entity<RoleMannage>().HasData(roleMannage);
            return;




        }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<StoredEvent> StoredEvents { get; set; }

        public DbSet<RoleMannage> RoleMannages { get; set; }
    }
}
