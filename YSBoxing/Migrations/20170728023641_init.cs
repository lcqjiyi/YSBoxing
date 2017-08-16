using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace YSBoxing.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    HasMixStyle = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    GroupDescription = table.Column<string>(nullable: true),
                    GroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    JcNo = table.Column<string>(maxLength: 20, nullable: true),
                    OrderGroupId = table.Column<int>(nullable: false),
                    Style = table.Column<string>(maxLength: 20, nullable: true),
                    StyleDescription = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_OrderGroup_OrderGroupId",
                        column: x => x.OrderGroupId,
                        principalTable: "OrderGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Box = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    HasMixStyle = table.Column<bool>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    OrderInfo1 = table.Column<string>(maxLength: 20, nullable: true),
                    OrderInfo2 = table.Column<string>(maxLength: 20, nullable: true),
                    OrderInfo3 = table.Column<string>(maxLength: 20, nullable: true),
                    OrderItemName = table.Column<string>(maxLength: 20, nullable: true),
                    OrderOtherInfo = table.Column<string>(maxLength: 255, nullable: true),
                    Qty = table.Column<int>(nullable: false),
                    ShipBox = table.Column<int>(nullable: false),
                    ShipQty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItemBoxings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BoxCode = table.Column<string>(nullable: true),
                    BoxNumber = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsShip = table.Column<bool>(nullable: false),
                    OrderItemId = table.Column<int>(nullable: false),
                    ShipDate = table.Column<DateTime>(nullable: false),
                    TotalQty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemBoxings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItemBoxings_OrderItems_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItemQtys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BoxingQty = table.Column<int>(nullable: false),
                    Color = table.Column<string>(maxLength: 20, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    OrderItemId = table.Column<int>(nullable: false),
                    OtherInfo = table.Column<string>(maxLength: 255, nullable: true),
                    Qty = table.Column<int>(nullable: false),
                    ShipQty = table.Column<int>(nullable: false),
                    Size = table.Column<string>(maxLength: 20, nullable: true),
                    Style = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemQtys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItemQtys_OrderItems_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItemBoxingQtys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Color = table.Column<string>(maxLength: 20, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    OrderItemBoxingId = table.Column<int>(nullable: false),
                    Qty = table.Column<int>(nullable: false),
                    Size = table.Column<string>(maxLength: 20, nullable: true),
                    Style = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemBoxingQtys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItemBoxingQtys_OrderItemBoxings_OrderItemBoxingId",
                        column: x => x.OrderItemBoxingId,
                        principalTable: "OrderItemBoxings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderGroupId",
                table: "Order",
                column: "OrderGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemBoxings_OrderItemId",
                table: "OrderItemBoxings",
                column: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemBoxingQtys_OrderItemBoxingId",
                table: "OrderItemBoxingQtys",
                column: "OrderItemBoxingId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemQtys_OrderItemId",
                table: "OrderItemQtys",
                column: "OrderItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItemBoxingQtys");

            migrationBuilder.DropTable(
                name: "OrderItemQtys");

            migrationBuilder.DropTable(
                name: "OrderItemBoxings");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "OrderGroup");
        }
    }
}
