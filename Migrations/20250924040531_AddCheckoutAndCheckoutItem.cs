using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace pos_simple.Migrations
{
    /// <inheritdoc />
    public partial class AddCheckoutAndCheckoutItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkouts_Categories_CategoryId",
                table: "Checkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Checkouts_Products_ProductId",
                table: "Checkouts");

            migrationBuilder.DropIndex(
                name: "IX_Checkouts_CategoryId",
                table: "Checkouts");

            migrationBuilder.DropIndex(
                name: "IX_Checkouts_ProductId",
                table: "Checkouts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Checkouts");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Checkouts");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Checkouts");

            migrationBuilder.CreateTable(
                name: "CheckoutItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CheckoutId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    SubTotal = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckoutItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckoutItems_Checkouts_CheckoutId",
                        column: x => x.CheckoutId,
                        principalTable: "Checkouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckoutItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutItems_CheckoutId",
                table: "CheckoutItems",
                column: "CheckoutId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutItems_ProductId",
                table: "CheckoutItems",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckoutItems");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Checkouts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Checkouts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Checkouts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Checkouts_CategoryId",
                table: "Checkouts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkouts_ProductId",
                table: "Checkouts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkouts_Categories_CategoryId",
                table: "Checkouts",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Checkouts_Products_ProductId",
                table: "Checkouts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
