using InoaB3.Managers;
using InoaB3.Models;

// Chama a API com a cotação solicitada e verifica o valor atual.
class Program 
{
    static async Task Main(string[] args)
    {
        if(args.Length != 3) {
            Console.WriteLine("Por favor, forneça três parâmetros.");
            return;
        }
        string quoteName = args[0];
        string quoteSellPrice = args[1];
        string quoteBuyPrice = args[2];

        if (!float.TryParse(quoteSellPrice, out float sellPrice))
        {
            Console.WriteLine("O preço de venda fornecido não é um número válido.");
            return;
        }

        if (!float.TryParse(quoteBuyPrice, out float buyPrice))
        {
            Console.WriteLine("O preço de compra fornecido não é um número válido.");
            return;
        }

        string endpoint = $"{quoteName}?modules=summaryProfile";
        
        try
        {
            Quote quote = await StockMarketManager.GetQuoteAsync(endpoint);

            Console.WriteLine($"Resultado da chamada da api: {quote.regularMarketPrice}");

            if (quote.regularMarketPrice >= sellPrice)
            {
                Console.WriteLine("Preço atual excede o preço de venda. Considere vender.");
            }
            else if (quote.regularMarketPrice <= buyPrice)
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

        Console.WriteLine($"Parâmetro 1: {quoteName}");
        Console.WriteLine($"Parâmetro 2: {sellPrice}");
        Console.WriteLine($"Parâmetro 2: {buyPrice}");
        Console.Write(args.Length);
    }
}