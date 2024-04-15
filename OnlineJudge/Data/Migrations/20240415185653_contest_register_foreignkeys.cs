using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineJudge.Data.Migrations
{
    /// <inheritdoc />
    public partial class contest_register_foreignkeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ContestRegister",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_ContestRegiseter_AspNetUsers_userId",
                table: "ContestRegister",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContestRegister_Contest_userId",
                table: "ContestRegister",
                column: "ContestId",
                principalTable: "Contest",
                principalColumn: "Id"
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ContestRegister",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.DropForeignKey(
                name: "FK_ContestRegiseter_AspNetUsers_userId",
                table: "ContestRegister");

            migrationBuilder.DropForeignKey(
               name: "FK_ContestRegister_Contest_userId",
               table: "ContestRegister");
        }
    }
}
