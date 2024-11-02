namespace InoaB3.Models
{
    public class QuoteResponse
    {
        public List<Quote> results { get; set; }
        public string requestedAt { get; set; }
        public string took { get; set; }
    }
}