using Element.Common.Common;
using Element.Core.Events;
using Element.Data.EntityFrameworkCores.CodeFirst;
using Element.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Data.EntityFrameworkCores
{
   public class BackDbcontextRepository :DbContext
    {

        public BackDbcontextRepository()
        {

        }


        public static BackDbcontextRepository Context
        {
            get
            {
                return new BackDbcontextRepository();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var sqltype = "BackSql:sqlType";
            var sqlstr = "BackSql:str";
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
                optionsBuilder.UseMySQL(sql.Item2);
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
        }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<StoredEvent> StoredEvents { get; set; }


    }
}
