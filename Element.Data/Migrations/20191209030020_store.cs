using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Element.Data.Migrations
{
    public partial class store : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name:"EventStore", columns: table => new
            {
                Id = table.Column<Guid>(nullable: false),
                
            });


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
