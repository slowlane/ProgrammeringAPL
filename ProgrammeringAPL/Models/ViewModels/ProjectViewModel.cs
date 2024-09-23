
// File: ViewModels/ProjectViewModel.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http; // If handling file uploads

namespace ProgrammeringAPL.Models.ViewModels
{
    public class ProjectViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(2000)]
        public string Description { get; set; }


        [Url]
        [Display(Name = "Project URL")]
        public string? ProjectUrl { get; set; }

        [StringLength(500)]
        [Display(Name = "Repository URL")]
        public string? RepositoryUrl { get; set; }

        [StringLength(500)]
        [Display(Name = "Demo URL")]
        public string? DemoUrl { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Last Updated")]
        public DateTime LastUpdated { get; set; }




        // Related Entities
        public List<TechnologyViewModel> Technologies { get; set; } = new List<TechnologyViewModel>();
        public List<TagViewModel> Tags { get; set; } = new List<TagViewModel>();
        public List<GalleryImageViewModel> Gallery { get; set; } = new List<GalleryImageViewModel>();

        // Optional: For file uploads in Gallery Images
        // public List<IFormFile> GalleryImages { get; set; } = new List<IFormFile>();
    }

    public class TechnologyViewModel
    {
        public int TechnologyId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }

    public class TagViewModel
    {
        public int TagId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }
    }

    public class GalleryImageViewModel
    {
        public int GalleryImageId { get; set; }

        //[Required]
        //[StringLength(500)]
        //public string ImageUrl { get; set; }

        [StringLength(200)]
        public string Caption { get; set; }
        
        [Required(ErrorMessage = "Please upload an image.")]
        [DataType(DataType.Upload)]
        public IFormFile ImageFile { get; set; }
    }
}

