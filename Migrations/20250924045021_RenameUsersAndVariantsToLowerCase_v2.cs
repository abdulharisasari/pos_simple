using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pos_simple.Migrations
{
    /// <inheritdoc />
    public partial class RenameUsersAndVariantsToLowerCase_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_checkoutitems_checkouts_checkoutid",
                table: "checkoutitems");

            migrationBuilder.DropForeignKey(
                name: "FK_checkoutitems_products_productid",
                table: "checkoutitems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariants",
                table: "ProductVariants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_checkoutitems",
                table: "checkoutitems");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "ProductVariants",
                newName: "product_variants");

            migrationBuilder.RenameTable(
                name: "checkoutitems",
                newName: "checkout_items");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "users",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "product_variants",
                newName: "productid");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "product_variants",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "product_variants",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_checkoutitems_productid",
                table: "checkout_items",
                newName: "IX_checkout_items_productid");

            migrationBuilder.RenameIndex(
                name: "IX_checkoutitems_checkoutid",
                table: "checkout_items",
                newName: "IX_checkout_items_checkoutid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_product_variants",
                table: "product_variants",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_checkout_items",
                table: "checkout_items",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_checkout_items_checkouts_checkoutid",
                table: "checkout_items",
                column: "checkoutid",
                principalTable: "checkouts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_checkout_items_products_productid",
                table: "checkout_items",
                column: "productid",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_checkout_items_checkouts_checkoutid",
                table: "checkout_items");

            migrationBuilder.DropForeignKey(
                name: "FK_checkout_items_products_productid",
                table: "checkout_items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_product_variants",
                table: "product_variants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_checkout_items",
                table: "checkout_items");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "product_variants",
                newName: "ProductVariants");

            migrationBuilder.RenameTable(
                name: "checkout_items",
                newName: "checkoutitems");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "productid",
                table: "ProductVariants",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "ProductVariants",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ProductVariants",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_checkout_items_productid",
                table: "checkoutitems",
                newName: "IX_checkoutitems_productid");

            migrationBuilder.RenameIndex(
                name: "IX_checkout_items_checkoutid",
                table: "checkoutitems",
                newName: "IX_checkoutitems_checkoutid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariants",
                table: "ProductVariants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_checkoutitems",
                table: "checkoutitems",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_checkoutitems_checkouts_checkoutid",
                table: "checkoutitems",
                column: "checkoutid",
                principalTable: "checkouts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_checkoutitems_products_productid",
                table: "checkoutitems",
                column: "productid",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
