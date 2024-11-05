// Observer
public class EmailAlert(string quoteName, double sellQuote, double buyQuote, List<string> destinationEmail) : IObserver<double>
{
    private readonly double _sellQuote = sellQuote;
    private readonly double _buyQuote = buyQuote;
    private readonly string _quoteName = quoteName;
    private readonly List<string> _destinationEmail = destinationEmail;
    private bool _hasSentSellAlert = false;
    private bool _hasSentBuyAlert = false;

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
            if (regularMarketPrice >= _sellQuote && !_hasSentSellAlert)
            {
                foreach (var email in _destinationEmail)
                {
                    SendEmail(email, "Aconselha-se vender", $"O preço de {_quoteName} está acima de {_sellQuote}: {regularMarketPrice}");
                }
                _hasSentSellAlert = true;
                _hasSentBuyAlert = false;
            }
            else if (regularMarketPrice <= _buyQuote && !_hasSentBuyAlert)
            {
                foreach (var email in _destinationEmail)
                {
                    SendEmail(email, "Aconselha-se comprar", $"O preço de {_quoteName} está abaixo de {_buyQuote}: {regularMarketPrice}");
                }
                _hasSentBuyAlert = true;
                _hasSentSellAlert = false;
            }
            else
            {
                Console.WriteLine($"Preço de {_quoteName} está dentro da faixa desejada: {regularMarketPrice}");
                _hasSentSellAlert = false;
                _hasSentBuyAlert = false;
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
            Console.WriteLine($"Enviando alerta: {subject}");
            InoaB3.Services.SendEmail.Send(email, subject, body); // ja que é estatico eu não preciso instanciar o objeto
            Console.WriteLine($"Alerta enviado: {subject}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
        }
    }
}