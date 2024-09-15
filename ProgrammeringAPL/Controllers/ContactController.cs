using Microsoft.AspNetCore.Mvc;
using ProgrammeringAPL.Models;

namespace ProgrammeringAPL.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View(new ContactFormModel());
        }
        [HttpPost]
        public IActionResult Submit(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                // Hantera inlämningen, t.ex. skicka e-post eller spara till en databas
                ViewBag.Message = "Formuläret skickades korrekt!";
                return View("Success"); // Laddar en vy som bekräftar inlämningen
            }

            // Om något är felaktigt, skicka tillbaka formuläret med felmeddelanden
            return View("Index", model);
        }
    }
}
