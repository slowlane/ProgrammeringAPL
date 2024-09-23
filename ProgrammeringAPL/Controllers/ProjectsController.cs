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
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProjectsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects
                .Include(p => p.Gallery)
                .ToListAsync();

            return View(projects);
            //return View(await _context.Projects.ToListAsync());
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Gallery)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {

            var viewModel = new ProjectViewModel();

            viewModel.Technologies.Add(new TechnologyViewModel());
            viewModel.Tags.Add(new TagViewModel());
            viewModel.Gallery.Add(new GalleryImageViewModel());
            return View(viewModel);
            //return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectViewModel viewModel)  //Project project [Bind("Id,Title,Description,ImageUrl,ProjectUrl,RepositoryUrl,DemoUrl,CreatedDate,LastUpdated,Category,Status,Client")] 
        {
            if (ModelState.IsValid)
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
                        .ToList(),
                    Tags = viewModel.Tags
                        .Where(t => !string.IsNullOrWhiteSpace(t.Name))
                        .Select(t => new Tag { Name = t.Name })
                        .ToList(),
                    Gallery = new List<GalleryImage>()
                    //Gallery = viewModel.Gallery
                    //  .Where(g => !string.IsNullOrWhiteSpace(g.ImageUrl))
                    //.Select(g => new GalleryImage { ImageUrl = g.ImageUrl, Caption = g.Caption })
                    //.ToList()
                };

                if (viewModel.Gallery != null && viewModel.Gallery.Count > 0)
                {

                    foreach (var galleryVM in viewModel.Gallery)
                    {
                        if (!IsValidImage(galleryVM.ImageFile))
                        {
                            ModelState.AddModelError("Gallery", "Invalid image file.");
                            return View(viewModel);
                        }
                        if (galleryVM.ImageFile.Length > 2 * 1024 * 1024) // 2 MB
                        {
                            ModelState.AddModelError("Gallery", "File size should be less than 2 MB.");
                            return View(viewModel);
                        }

                        var imagePath = await SaveImageAsync(galleryVM.ImageFile);

                        var galleryImage = new GalleryImage
                        {
                            ImagePath = imagePath,
                            Caption = galleryVM.Caption
                        };

                        project.Gallery.Add(galleryImage);
                    }
                }


                _context.Add(project);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }


            return View(viewModel);


            //if (ModelState.IsValid)
            //{
            //_context.Add(project);
            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            //}
            // return View(project);
        }


        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsFolder); // Ensure the folder exists
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return "/uploads/" + uniqueFileName;
        }

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
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ImageUrl,ProjectUrl,RepositoryUrl,DemoUrl,CreatedDate,LastUpdated,Category,Status,Client")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Id == id);
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
                _context.Projects.Remove(project);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
