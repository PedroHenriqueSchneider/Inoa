﻿using InoaB3.Managers;
using InoaB3.Models;
using InoaB3.Observer;
using System.Globalization;

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

        if (float.TryParse(quoteSellPrice, NumberStyles.Float, CultureInfo.InvariantCulture, out float sellPrice))
        {
            Console.WriteLine($"Preço de venda convertido: {sellPrice}"); // Saída esperada: 22.59
        }
        else
        {
            Console.WriteLine("Conversão do preço de venda falhou");
            return;
        }

        if (float.TryParse(quoteBuyPrice, NumberStyles.Float, CultureInfo.InvariantCulture, out float buyPrice))
        {
            Console.WriteLine($"Preço de compra convertido: {buyPrice}"); // Saída esperada
        }
        else
        {
            Console.WriteLine("Conversão do preço de compra falhou");
            return;
        }

        string endpoint = $"{quoteName}?modules=summaryProfile";
        
        try
        {
            // Configurar para pegar a lista de emails cadastrados
            // Usar exemplo de destinationEmail
            string destinationEmail = "phbschneider@hotmail.com";
            EmailAlert emailAlert = new (quoteName, sellPrice, buyPrice, destinationEmail);

            Quote quote = await StockMarketManager.GetQuoteAsync(endpoint);

            StockMarketNotification stockNotification = new (quoteName, quote.regularMarketPrice);

            stockNotification.Attach(emailAlert);

            stockNotification.UpdateStockPrice(quote.regularMarketPrice); // a partir daqui temos o envio de email
            
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