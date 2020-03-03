using Element.Core.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Data.EntityFrameworkCores.CodeFirst
{
    public class EvenStorMap : IEntityTypeConfiguration<StoredEvent>
    {
        public void Configure(EntityTypeBuilder<StoredEvent> builder)
        {
            builder.Property(c => c.Id).HasColumnName("Id").IsRequired();
            builder.Property(c => c.MessageType)
                .HasColumnType("varchar(100)")
                .IsRequired();
            builder.Property(c => c.AggregateId)
                .HasColumnName("AggregateId")
                .HasColumnType("uniqueidentifier")
                .IsRequired();
            builder.Property(c => c.Timestamp)
                .HasColumnName("TimeStamp")
                .IsRequired();
            builder.Property(c => c.User)
                .HasColumnName("User")
                .HasColumnType("varchar(50)")
                .IsRequired();
            builder.Property(c => c.Data)
                .HasColumnName("Data")
                .HasMaxLength(500)
                .IsRequired();
        }
    }
}
