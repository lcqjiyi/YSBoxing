using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace YSBoxing.Migrations
{
    public partial class Add_inputysOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InputYsOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressCode = table.Column<string>(nullable: true),
                    AddressName = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    CustomerId = table.Column<int>(nullable: false),
                    IsInput = table.Column<bool>(nullable: false),
                    OrderGroupId = table.Column<int>(nullable: false),
                    Qty1 = table.Column<int>(nullable: false),
                    Qty2 = table.Column<int>(nullable: false),
                    Qty3 = table.Column<int>(nullable: false),
                    Qty4 = table.Column<int>(nullable: false),
                    Style = table.Column<string>(nullable: true),
                    YsOrderNo = table.Column<string>(nullable: true),
                    qty10 = table.Column<int>(nullable: false),
                    qty5 = table.Column<int>(nullable: false),
                    qty6 = table.Column<int>(nullable: false),
                    qty7 = table.Column<int>(nullable: false),
                    qty8 = table.Column<int>(nullable: false),
                    qty9 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputYsOrders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InputYsOrders");
        }
    }
}
