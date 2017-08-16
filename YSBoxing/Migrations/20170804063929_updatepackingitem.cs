using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YSBoxing.Migrations
{
    public partial class updatepackingitem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Qty2",
                table: "PackingItems",
                newName: "MaxQtyBig");

            migrationBuilder.RenameColumn(
                name: "Qty1",
                table: "PackingItems",
                newName: "MaxQty");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxQtyBig",
                table: "PackingItems",
                newName: "Qty2");

            migrationBuilder.RenameColumn(
                name: "MaxQty",
                table: "PackingItems",
                newName: "Qty1");
        }
    }
}
