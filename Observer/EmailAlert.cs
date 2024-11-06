// Observer
public class EmailAlert(string quoteName, double sellQuote, double buyQuote, List<string> destinationEmail) : IObserver<double>
{
    private readonly double _sellQuote = sellQuote;
    private readonly double _buyQuote = buyQuote;
    private readonly string _quoteName = quoteName;
    private readonly List<string> _destinationEmail = destinationEmail;

    public void OnNext(double regularMarketPrice)
    {
        Console.WriteLine("Atualizando...");
        Update(regularMarketPrice);
    }

    public void OnError(Exception error)
    {
        Console.WriteLine($"Erro ao monitorar {_quoteName}: {error.Message}");
    }

    public void OnCompleted()
    {
        Console.WriteLine($"Monitoramento do ativo {_quoteName} finalizado.");
    }

    public void Update(double regularMarketPrice)
    {
        try
        {
            if (regularMarketPrice > _sellQuote)
            {
                foreach (var email in _destinationEmail)
                {
                    SendEmail(email, "Inoa te aconselha... Venda! ", $"O preço de {_quoteName} está acima de {_sellQuote:F2} com um preço de mercado de {regularMarketPrice:F2}, te aconselhamos a venda dessa ação.");
                }
            }
            else if (regularMarketPrice < _buyQuote)
            {
                foreach (var email in _destinationEmail)
                {
                    SendEmail(email, "Inoa te aconselha... Compre!", $"O preço de {_quoteName} está abaixo de {_buyQuote:F2} com um preço de mercado de {regularMarketPrice:F2}, te aconselhamos a compra dessa ação.");
                }
            }
            else
            {
                Console.WriteLine($"Preço de {_quoteName} está dentro da faixa desejada com um preço de mercado de {regularMarketPrice}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao monitorar {_quoteName}: {ex.Message}");
        }
    }

    private static void SendEmail(string email, string subject, string body)
    {
        try
        {
            Console.WriteLine($"Enviando alerta");
            InoaB3.Services.SendEmail.Send(email, subject, body); // ja que é estatico eu não preciso instanciar o objeto
            Console.WriteLine($"Alerta enviado");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
        }
    }
}