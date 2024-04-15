using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineJudge.Data.Migrations
{
    /// <inheritdoc />
    public partial class test_foreign3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contest_AspNetUsers_ApplicationUserId",
                table: "Contest");

            migrationBuilder.AddForeignKey(
                name: "FK_Contest_AspNetUsers_AuthorId",
                table: "Contest",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.DropIndex(
                name: "IX_Contest_ApplicationUserId",
                table: "Contest");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Contest");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Contest",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Contest_ApplicationUserId",
                table: "Contest",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contest_AspNetUsers_ApplicationUserId",
                table: "Contest",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropForeignKey(
                name: "FK_Contest_AspNetUsers_AuthorId",
                table: "Contest");
        }
    }
}
