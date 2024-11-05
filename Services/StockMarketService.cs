using System.Net.Http.Json;
using InoaB3.Models;
using InoaB3.Utils;

namespace InoaB3.Services
{
    public class StockMarketService(string baseAddress, string token)
    {
        private readonly HttpClient _client = HttpClientHelper.GetClient(baseAddress, token);

        public async Task<Quote> GetQuoteAsync(string endpoint)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();

                var quoteResponse = await response.Content.ReadFromJsonAsync<QuoteResponse>()
                                    ?? throw new InvalidOperationException("Dados de cotação são nulos.");

                // coalescence expression
                var quote = (quoteResponse.Results?.FirstOrDefault()) ?? throw new InvalidOperationException("Nenhuma cotação encontrada.");
                return quote;
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"Erro HTTP: {httpEx.Message}");
                throw;
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"Formato de conteúdo não suportado: {ex.Message}");
                throw;
            }
            catch (System.Text.Json.JsonException ex)
            {
                Console.WriteLine($"Erro ao desserializar o JSON: {ex.Message}");
                throw;
            }
        } 
    }
}