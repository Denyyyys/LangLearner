using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LangLearner.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCourseAvailableLanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseAvailableLanguages_Courses_CourseId",
                table: "CourseAvailableLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseAvailableLanguages_Languages_LanguageName",
                table: "CourseAvailableLanguages");

            migrationBuilder.RenameColumn(
                name: "LanguageName",
                table: "CourseAvailableLanguages",
                newName: "CourseAvailableLanguagesName");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "CourseAvailableLanguages",
                newName: "CourseAvailableLanguagesId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseAvailableLanguages_LanguageName",
                table: "CourseAvailableLanguages",
                newName: "IX_CourseAvailableLanguages_CourseAvailableLanguagesName");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseAvailableLanguages_Courses_CourseAvailableLanguagesId",
                table: "CourseAvailableLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseAvailableLanguages_Languages_CourseAvailableLanguagesName",
                table: "CourseAvailableLanguages");

            migrationBuilder.RenameColumn(
                name: "CourseAvailableLanguagesName",
                table: "CourseAvailableLanguages",
                newName: "LanguageName");

            migrationBuilder.RenameColumn(
                name: "CourseAvailableLanguagesId",
                table: "CourseAvailableLanguages",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseAvailableLanguages_CourseAvailableLanguagesName",
                table: "CourseAvailableLanguages",
                newName: "IX_CourseAvailableLanguages_LanguageName");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseAvailableLanguages_Courses_CourseId",
                table: "CourseAvailableLanguages",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseAvailableLanguages_Languages_LanguageName",
                table: "CourseAvailableLanguages",
                column: "LanguageName",
                principalTable: "Languages",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
