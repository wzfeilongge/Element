using Element.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Data.EntityFrameworkCores.CodeFirst
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(c => c.Id).HasColumnName("Id");
            builder.Property(c => c.Name).HasColumnType("varchar(10)")
                .HasMaxLength(10)
                .IsRequired(false);
            builder.Property(c => c.IdCard)                
                .HasColumnType("varchar(18)")
                .HasMaxLength(18)               
                .IsRequired(false);
            builder.Property(c => c.Phone).HasColumnType("varchar(11)")
                .HasMaxLength(18)
                .IsRequired(false);

            builder.Property(c => c.Address).HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsRequired(false);

            builder.HasAlternateKey(c => c.IdCard);

        }
    }
}
