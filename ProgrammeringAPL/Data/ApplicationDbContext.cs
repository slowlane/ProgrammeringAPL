using ProgrammeringAPL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProgrammeringAPL.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ProgrammeringAPL.Models.ToDoItem> ToDoItem { get; set; } = default!;
        public DbSet<Project> Projects { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<GalleryImage> GalleryImages { get; set; }

        public DbSet<Book> Books { get; set; }
        //public DbSet<ProgrammeringAPL.Models.ContactFormModel> ContactFormModel { get; set; }
    }
}
