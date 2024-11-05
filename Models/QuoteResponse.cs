namespace InoaB3.Models
{
    public class QuoteResponse
    {
        public List<Quote>? Results { get; set; }
        public string? RequestedAt { get; set; }
        public string? Took { get; set; }
    }
}