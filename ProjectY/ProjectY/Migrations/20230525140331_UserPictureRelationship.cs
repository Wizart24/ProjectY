using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectY.Migrations
{
    /// <inheritdoc />
    public partial class UserPictureRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Pictures",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_UserId",
                table: "Pictures",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Users_UserId",
                table: "Pictures",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Users_UserId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_UserId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Pictures");
        }
    }
}
