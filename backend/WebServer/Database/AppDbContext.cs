using LangLearner.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LangLearner.Database
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Language> Languages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }

        //public DbSet<CourseUser> CourseUsers { get; set; }

        //public DbSet<CourseAvailableLanguages> CourseAvailableLanguages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // CourseAvailableLanguages            
            //modelBuilder.Entity<CourseAvailableLanguages>()
            //    .HasKey(cl => new { cl.CourseId, cl.LanguageName });

            //modelBuilder.Entity<CourseAvailableLanguages>()
            //    .HasOne(cl => cl.Course)
            //    .WithMany(c => c.CourseAvailableLanguages)
            //    .HasForeignKey(cl => cl.CourseId);

            //modelBuilder.Entity<CourseAvailableLanguages>()
            //    .HasOne(cl => cl.Language)
            //    .WithMany(l => l.CourseAvailableLanguages)
            //    .HasForeignKey(cl => cl.LanguageName);

            // CourseUser
            //modelBuilder.Entity<CourseUser>()
            //    .HasKey(cu => new { cu.EnrolledCoursesId, cu.EnrolledUsersId});

            //modelBuilder.Entity<CourseUser>()
            //    .HasOne(cu => cu.Course)
            //    .WithMany(c => c.CourseUsers)
            //    .HasForeignKey(cu => cu.EnrolledCoursesId);

            //modelBuilder.Entity<CourseUser>()
            //    .HasOne(cu => cu.User)
            //    .WithMany(u => u.CourseUsers)
            //    .HasForeignKey(cu => cu.EnrolledUsersId);

            // Language
            modelBuilder.Entity<Language>()
                .HasKey(l => l.Name);

            modelBuilder.Entity<Language>()
                .HasIndex(l => l.Code)
                .IsUnique();
            
            // User
            modelBuilder.Entity<User>()
                .HasOne(u => u.NativeLanguage)
                .WithMany(l => l.NativeLanguageUsers)
                .HasForeignKey(u => u.NativeLanguageName)
                .HasPrincipalKey(l => l.Name)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<User>()
                .HasOne(u => u.AppLanguage)
                .WithMany(l => l.AppLanguageUsers)
                .HasForeignKey(u => u.AppLanguageName)
                .HasPrincipalKey(l => l.Name)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasMany(u => u.EnrolledCourses)
                .WithMany(c => c.EnrolledUsers)
                .UsingEntity(j => j.ToTable("UserCourse"));

            // Course
            modelBuilder.Entity<Course>()
                .HasOne(c => c.TargetLanguage)
                .WithMany(l => l.CoursesWithTargetLanguage)
                .HasForeignKey(c => c.TargetLanguageName)
                .HasPrincipalKey(l => l.Name)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Creator)
                .WithMany(u => u.CreatedCourses)
                .HasForeignKey(c => c.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.AvailableLanguages)
                .WithMany(l => l.CoursesWithAvailableLanguage)
                .UsingEntity(j => j.ToTable("CourseAvailableLanguages"));

            //modelBuilder.Entity<Course>()
            //    .HasMany(c => c.EnrolledUser)
            //    .WithMany(u => u.EnrolledCourses)
            //    .UsingEntity(j => j.ToTable("CourseUser"));

            //modelBuilder.Entity<Course>()
            //    .HasOne(c => c.Creator)
            //.WithMany(u => u.CreatedCourses)
            //.HasForeignKey(c => c.CreatorId)
            //.OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Course>()
            //    .HasOne(c => c.Creator)
            //    .WithMany(u => u.)

            //modelBuilder.Entity<Language>()
            //    .Property(l => l.Name)
            //    .HasMaxLength(100);
        }
    }
}
