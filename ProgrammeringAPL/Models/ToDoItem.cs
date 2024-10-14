using System.ComponentModel.DataAnnotations;

namespace ProgrammeringAPL.Models
{
    // Representerar en todo-post
    public class ToDoItem
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Uppgiftsbeskrivning krävs")]
        [StringLength(100)]
        public string Task { get; set; }

        public bool IsComplete { get; set; } = false;
    }
}
