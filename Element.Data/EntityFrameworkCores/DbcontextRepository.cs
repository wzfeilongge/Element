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
            return;




        }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<StoredEvent> StoredEvents { get; set; }

        public DbSet<RoleMannage> RoleMannages { get; set; }
    }
}
