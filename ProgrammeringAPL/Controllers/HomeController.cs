using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProgrammeringAPL.Data;
using ProgrammeringAPL.Models.ViewModels;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProgrammeringAPL.Models;

namespace ProgrammeringAPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        
        // Konstruktor för HomeController som initierar logger och databaskontext

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger; // Används för loggning av händelser i applikationen
            _context = context; // Används för att interagera med databasen
        }
        
        
        // Hämtar data för att visa huvudsidan
        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeViewModel
            {
                Projects = await _context.Projects
                    .Include(p => p.Gallery) // Inkluderar galleribilder som är relaterade till projekten
                    .ToListAsync() // Hämtar listan med projekt asynkront
            };

            return View(viewModel); // Returnerar vyn med ViewModel som innehåller projekten
        }

        // Visar integritetspolicyn
        public IActionResult Privacy()
        {
            return View();
        }


        // Hanterar fel och returnerar en felvy med information om felet
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
