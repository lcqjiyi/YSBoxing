using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YSBoxing.Migrations
{
    public partial class Add_inputysOrder2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsInput",
                table: "InputYsOrders",
                newName: "IsGenerated");

            migrationBuilder.AddColumn<string>(
                name: "YsGoodNo",
                table: "InputYsOrders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YsGoodNo",
                table: "InputYsOrders");

            migrationBuilder.RenameColumn(
                name: "IsGenerated",
                table: "InputYsOrders",
                newName: "IsInput");
        }
    }
}
