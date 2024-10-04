using LangLearner.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace LangLearner.Models.Dtos.Responses
{
    public class CourseDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string CreatorUsername { get; set; } = string.Empty;

        [Required]

        public int ReportsCount { get; set; } = 0;

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
        public bool Verified { get; set; } = false;

        [Required]
        public int LikesCount { get; set; } = 10;

        [Required]
        public int DislikesCount { get; set; } = 0;

        public IEnumerable<string> AvailableLanguages { get; set; } = Enumerable.Empty<string>();

        public int numberEnrolledUsers { get; set; } = 0;

    }
}
