using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LangLearner.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserCourseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Courses",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "UserCourse",
                columns: table => new
                {
                    EnrolledCoursesId = table.Column<int>(type: "int", nullable: false),
                    EnrolledUsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourse", x => new { x.EnrolledCoursesId, x.EnrolledUsersId });
                    table.ForeignKey(
                        name: "FK_UserCourse_Courses_EnrolledCoursesId",
                        column: x => x.EnrolledCoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCourse_Users_EnrolledUsersId",
                        column: x => x.EnrolledUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCourse_EnrolledUsersId",
                table: "UserCourse",
                column: "EnrolledUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCourse");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateTable(
                name: "CourseUsers",
                columns: table => new
                {
                    EnrolledCoursesId = table.Column<int>(type: "int", nullable: false),
                    EnrolledUsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseUsers", x => new { x.EnrolledCoursesId, x.EnrolledUsersId });
                    table.ForeignKey(
                        name: "FK_CourseUsers_Courses_EnrolledCoursesId",
                        column: x => x.EnrolledCoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseUsers_Users_EnrolledUsersId",
                        column: x => x.EnrolledUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseUsers_EnrolledUsersId",
                table: "CourseUsers",
                column: "EnrolledUsersId");
        }
    }
}
