using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pos_simple.Migrations
{
    /// <inheritdoc />
    public partial class RenameTablesAndColumnsToLowerCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckoutItems_Checkouts_CheckoutId",
                table: "CheckoutItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckoutItems_Products_ProductId",
                table: "CheckoutItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Checkouts_PaymentMethods_PaymentMethodId",
                table: "Checkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_category_id",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Checkouts",
                table: "Checkouts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckoutItems",
                table: "CheckoutItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentMethods",
                table: "PaymentMethods");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "products");

            migrationBuilder.RenameTable(
                name: "Checkouts",
                newName: "checkouts");

            migrationBuilder.RenameTable(
                name: "CheckoutItems",
                newName: "checkoutitems");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "categories");

            migrationBuilder.RenameTable(
                name: "PaymentMethods",
                newName: "payment_methods");

            migrationBuilder.RenameIndex(
                name: "IX_Products_category_id",
                table: "products",
                newName: "IX_products_category_id");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "checkouts",
                newName: "totalprice");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "checkouts",
                newName: "createdat");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "checkouts",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "StatusOrderId",
                table: "checkouts",
                newName: "status_order_id");

            migrationBuilder.RenameColumn(
                name: "PaymentMethodId",
                table: "checkouts",
                newName: "payment_method_id");

            migrationBuilder.RenameIndex(
                name: "IX_Checkouts_PaymentMethodId",
                table: "checkouts",
                newName: "IX_checkouts_payment_method_id");

            migrationBuilder.RenameColumn(
                name: "SubTotal",
                table: "checkoutitems",
                newName: "subtotal");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "checkoutitems",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "checkoutitems",
                newName: "productid");

            migrationBuilder.RenameColumn(
                name: "CheckoutId",
                table: "checkoutitems",
                newName: "checkoutid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "checkoutitems",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_CheckoutItems_ProductId",
                table: "checkoutitems",
                newName: "IX_checkoutitems_productid");

            migrationBuilder.RenameIndex(
                name: "IX_CheckoutItems_CheckoutId",
                table: "checkoutitems",
                newName: "IX_checkoutitems_checkoutid");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "categories",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "categories",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "payment_methods",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "payment_methods",
                newName: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_products",
                table: "products",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_checkouts",
                table: "checkouts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_checkoutitems",
                table: "checkoutitems",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categories",
                table: "categories",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_payment_methods",
                table: "payment_methods",
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

            migrationBuilder.AddForeignKey(
                name: "FK_checkouts_payment_methods_payment_method_id",
                table: "checkouts",
                column: "payment_method_id",
                principalTable: "payment_methods",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_category_id",
                table: "products",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_checkoutitems_checkouts_checkoutid",
                table: "checkoutitems");

            migrationBuilder.DropForeignKey(
                name: "FK_checkoutitems_products_productid",
                table: "checkoutitems");

            migrationBuilder.DropForeignKey(
                name: "FK_checkouts_payment_methods_payment_method_id",
                table: "checkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_category_id",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_products",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_checkouts",
                table: "checkouts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_checkoutitems",
                table: "checkoutitems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categories",
                table: "categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_payment_methods",
                table: "payment_methods");

            migrationBuilder.RenameTable(
                name: "products",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "checkouts",
                newName: "Checkouts");

            migrationBuilder.RenameTable(
                name: "checkoutitems",
                newName: "CheckoutItems");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "payment_methods",
                newName: "PaymentMethods");

            migrationBuilder.RenameIndex(
                name: "IX_products_category_id",
                table: "Products",
                newName: "IX_Products_category_id");

            migrationBuilder.RenameColumn(
                name: "totalprice",
                table: "Checkouts",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "createdat",
                table: "Checkouts",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Checkouts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "status_order_id",
                table: "Checkouts",
                newName: "StatusOrderId");

            migrationBuilder.RenameColumn(
                name: "payment_method_id",
                table: "Checkouts",
                newName: "PaymentMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_checkouts_payment_method_id",
                table: "Checkouts",
                newName: "IX_Checkouts_PaymentMethodId");

            migrationBuilder.RenameColumn(
                name: "subtotal",
                table: "CheckoutItems",
                newName: "SubTotal");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "CheckoutItems",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "productid",
                table: "CheckoutItems",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "checkoutid",
                table: "CheckoutItems",
                newName: "CheckoutId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CheckoutItems",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_checkoutitems_productid",
                table: "CheckoutItems",
                newName: "IX_CheckoutItems_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_checkoutitems_checkoutid",
                table: "CheckoutItems",
                newName: "IX_CheckoutItems_CheckoutId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Categories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Categories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "PaymentMethods",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "PaymentMethods",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Checkouts",
                table: "Checkouts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckoutItems",
                table: "CheckoutItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentMethods",
                table: "PaymentMethods",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckoutItems_Checkouts_CheckoutId",
                table: "CheckoutItems",
                column: "CheckoutId",
                principalTable: "Checkouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckoutItems_Products_ProductId",
                table: "CheckoutItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Checkouts_PaymentMethods_PaymentMethodId",
                table: "Checkouts",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_category_id",
                table: "Products",
                column: "category_id",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
