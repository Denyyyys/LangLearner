using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LangLearner.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCourseAvailableLanguages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseAvailableLanguages_Courses_CourseAvailableLanguagesId",
                table: "CourseAvailableLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseAvailableLanguages_Languages_CourseAvailableLanguagesName",
                table: "CourseAvailableLanguages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseAvailableLanguages",
                table: "CourseAvailableLanguages");

            migrationBuilder.DropIndex(
                name: "IX_CourseAvailableLanguages_CourseAvailableLanguagesName",
                table: "CourseAvailableLanguages");

            migrationBuilder.RenameColumn(
                name: "CourseAvailableLanguagesName",
                table: "CourseAvailableLanguages",
                newName: "AvailableLanguagesName");

            migrationBuilder.RenameColumn(
                name: "CourseAvailableLanguagesId",
                table: "CourseAvailableLanguages",
                newName: "CoursesWithAvailableLanguageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseAvailableLanguages",
                table: "CourseAvailableLanguages",
                columns: new[] { "AvailableLanguagesName", "CoursesWithAvailableLanguageId" });

            migrationBuilder.CreateIndex(
                name: "IX_CourseAvailableLanguages_CoursesWithAvailableLanguageId",
                table: "CourseAvailableLanguages",
                column: "CoursesWithAvailableLanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseAvailableLanguages_Courses_CoursesWithAvailableLanguageId",
                table: "CourseAvailableLanguages",
                column: "CoursesWithAvailableLanguageId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseAvailableLanguages_Languages_AvailableLanguagesName",
                table: "CourseAvailableLanguages",
                column: "AvailableLanguagesName",
                principalTable: "Languages",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseAvailableLanguages_Courses_CoursesWithAvailableLanguageId",
                table: "CourseAvailableLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseAvailableLanguages_Languages_AvailableLanguagesName",
                table: "CourseAvailableLanguages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseAvailableLanguages",
                table: "CourseAvailableLanguages");

            migrationBuilder.DropIndex(
                name: "IX_CourseAvailableLanguages_CoursesWithAvailableLanguageId",
                table: "CourseAvailableLanguages");

            migrationBuilder.RenameColumn(
                name: "CoursesWithAvailableLanguageId",
                table: "CourseAvailableLanguages",
                newName: "CourseAvailableLanguagesId");

            migrationBuilder.RenameColumn(
                name: "AvailableLanguagesName",
                table: "CourseAvailableLanguages",
                newName: "CourseAvailableLanguagesName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseAvailableLanguages",
                table: "CourseAvailableLanguages",
                columns: new[] { "CourseAvailableLanguagesId", "CourseAvailableLanguagesName" });

            migrationBuilder.CreateIndex(
                name: "IX_CourseAvailableLanguages_CourseAvailableLanguagesName",
                table: "CourseAvailableLanguages",
                column: "CourseAvailableLanguagesName");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseAvailableLanguages_Courses_CourseAvailableLanguagesId",
                table: "CourseAvailableLanguages",
                column: "CourseAvailableLanguagesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseAvailableLanguages_Languages_CourseAvailableLanguagesName",
                table: "CourseAvailableLanguages",
                column: "CourseAvailableLanguagesName",
                principalTable: "Languages",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
