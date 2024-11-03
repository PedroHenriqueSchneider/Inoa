using InoaB3.Services;
using InoaB3.Models;
using Microsoft.Extensions.Configuration;

namespace InoaB3.Managers
{
    public static class StockMarketManager
    {
        private static readonly StockMarketService _stockMarketService;

        static StockMarketManager()
        {
            try{
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                // Lê as configurações
                string baseAddress = configuration["ApiSettings:BaseAddress"];
                string apiToken = configuration["ApiSettings:ApiToken"]; 

                _stockMarketService = new StockMarketService(baseAddress, apiToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter cotação do manager: {ex.Message}");
                throw;
            }
        }

        public static async Task<Quote> GetQuoteAsync(string endpoint)
        {
            try
            {
                Quote quote = await _stockMarketService.GetQuoteAsync(endpoint);
                return quote;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter cotação do quota: {ex.Message}");
                throw;
            }
        }
    }
}