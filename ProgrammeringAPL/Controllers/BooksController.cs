using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgrammeringAPL.Models;
using ProgrammeringAPL.Data;
using Microsoft.AspNetCore.Authorization;

namespace ProgrammeringAPL.Controllers
{
    [Authorize] // Säkerställer att bara auktoriserade användare kan komma åt denna controller
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Konstruktor för BooksController som initierar databaskontexten
        public BooksController(ApplicationDbContext context) 
        {
            _context = context; // Används för att interagera med databasen
        }

        // GET: Books
        // Hämtar en lista av böcker och returnerar vyn
        public async Task<IActionResult> Index()
        {
            return View(await _context.Books.ToListAsync()); // Hämtar alla böcker från databasen
        }

        // GET: Books/Details/5
        // Visar detaljer för en specifik bok
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) // Kontrollerar om id är null
            {
                return NotFound(); // Returnerar en NotFound vy om id saknas
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id); // Hämtar boken med specificerat id
            if (book == null)
            {
                return NotFound();
            }

            return View(book); // Returnerar vyn med bokens detaljer
        }

        // GET: Books/Create
        // Visar formuläret för att skapa en ny bok
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // Skapar en ny bok baserat på inmatad data
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Author,YearPublished")] Book book)
        {
            if (ModelState.IsValid) // Kontrollerar om inmatad data är giltig
            {
                _context.Add(book); // Lägger till den nya boken i databasen
                await _context.SaveChangesAsync(); // Sparar ändringarna asynkront
                return RedirectToAction(nameof(Index)); // Redirectar till index-sidan 
            }
            return View(book); // Returnerar vyn med bokens information om validering misslyckas
        }

        // GET: Books/Edit/5
        // Visar formuläret för att redigera en existerande bok
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) // Kontrollerar om id är null
            {
                return NotFound(); // Returnerar en NotFound vy om id saknas
            }

            var book = await _context.Books.FindAsync(id); // Hämtar boken med specificerat id
            if (book == null)
            {
                return NotFound(); // Returnerar en NotFound vy om boken inte finns
            }
            return View(book); // Returnerar vyn med bokens information
        }

        // POST: Books/Edit/5
        // Uppdaterar en existerande bok med ny data
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,YearPublished")] Book book)
        {
            if (id != book.Id) // Kontrollerar om id matchar bokens id
            {
                return NotFound();
            }

            if (ModelState.IsValid) // Kontrollerar om inmatad data är giltig
            {
                try
                {
                    _context.Update(book); // Uppdaterar boken i databasen
                    await _context.SaveChangesAsync(); // Sparar ändringarna asynkront
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id)) // Kontrollerar om boken fortfarande finns
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirectar till index-sidan efter uppdateringen
            }
            return View(book);
        }

        // GET: Books/Delete/5
        // Visar bekräftelsesidan för att ta bort en bok
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) // Kontrollerar om id är null
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id); // Hämtar boken med specificerat id
            if (book == null)
            {
                return NotFound();
            }

            return View(book); // Returnerar vyn med bokens information som ska tas bort
        }

        // POST: Books/Delete/5
        // Tar bort en bok efter bekräftelse
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id); // Hämtar boken med specificerat id
            if (book != null)
            {
                _context.Books.Remove(book); // Tar bort boken från databasen
            }

            await _context.SaveChangesAsync(); // Sparar ändringarna asynkront
            return RedirectToAction(nameof(Index));
        }

        // Hjälpmetod som kontrollerar om en bok existerar
        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
