﻿// <auto-generated />
using LangLearner.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LangLearner.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240729164137_ChangeUserCourseTable")]
    partial class ChangeUserCourseTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CourseLanguage", b =>
                {
                    b.Property<string>("AvailableLanguagesName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("CoursesWithAvailableLanguageId")
                        .HasColumnType("int");

                    b.HasKey("AvailableLanguagesName", "CoursesWithAvailableLanguageId");

                    b.HasIndex("CoursesWithAvailableLanguageId");

                    b.ToTable("CourseAvailableLanguages", (string)null);
                });

            modelBuilder.Entity("CourseUser", b =>
                {
                    b.Property<int>("EnrolledCoursesId")
                        .HasColumnType("int");

                    b.Property<int>("EnrolledUsersId")
                        .HasColumnType("int");

                    b.HasKey("EnrolledCoursesId", "EnrolledUsersId");

                    b.HasIndex("EnrolledUsersId");

                    b.ToTable("UserCourse", (string)null);
                });

            modelBuilder.Entity("LangLearner.Models.Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DislikesCount")
                        .HasColumnType("int");

                    b.Property<int>("LikesCount")
                        .HasColumnType("int");

                    b.Property<int>("ReportsCount")
                        .HasColumnType("int");

                    b.Property<string>("TargetLanguageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("Verified")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("TargetLanguageName");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("LangLearner.Models.Entities.Language", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("NativeName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Name");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("LangLearner.Models.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AppLanguageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NativeLanguageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("AppLanguageName");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("NativeLanguageName");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CourseLanguage", b =>
                {
                    b.HasOne("LangLearner.Models.Entities.Language", null)
                        .WithMany()
                        .HasForeignKey("AvailableLanguagesName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LangLearner.Models.Entities.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesWithAvailableLanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CourseUser", b =>
                {
                    b.HasOne("LangLearner.Models.Entities.Course", null)
                        .WithMany()
                        .HasForeignKey("EnrolledCoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LangLearner.Models.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("EnrolledUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LangLearner.Models.Entities.Course", b =>
                {
                    b.HasOne("LangLearner.Models.Entities.User", "Creator")
                        .WithMany("CreatedCourses")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LangLearner.Models.Entities.Language", "TargetLanguage")
                        .WithMany("CoursesWithTargetLanguage")
                        .HasForeignKey("TargetLanguageName")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("TargetLanguage");
                });

            modelBuilder.Entity("LangLearner.Models.Entities.User", b =>
                {
                    b.HasOne("LangLearner.Models.Entities.Language", "AppLanguage")
                        .WithMany("AppLanguageUsers")
                        .HasForeignKey("AppLanguageName")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LangLearner.Models.Entities.Language", "NativeLanguage")
                        .WithMany("NativeLanguageUsers")
                        .HasForeignKey("NativeLanguageName")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AppLanguage");

                    b.Navigation("NativeLanguage");
                });

            modelBuilder.Entity("LangLearner.Models.Entities.Language", b =>
                {
                    b.Navigation("AppLanguageUsers");

                    b.Navigation("CoursesWithTargetLanguage");

                    b.Navigation("NativeLanguageUsers");
                });

            modelBuilder.Entity("LangLearner.Models.Entities.User", b =>
                {
                    b.Navigation("CreatedCourses");
                });
#pragma warning restore 612, 618
        }
    }
}
