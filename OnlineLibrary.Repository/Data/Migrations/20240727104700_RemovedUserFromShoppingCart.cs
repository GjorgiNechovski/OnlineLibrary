using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLibrary.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUserFromShoppingCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BookCartId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LibraryMemberId",
                table: "Carts");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BookCartId",
                table: "AspNetUsers",
                column: "BookCartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BookCartId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "LibraryMemberId",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BookCartId",
                table: "AspNetUsers",
                column: "BookCartId",
                unique: true);
        }
    }
}
