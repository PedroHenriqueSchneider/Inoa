using InoaB3.Managers;
using InoaB3.Models;
using InoaB3.Observer;
using System.Globalization;
using Microsoft.Extensions.Configuration;

class Program
{
    private static string ?quoteName;
    private static float sellPrice;
    private static float buyPrice;
    private static List<string> ?emails;
    private static EmailAlert ?emailAlert;
    private static StockMarketNotification ?stockNotification;

    static void Main(string[] args)
    {
        if (args.Length != 3)
        {
            Console.WriteLine("Por favor, forneça três parâmetros.");
            return;
        }

        quoteName = args[0];
        // deixei melhor aqui, de forma a deixar o código menor.
        if (!float.TryParse(args[1], NumberStyles.Float, CultureInfo.InvariantCulture, out sellPrice) ||
            !float.TryParse(args[2], NumberStyles.Float, CultureInfo.InvariantCulture, out buyPrice))
        {
            Console.WriteLine("Conversão dos preços de venda ou compra falhou.");
            return;
        }

        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        emails = [];

        foreach (var email in configuration.GetSection("EmailsList:Emails").GetChildren())
        {
            if (email.Value != null)
                emails.Add(email.Value);
        }

        stockNotification = new StockMarketNotification(quoteName, 0); 
        emailAlert = new EmailAlert(quoteName, sellPrice, buyPrice, emails);

        var timer = new Timer(async _ => await NotifyPeriodical(), null, TimeSpan.Zero, TimeSpan.FromSeconds(10));

        Console.WriteLine("Pressione Enter para encerrar o programa...");
        Console.ReadLine();
    }

    private static async Task NotifyPeriodical()
    {
        try
        {
            string endpoint = $"{quoteName}?modules=summaryProfile";
            Quote quote = await StockMarketManager.GetQuoteAsync(endpoint);

            Console.WriteLine($"Resultado da chamada da API: {quote.RegularMarketPrice}");

            if(stockNotification != null){
                stockNotification.UpdateStockPrice(quote.RegularMarketPrice);
                if(emailAlert != null)
                    stockNotification.Attach(emailAlert);
                stockNotification.UpdateStockPrice(quote.RegularMarketPrice); // Envia o alerta, se aplicável
            }

            if (quote.RegularMarketPrice >= sellPrice)
            {
                Console.WriteLine("Preço atual excede o preço de venda. Considere vender.");
            }
            else if (quote.RegularMarketPrice <= buyPrice)
            {
                Console.WriteLine("Preço atual está abaixo do preço de compra. Considere comprar.");
            }
            else
            {
                Console.WriteLine("Preço atual está dentro da faixa desejada.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocorreu um erro: {ex.Message}");
        }
    }
}