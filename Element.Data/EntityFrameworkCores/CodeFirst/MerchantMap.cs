using Element.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Element.Data.EntityFrameworkCores.CodeFirst
{
    public class MerchantMap : IEntityTypeConfiguration<Merchant>
    {
        public void Configure(EntityTypeBuilder<Merchant> builder)
        {
            builder.Property(c => c.Id).IsRequired();
            builder.Property(c => c.MerchantName).HasColumnType("varchar(10)")
                .HasMaxLength(10)
                .IsRequired();
            builder.Property(c => c.MerchantIdCard).HasColumnType("varchar(18)")
                .HasMaxLength(18)
                .IsRequired();
            builder.Property(c => c.Phone).HasColumnType("varchar(11)")
                .HasMaxLength(18)
                .IsRequired();
            builder.Property(c => c.BirthDate).HasColumnType("DateTime")
                .IsRequired();
            builder.Property(c => c.Password).HasColumnType("varchar(50)")
                .IsRequired();
            builder.OwnsOne(p=>p.Address);           
        }
    }
}
