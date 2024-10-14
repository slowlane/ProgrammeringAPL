namespace ProgrammeringAPL.Models
{
    // Representerar ett felmeddelande som används vid felsökning
    public class ErrorViewModel
    {
        public string? RequestId { get; set; } // ID för att spåra fångade fel i applikationen

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId); // Returnerar true om RequestId är satt
    }
}
