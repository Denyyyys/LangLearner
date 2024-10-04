using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LangLearner.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNameForCourseUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseUsers_Courses_EnrolledCoursesId",
                table: "CourseUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseUsers_Users_EnrolledUsersId",
                table: "CourseUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseUsers",
                table: "CourseUsers");

            migrationBuilder.RenameTable(
                name: "CourseUsers",
                newName: "CourseUser");

            migrationBuilder.RenameIndex(
                name: "IX_CourseUsers_EnrolledUsersId",
                table: "CourseUser",
                newName: "IX_CourseUser_EnrolledUsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseUser",
                table: "CourseUser",
                columns: new[] { "EnrolledCoursesId", "EnrolledUsersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CourseUser_Courses_EnrolledCoursesId",
                table: "CourseUser",
                column: "EnrolledCoursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseUser_Users_EnrolledUsersId",
                table: "CourseUser",
                column: "EnrolledUsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseUser_Courses_EnrolledCoursesId",
                table: "CourseUser");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseUser_Users_EnrolledUsersId",
                table: "CourseUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseUser",
                table: "CourseUser");

            migrationBuilder.RenameTable(
                name: "CourseUser",
                newName: "CourseUsers");

            migrationBuilder.RenameIndex(
                name: "IX_CourseUser_EnrolledUsersId",
                table: "CourseUsers",
                newName: "IX_CourseUsers_EnrolledUsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseUsers",
                table: "CourseUsers",
                columns: new[] { "EnrolledCoursesId", "EnrolledUsersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CourseUsers_Courses_EnrolledCoursesId",
                table: "CourseUsers",
                column: "EnrolledCoursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseUsers_Users_EnrolledUsersId",
                table: "CourseUsers",
                column: "EnrolledUsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
