using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Element.Data.Migrations
{
    public partial class CodeFirstElementMannage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Merchants",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Address_City = table.Column<string>(nullable: true),
                    Address_County = table.Column<string>(nullable: true),
                    Address_Province = table.Column<string>(nullable: true),
                    Address_Street = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    MerchantIdCard = table.Column<string>(type: "varchar(18)", maxLength: 18, nullable: false),
                    MerchantName = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Phone = table.Column<string>(type: "varchar(11)", maxLength: 18, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Merchants");
        }
    }
}
