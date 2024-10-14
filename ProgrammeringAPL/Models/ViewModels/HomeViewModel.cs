using System.Collections.Generic;
using ProgrammeringAPL.Models;

namespace ProgrammeringAPL.Models.ViewModels
{
    // ViewModel för att visa projekt på hemsidan
    public class HomeViewModel
    {
        // Lista med projekt som visas på hemsidan
        public IEnumerable<Project> Projects { get; set; }
    }
}
