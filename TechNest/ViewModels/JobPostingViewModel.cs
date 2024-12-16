﻿using System.ComponentModel.DataAnnotations;
namespace TechNest.ViewModels
{
    public class JobPostingViewModel
    {
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        public string Location { get; set; }
    }
}
