using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgrammeringAPL.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(2000)]
        public string Description { get; set; }

        [StringLength(500)]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Url]
        [Display(Name = "Project URL")]
        public string ProjectUrl { get; set; }

        [StringLength(500)]
        [Display(Name = "Repository URL")]
        public string RepositoryUrl { get; set; }

        [StringLength(500)]
        [Display(Name = "Demo URL")]
        public string DemoUrl { get; set; }

        public List<Technology> Technologies { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Last Updated")]
        public DateTime? LastUpdated { get; set; }

        [StringLength(100)]
        public string Category { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public List<Tag> Tags { get; set; }

        [StringLength(100)]
        public string Client { get; set; }

        public List<GalleryImage> Gallery { get; set; }
    }

    public class Technology
    {
        [Key]
        public int TechnologyId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        // Navigation property
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }

    public class Tag
    {
        [Key]
        public int TagId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        // Navigation property
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }

    public class GalleryImage
    {
        [Key]
        public int GalleryImageId { get; set; }

        [Required]
        [StringLength(500)]
        public string ImageUrl { get; set; }

        [StringLength(200)]
        public string Caption { get; set; }

        // Navigation property
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
