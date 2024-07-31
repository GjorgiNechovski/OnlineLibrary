using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLibrary.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBookInCartModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Carts_BookCartId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_BookCartId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookCartId",
                table: "Books");

            migrationBuilder.CreateTable(
                name: "BooksInCarts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    BookCartId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksInCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BooksInCarts_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BooksInCarts_Carts_BookCartId",
                        column: x => x.BookCartId,
                        principalTable: "Carts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BooksInCarts_BookCartId",
                table: "BooksInCarts",
                column: "BookCartId");

            migrationBuilder.CreateIndex(
                name: "IX_BooksInCarts_BookId",
                table: "BooksInCarts",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BooksInCarts");

            migrationBuilder.AddColumn<Guid>(
                name: "BookCartId",
                table: "Books",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookCartId",
                table: "Books",
                column: "BookCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Carts_BookCartId",
                table: "Books",
                column: "BookCartId",
                principalTable: "Carts",
                principalColumn: "Id");
        }
    }
}
