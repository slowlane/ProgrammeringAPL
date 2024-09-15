using System.ComponentModel.DataAnnotations;

namespace ProgrammeringAPL.Models
{
    
    public class ContactFormModel
    {
        [Required(ErrorMessage = "Namn krävs.")]
        [MinLength(2, ErrorMessage = "Ditt namn måste vara minst 2 tecken långt.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "E-post krävs.")]
        [EmailAddress(ErrorMessage = "Ange en giltig e-postadress.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ett meddelande krävs.")]
        [MinLength(10, ErrorMessage = "Meddelandet måste vara minst 10 tecken långt.")]
        public string Message { get; set; }
    }

}
