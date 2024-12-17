using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UniqueEmailAndUserName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlist_Games_GamedId",
                table: "Wishlist");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserName_Email",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.RenameColumn(
                name: "GamedId",
                table: "Wishlist",
                newName: "GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Wishlist_GamedId",
                table: "Wishlist",
                newName: "IX_Wishlist_GameId");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "RoleName",
                value: "Publisher");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlist_Games_GameId",
                table: "Wishlist",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlist_Games_GameId",
                table: "Wishlist");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserName",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "Wishlist",
                newName: "GamedId");

            migrationBuilder.RenameIndex(
                name: "IX_Wishlist_GameId",
                table: "Wishlist",
                newName: "IX_Wishlist_GamedId");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "RoleName",
                value: "Developer");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[] { 4, "Publisher" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName_Email",
                table: "Users",
                columns: new[] { "UserName", "Email" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlist_Games_GamedId",
                table: "Wishlist",
                column: "GamedId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
