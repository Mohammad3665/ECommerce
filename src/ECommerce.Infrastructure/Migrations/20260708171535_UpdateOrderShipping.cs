using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderShipping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OrderId",
                table: "OrderShippingInfos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_OrderShippingInfos_OrderId",
                table: "OrderShippingInfos",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderShippingInfos_Orders_OrderId",
                table: "OrderShippingInfos",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderShippingInfos_Orders_OrderId",
                table: "OrderShippingInfos");

            migrationBuilder.DropIndex(
                name: "IX_OrderShippingInfos_OrderId",
                table: "OrderShippingInfos");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderShippingInfos");
        }
    }
}
