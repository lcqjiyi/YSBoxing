using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YSBoxing.Migrations
{
    public partial class addStyleDst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StyleDescription",
                table: "InputYsOrders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StyleDescription",
                table: "InputYsOrders");
        }
    }
}
