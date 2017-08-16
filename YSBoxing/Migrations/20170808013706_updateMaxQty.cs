using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YSBoxing.Migrations
{
    public partial class updateMaxQty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalQty",
                table: "OrderItemBoxings",
                newName: "Qty");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Qty",
                table: "OrderItemBoxings",
                newName: "TotalQty");
        }
    }
}
