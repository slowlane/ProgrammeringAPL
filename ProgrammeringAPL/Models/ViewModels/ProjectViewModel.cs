

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ProgrammeringAPL.Models.ViewModels
{
    // ViewModel för att skapa eller visa detaljer om ett projekt
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




   
        public List<TechnologyViewModel> Technologies { get; set; } = new List<TechnologyViewModel>(); // Lista med teknologier kopplade till projektet
        public List<TagViewModel> Tags { get; set; } = new List<TagViewModel>(); // Lista med taggar kopplade till projektet
        public List<GalleryImageViewModel> Gallery { get; set; } = new List<GalleryImageViewModel>(); // Lista med bilder relaterade till projektet


        // public List<IFormFile> GalleryImages { get; set; } = new List<IFormFile>();
    }

   
    
    // ViewModel för att representera en teknologi som är kopplad till ett projekt
    public class TechnologyViewModel
    {
        public int TechnologyId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }

   
    
    
    // ViewModel för att representera en tagg som är kopplad till ett projekt
    public class TagViewModel
    {
        public int TagId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }
    }



    // ViewModel för att representera en bild i projektets galleri
    public class GalleryImageViewModel
    {
        public int GalleryImageId { get; set; }

        //[Required]
        //[StringLength(500)]
        //public string ImageUrl { get; set; }

        [StringLength(200)]
        public string Caption { get; set; }
        
        [Required(ErrorMessage = "Var god och ladda upp en bild.")]
        [DataType(DataType.Upload)]
        public IFormFile ImageFile { get; set; } // Filen som laddas upp som bild
    }
}

