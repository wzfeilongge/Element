using Element.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Data.EntityFrameworkCores.CodeFirst
{
    public class RoleMap : IEntityTypeConfiguration<RoleMannage>
    {
        public void Configure(EntityTypeBuilder<RoleMannage> builder)
        {
            builder.Property(c => c.Id).HasColumnName("Id");
            builder.Property(c => c.RoleName).HasColumnType("varchar(10)");
            builder.Property(c => c.CreateTime).HasColumnType("datetime");
        }
    }
}
