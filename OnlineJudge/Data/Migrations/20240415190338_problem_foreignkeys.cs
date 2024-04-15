using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineJudge.Data.Migrations
{
    /// <inheritdoc />
    public partial class problem_foreignkeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Problem",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Problem_AspNetUsers_AuthorId",
                table: "Problem",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Problem_Contest_ContestId",
                table: "Problem",
                column: "ContestId",
                principalTable: "Contest",
                principalColumn: "Id"
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Problem",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.DropForeignKey(
                name: "FK_Problem_AspNetUsers_AuthorId",
                table: "Problem");

            migrationBuilder.DropForeignKey(
               name: "FK_Problem_Contest_ContestId",
               table: "Problem");
        }
    }
}
