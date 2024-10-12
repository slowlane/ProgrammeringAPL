using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgrammeringAPL.Data;
using ProgrammeringAPL.Models;
using ProgrammeringAPL.Models.ViewModels;

namespace ProgrammeringAPL.Controllers
{
    // Konstruktor för att initiera databaskontexten och miljön för webbhosting
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context; // Används för att hantera databasoperationer
        private readonly IWebHostEnvironment _webHostEnvironment; // Används för att hantera filer i webbmappen (wwwroot), t.ex. uppladdningar

        public ProjectsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Projects Hämtar alla projekt från databasen och inkluderar galleriobjekten som är relaterade till varje projekt.
        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects
                .Include(p => p.Gallery) // Inkluderar galleribilder relaterade till projektet, detta är för Index-sidan
                .ToListAsync();

            return View(projects);

        }

        // GET: Projects/Details/5 Denna metod tar emot ett projekt-ID och hämtar det projektet från databasen, inklusive galleriobjekten.
        public async Task<IActionResult> Details(int? id)
        {
            // Kontrollerar att ett ID skickas med
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Gallery) // Inkluderar galleri för att visa bilder
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound(); // Kontrollerar om projektet existerar
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {

            var viewModel = new ProjectViewModel();
            // Initierar tomma listor för teknologier, taggar och galleri så användaren kan fylla i dessa

            viewModel.Technologies.Add(new TechnologyViewModel());
            viewModel.Tags.Add(new TagViewModel());
            viewModel.Gallery.Add(new GalleryImageViewModel());
            return View(viewModel);
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectViewModel viewModel) 
        {
            if (ModelState.IsValid) // Kontrollerar att alla inmatningar är giltiga
            {

                var project = new Project
                {
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    ProjectUrl = viewModel.ProjectUrl,
                    RepositoryUrl = viewModel.RepositoryUrl,
                    DemoUrl = viewModel.DemoUrl,
                    CreatedDate = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    Technologies = viewModel.Technologies
                        .Where(t => !string.IsNullOrWhiteSpace(t.Name))
                        .Select(t => new Technology { Name = t.Name })
                        .ToList(), // Skapar lista av teknologier från ViewModel
                    Tags = viewModel.Tags
                        .Where(t => !string.IsNullOrWhiteSpace(t.Name))
                        .Select(t => new Tag { Name = t.Name })
                        .ToList(),
                    Gallery = new List<GalleryImage>()
                };

                // Hanterar bilduppladdning om det finns några bilder
                if (viewModel.Gallery != null && viewModel.Gallery.Count > 0)
                {

                    foreach (var galleryVM in viewModel.Gallery)
                    {
                        if (!IsValidImage(galleryVM.ImageFile)) // Validerar att det är en tillåten filtyp
                        {
                            ModelState.AddModelError("Gallery", "Invalid image file.");
                            return View(viewModel);
                        }
                        if (galleryVM.ImageFile.Length > 2 * 1024 * 1024) // Max filstorlek 2 MB
                        {
                            ModelState.AddModelError("Gallery", "File size should be less than 2 MB.");
                            return View(viewModel);
                        }

                        var imagePath = await SaveImageAsync(galleryVM.ImageFile); // Sparar bilden

                        var galleryImage = new GalleryImage
                        {
                            ImagePath = imagePath,
                            Caption = galleryVM.Caption
                        };

                        project.Gallery.Add(galleryImage); // Lägger till sparade bilden i projektets galleri
                    }
                }


                _context.Add(project);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index)); // Redirectar till Index-sidan efter skapande
            }


            return View(viewModel);

        }

        //Sparar en bild till serverns uppladdningsmapp.
        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            // Skapa sökvägen till uppladdningsmappen
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsFolder); // Säkerställ att mappen finns, skapa den annars
            // Skapa ett unikt filnamn för att undvika namnkonflikter
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Spara bilden i mappen
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream); // Kopiera filens innehåll till den nya platsen på servern
            }

            // Returnera sökvägen som behövs för att visa bilden
            return "/uploads/" + uniqueFileName;
        }
        //Validerar om en fil är av tillåten typ.
        private bool IsValidImage(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return !string.IsNullOrEmpty(extension) && allowedExtensions.Contains(extension);
        }

        // GET: Projects/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            // Mappning av data från databasen till en edit viewmodel
            var viewModel = new ProjectEditViewModel
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                ProjectUrl = project.ProjectUrl,
                RepositoryUrl = project.RepositoryUrl,
                DemoUrl = project.DemoUrl,
                CreatedDate = project.CreatedDate,
                LastUpdated = project.LastUpdated
                // Mappa andra fält eventuellt
            };

            return View(viewModel);
        }


        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProjectEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid) // Kontrollerar att alla inmatningar är giltiga
            {
                try
                {
                    var project = await _context.Projects.FindAsync(id);
                    if (project == null)
                    {
                        return NotFound();
                    }

                    // Uppdatera projektets egenskaper med de ifyllda fälten
                    project.Title = viewModel.Title;
                    project.Description = viewModel.Description;
                    project.ProjectUrl = viewModel.ProjectUrl;
                    project.RepositoryUrl = viewModel.RepositoryUrl;
                    project.DemoUrl = viewModel.DemoUrl;
                    project.LastUpdated = DateTime.Now; // Uppdatera till nuvarande tid automatiskt

                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }

                //Hanterar potentiella fel med databaskonflikter.
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(viewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)); // Tillbaka till index automatiskt
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // kontrollerar om projektet finns, om det finns returnera vyn
            //var project = await _context.Projects
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            { 
                _context.Projects.Remove(project);// Tar bort projektet från databasen
            }

            //Spara ändringen i databasen och återgå till index
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
