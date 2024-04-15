using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineJudge.Data.Migrations
{
    /// <inheritdoc />
    public partial class submission_foreignkeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Submission",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Submission_AspNetUsers_UserId",
                table: "Submission",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submission_Contest_ContestId",
                table: "Submission",
                column: "ContestId",
                principalTable: "Contest",
                principalColumn: "Id"
                );

            migrationBuilder.AddForeignKey(
                name: "FK_Submission_Problem_ProblemId",
                table: "Submission",
                column: "ProblemId",
                principalTable: "Problem",
                principalColumn: "Id"
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Submission",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.DropForeignKey(
                name: "FK_Submission_AspNetUsers_UserId",
                table : "Submission"
                );

            migrationBuilder.DropForeignKey(
                name: "FK_Submission_Contest_ContestId",
                table: "Submission"
                );

            migrationBuilder.DropForeignKey(
                name: "FK_Submission_Problem_ProblemId",
                table: "Submission"
                );
        }
    }
}
