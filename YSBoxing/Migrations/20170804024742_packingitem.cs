using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace YSBoxing.Migrations
{
    public partial class packingitem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PackingItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Color = table.Column<string>(maxLength: 20, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Qty1 = table.Column<int>(nullable: false),
                    Qty2 = table.Column<int>(nullable: false),
                    Size = table.Column<string>(maxLength: 20, nullable: true),
                    Style = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackingItems", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackingItems");
        }
    }
}
