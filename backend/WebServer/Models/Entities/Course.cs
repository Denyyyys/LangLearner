﻿using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace LangLearner.Models.Entities
{
    public class Course
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int CreatorId { get; set; }

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
        public int LikesCount { get; set; } = 0;

        [Required]
        public int DislikesCount { get; set; } = 0;

        public virtual Language TargetLanguage { get; set; } = new Language();

        public virtual User Creator { get; set; } = new User();

        public virtual ICollection<User> EnrolledUsers { get; set; } = new Collection<User>(); // enrolled users for course

        public virtual ICollection<Language> AvailableLanguages { get; set; } = new Collection<Language>();

    }
}
