namespace ProgrammeringAPL.Models
{
    // Representerar ett felmeddelande som anv�nds vid fels�kning
    public class ErrorViewModel
    {
        public string? RequestId { get; set; } // ID f�r att sp�ra f�ngade fel i applikationen

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId); // Returnerar true om RequestId �r satt
    }
}
