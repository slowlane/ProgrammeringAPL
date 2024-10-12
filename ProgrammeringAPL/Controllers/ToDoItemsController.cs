using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgrammeringAPL.Data;
using ProgrammeringAPL.Models;

namespace ProgrammeringAPL.Controllers
{
    public class ToDoItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Konstruktor för ToDoItemsController som initierar databaskontexten
        public ToDoItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ToDoItems
        // Hämtar en lista av todos och returnerar vyn
        public async Task<IActionResult> Index()
        {
            return View(await _context.ToDoItem.ToListAsync()); // Hämtar alla todos från databasen
        }

        // GET: ToDoItems/Details/5
        // Visar detaljer för en specifik todo
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) // Kontrollerar om id är null
            {
                return NotFound();
            }

            var toDoItem = await _context.ToDoItem
                .FirstOrDefaultAsync(m => m.Id == id); // Hämtar todon med specificerat id
            if (toDoItem == null)
            {
                return NotFound();
            }

            return View(toDoItem); // Returnerar vyn med todons detaljer
        }

        // GET: ToDoItems/Create
        // Visar formuläret för att skapa en ny todo
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToDoItems/Create
        // Skapar en ny todo baserat på inmatad data
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Task,IsComplete")] ToDoItem toDoItem)
        {
            if (ModelState.IsValid) // Kontrollerar om inmatad data är giltig
            {
                _context.Add(toDoItem); // Lägger till den nya todon i databasen
                await _context.SaveChangesAsync(); // Sparar ändringarna asynkront
                return RedirectToAction(nameof(Index)); // Redirectar till index-sidan efter skapandet
            }
            return View(toDoItem); // Returnerar vyn med todons information om validering misslyckas
        }

        // GET: ToDoItems/Edit/5
        // Visar formuläret för att redigera en existerande todo
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoItem = await _context.ToDoItem.FindAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }
            return View(toDoItem);
        }

        // POST: ToDoItems/Edit/5
        // Uppdaterar en existerande todo med ny data
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Task,IsComplete")] ToDoItem toDoItem)
        {
            if (id != toDoItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid) // Kontrollerar om inmatad data är giltig
            {
                try
                {   //Uppdaterar databasen och spara ändringarna asynkront
                    _context.Update(toDoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoItemExists(toDoItem.Id)) //Kontrollerar om todon fortfarande finns
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)); //Redirecta till index
            }
            return View(toDoItem); //Returnera vyn med todon om validering misslyckas
        }

        // GET: ToDoItems/Delete/5
        // Visar bekräftelsesidan för att ta bort en todo
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoItem = await _context.ToDoItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            return View(toDoItem);
        }

        // POST: ToDoItems/Delete/5
        // Tar bort en todo efter bekräftelse
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toDoItem = await _context.ToDoItem.FindAsync(id);
            if (toDoItem != null)
            {
                _context.ToDoItem.Remove(toDoItem); // Tar bort todon från databasen
            }

            await _context.SaveChangesAsync(); //Spara ändringarna i databasen
            return RedirectToAction(nameof(Index));
        }

        //Hjälpmetod som kontrollerar om en todo existerar
        private bool ToDoItemExists(int id)
        {
            return _context.ToDoItem.Any(e => e.Id == id);
        }
    }
}
