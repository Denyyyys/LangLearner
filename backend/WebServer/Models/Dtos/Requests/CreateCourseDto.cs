using LangLearner.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace LangLearner.Models.Dtos.Requests
{
    public class CreateCourseDto
    {
        [Required]
        [MinLength(10)]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(5000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string TargetLanguageName { get; set; } = string.Empty;

        [Required]
        public IEnumerable<string> AvailableLanguages { get; set; } = Array.Empty<string>();
    }
}
