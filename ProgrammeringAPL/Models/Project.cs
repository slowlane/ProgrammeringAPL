using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgrammeringAPL.Models
{
    // Representerar ett projekt i systemet
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


        [Url]
        [Display(Name = "Project URL")]
        public string? ProjectUrl { get; set; } // URL till projektets webbsida

        [StringLength(500)]
        [Display(Name = "Repository URL")]
        public string? RepositoryUrl { get; set; } // URL till projektets kodlager (repository)


        [StringLength(500)]
        [Display(Name = "Demo URL")]
        public string? DemoUrl { get; set; }

        

        [DataType(DataType.Date)]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Last Updated")]
        public DateTime LastUpdated { get; set; }


        public List<Tag> Tags { get; set; } // Lista med taggar kopplade till projektet

        public List<Technology> Technologies { get; set; } // Lista med teknologier använda i projektet


        public List<GalleryImage> Gallery { get; set; } // Lista med bilder relaterade till projektet
    }

    // Representerar en teknologi använd i projektet
    public class Technology
    {
        [Key]
        public int TechnologyId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int ProjectId { get; set; } // ID för projektet som teknologin är kopplad till
        public Project Project { get; set; } // Navigation property för projektet
    }

    // Representerar en tagg kopplad till projektet
    public class Tag
    {
        [Key]
        public int TagId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        // Navigation property
        public int ProjectId { get; set; } // ID för projektet som taggen är kopplad till
        public Project Project { get; set; } // Navigation property för projektet
    }

    // Representerar en bild i projektets galleri
    public class GalleryImage
    {
        [Key]
        public int GalleryImageId { get; set; }

        [StringLength(500)]
        public string ImagePath { get; set; } // Sökvägen till bilden

        [NotMapped]
        [Required(ErrorMessage = "Please upload an image.")]
        public IFormFile ImageFile { get; set; } // Själva filen för bilden som laddas upp



        [StringLength(200)]
        public string Caption { get; set; } // Bildtext för bilden

        // Navigation property
        public int ProjectId { get; set; } // ID för projektet som bilden är kopplad till
        public Project Project { get; set; } // Navigation property för projektet
    }
}
