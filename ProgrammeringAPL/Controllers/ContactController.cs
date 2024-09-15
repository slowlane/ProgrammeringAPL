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
                
                ViewBag.Message = "Formuläret skickades korrekt!";
                return View("Success");
            }

            return View("Index", model);
        }
    }
}
