// Observer
using InoaB3.Services.SendEmail;
using InoaB3.Observer;
using System.Threading.Tasks;

public class EmailAlert(string quoteName, double sellQuote, double buyQuote, List<string> destinationEmail) : IObserver<double>
{
    private readonly double _sellQuote = sellQuote;
    private readonly double _buyQuote = buyQuote;
    private readonly string _quoteName = quoteName;
    private readonly List<string> _destinationEmail = destinationEmail;
    private bool _hasSentSellAlert = false;
    private bool _hasSentBuyAlert = false;

    public async void OnNext(double regularMarketPrice)
    {
        Console.WriteLine("Atualizando...");
        await UpdateAsync(regularMarketPrice);
    }

    public void OnError(Exception error)
    {
        Console.WriteLine($"Erro ao monitorar {_quoteName}: {error.Message}");
    }

    public void OnCompleted()
    {
        Console.WriteLine($"Monitoramento do ativo {_quoteName} finalizado.");
    }

    public async Task UpdateAsync(double regularMarketPrice)
    {
        try
        {
            if (regularMarketPrice >= _sellQuote && !_hasSentSellAlert)
            {
                foreach (var email in _destinationEmail)
                {
                    await SendEmailAsync(email, "Aconselha-se vender", $"O preço de {_quoteName} está acima de {_sellQuote}: {regularMarketPrice}");
                }
                _hasSentSellAlert = true;
                _hasSentBuyAlert = false;
            }
            else if (regularMarketPrice <= _buyQuote && !_hasSentBuyAlert)
            {
                foreach (var email in _destinationEmail)
                {
                    await SendEmailAsync(email, "Aconselha-se comprar", $"O preço de {_quoteName} está abaixo de {_buyQuote}: {regularMarketPrice}");
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

    private static async Task SendEmailAsync(string email, string subject, string body)
    {
        try
        {
            Console.WriteLine($"Enviando alerta: {subject}");
            await SendEmail.SendAsync(email, subject, body); // ja que é estatico eu não preciso instanciar o objeto
            Console.WriteLine($"Alerta enviado: {subject}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
        }
    }
}