using Microsoft.AspNetCore.Mvc;
using ProgrammeringAPL.Models;

namespace ProgrammeringAPL.Controllers
{
    public class ContactController : Controller
    {

        // Visar kontaktformuläret
        public IActionResult Index()
        {
            return View(new ContactFormModel()); // Returnerar en tom modell för kontaktformuläret
        }

        // Tar emot data från kontaktformuläret och validerar det
        [HttpPost]
        public IActionResult Submit(ContactFormModel model)
        {
            if (ModelState.IsValid) // Kontrollerar om inmatad data är giltig
            {
                
                ViewBag.Message = "Formuläret skickades korrekt!";
                return View("Success"); // Visar en vy för framgångsrik inlämning
            }

            return View("Index", model); // Om valideringen misslyckas, returnera till formuläret med nuvarande data
        } 
    }
}
