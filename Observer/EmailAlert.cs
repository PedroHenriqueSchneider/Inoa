// Esse é meu observer
using InoaB3.Services.SendEmail;
using InoaB3.Observer;

public class EmailAlert : IObserver<double>
{
    private readonly double _sellQuote;
    private readonly double _buyQuote;
    private readonly string _quoteName;
    private readonly string _destinationEmail;
    private bool _hasSentSellAlert = false;
    private bool _hasSentBuyAlert = false;


    // Se atentar se dessa forma ele corresponde com o schedule do projeto 
    public EmailAlert(string quoteName, double sellQuote, double buyQuote, string destinationEmail)
    {
        _quoteName = quoteName;
        _sellQuote = sellQuote;
        _buyQuote = buyQuote;
        _destinationEmail = destinationEmail;
    }

    // Método para mandar uma notificação de preço atualizado, chamado quando atualizar o preço
    public void OnNext(double regularMarketPrice)
    {
        try{
            if (regularMarketPrice >= _sellQuote)
            {
                SendEmail(_destinationEmail, $"Aconselha-se vender", $"O preço de {_quoteName} está acima de {_sellQuote}: {regularMarketPrice}");
            }
            else if (regularMarketPrice <= _buyQuote)
            {
                SendEmail(_destinationEmail, "Aconselha-se comprar", $"O preço de {_quoteName} está abaixo de {_buyQuote}: {regularMarketPrice}");
            }
            else{
                Console.WriteLine($"Preço de {_quoteName} está dentro da faixa.");
            }
        }   
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao monitorar {_quoteName}: {ex.Message}");
        }
    }
    // Método chamado se ocorrer um erro
    public void OnError(Exception error)
    {
        Console.WriteLine($"Erro ao monitorar {_quoteName}: {error.Message}");
    }

    // Método chamado quando o monitoramento termina
    public void OnCompleted()
    {
        Console.WriteLine($"Monitoramento do ativo {_quoteName} finalizado.");
    }

    public void Update(double regularMarketPrice)
    {
        try{
            if (regularMarketPrice >= _sellQuote && !_hasSentSellAlert)
            {
                SendEmail(_destinationEmail, "Aconselha-se vender", $"O preço de {_quoteName} está acima de {_sellQuote}: {regularMarketPrice}");
                _hasSentSellAlert = true;
                _hasSentBuyAlert = false;
            }
            else if (regularMarketPrice <= _buyQuote && !_hasSentBuyAlert)
            {
                SendEmail(_destinationEmail, "Aconselha-se comprar", $"O preço de {_quoteName} está abaixo de {_buyQuote}: {regularMarketPrice}");
                _hasSentBuyAlert = true;
                _hasSentSellAlert = false;
            }
            else if (regularMarketPrice > _buyQuote && regularMarketPrice < _sellQuote)
            {
                // Reset flags se o preço estiver na faixa desejada
                _hasSentSellAlert = false;
                _hasSentBuyAlert = false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao monitorar {_quoteName}: {ex.Message}");
        }
    }

    // Método para enviar o e-mail de alerta
    private void SendEmail(string email, string subject, string body)
    {
        try
        {
            SendEmail sendEmail = new SendEmail(); // email, subject, body 
            sendEmail.Send(email, subject, body);
            Console.WriteLine($"Alerta enviado: {subject}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
        }
    }
}