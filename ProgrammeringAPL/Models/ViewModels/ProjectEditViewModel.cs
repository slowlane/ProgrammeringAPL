using System.ComponentModel.DataAnnotations;

namespace ProgrammeringAPL.Models.ViewModels
{
    // ViewModel för att redigera ett projekt
    public class ProjectEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Project Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Project URL")]
        [Url]
        public string ProjectUrl { get; set; }

        [Display(Name = "Repository URL")]
        [Url]
        public string RepositoryUrl { get; set; }

        [Display(Name = "Demo URL")]
        [Url]
        public string DemoUrl { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Last Updated")]
        public DateTime LastUpdated { get; set; }


    }
}
