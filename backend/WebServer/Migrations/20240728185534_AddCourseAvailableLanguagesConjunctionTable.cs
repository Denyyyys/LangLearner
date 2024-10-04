using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LangLearner.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseAvailableLanguagesConjunctionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseUser");

            migrationBuilder.CreateTable(
                name: "CourseAvailableLanguages",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    LanguageName = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseAvailableLanguages", x => new { x.CourseId, x.LanguageName });
                    table.ForeignKey(
                        name: "FK_CourseAvailableLanguages_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseAvailableLanguages_Languages_LanguageName",
                        column: x => x.LanguageName,
                        principalTable: "Languages",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_CourseAvailableLanguages_LanguageName",
                table: "CourseAvailableLanguages",
                column: "LanguageName");

            migrationBuilder.CreateIndex(
                name: "IX_CourseUsers_EnrolledUsersId",
                table: "CourseUsers",
                column: "EnrolledUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseAvailableLanguages");

            migrationBuilder.DropTable(
                name: "CourseUsers");

            migrationBuilder.CreateTable(
                name: "CourseUser",
                columns: table => new
                {
                    EnrolledCoursesId = table.Column<int>(type: "int", nullable: false),
                    EnrolledUsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseUser", x => new { x.EnrolledCoursesId, x.EnrolledUsersId });
                    table.ForeignKey(
                        name: "FK_CourseUser_Courses_EnrolledCoursesId",
                        column: x => x.EnrolledCoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseUser_Users_EnrolledUsersId",
                        column: x => x.EnrolledUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseUser_EnrolledUsersId",
                table: "CourseUser",
                column: "EnrolledUsersId");
        }
    }
}
