using System.ComponentModel.DataAnnotations;

namespace LangLearner.Models.Entities
{
    public class CourseAvailableLanguages
    {
        [Required]
        public int CourseId { get; set; }

        [Required]
        public string LanguageName { get; set; } = string.Empty;

        public virtual Course? Course { get; set; }
        public virtual Language? Language { get; set; }
        
    }
}
