using System.ComponentModel.DataAnnotations;

namespace LangLearner.Models.Entities
{
    public class Language
    {
        [Required(ErrorMessage = "language code must be present")]
        [MaxLength(4)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Key]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string NativeName { get; set; } = string.Empty;

        public virtual IEnumerable<User>? NativeLanguageUsers { get; set; } = null;
        public virtual IEnumerable<User>? AppLanguageUsers { get; set; } = null;
        public virtual IEnumerable<Course>? CoursesWithTargetLanguage { get; set; } = null;
        //public virtual IEnumerable<CourseAvailableLanguages>? CourseAvailableLanguages { get; set; }
        public virtual ICollection<Course>? CoursesWithAvailableLanguage { get; set; }

    }
}
