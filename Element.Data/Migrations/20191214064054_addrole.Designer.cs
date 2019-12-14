﻿// <auto-generated />
using System;
using Element.Data.EntityFrameworkCores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Element.Data.Migrations
{
    [DbContext(typeof(DbcontextRepository))]
    [Migration("20191214064054_addrole")]
    partial class addrole
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Element.Core.Events.StoredEvent", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<Guid>("AggregateId");

                    b.Property<string>("Data");

                    b.Property<string>("MessageType");

                    b.Property<DateTime>("Timestamp");

                    b.Property<string>("User");

                    b.HasKey("Id");

                    b.ToTable("StoredEvents");
                });

            modelBuilder.Entity("Element.Domain.Models.Merchant", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("Id");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("DateTime");

                    b.Property<string>("MerchantIdCard")
                        .IsRequired()
                        .HasColumnType("varchar(18)")
                        .HasMaxLength(18);

                    b.Property<string>("MerchantName")
                        .IsRequired()
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("varchar(11)")
                        .HasMaxLength(18);

                    b.HasKey("Id");

                    b.ToTable("Merchants");
                });

            modelBuilder.Entity("Element.Domain.Models.RoleMannage", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<bool>("IsTrueRold");

                    b.Property<string>("RoleName");

                    b.HasKey("Id");

                    b.ToTable("RoleMannages");
                });

            modelBuilder.Entity("Element.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("Address");

                    b.Property<string>("IdCard");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Element.Domain.Models.Merchant", b =>
                {
                    b.OwnsOne("Element.Domain.Models.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("MerchantId");

                            b1.Property<string>("City");

                            b1.Property<string>("County");

                            b1.Property<string>("Province");

                            b1.Property<string>("Street");

                            b1.HasKey("MerchantId");

                            b1.ToTable("Merchants");

                            b1.HasOne("Element.Domain.Models.Merchant")
                                .WithOne("Address")
                                .HasForeignKey("Element.Domain.Models.Address", "MerchantId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
