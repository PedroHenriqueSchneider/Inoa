// Esse é meu subject 
/* Quando algo mudar no subject o observer será notificado, assim, quando uma ação ultrapassar algum dos limites, tanto inferior, 
quanto superior, o observer será notificado e enviará um email para o usuário. */

namespace InoaB3.Observer;

public class StockMarketNotification : ISubject
{
    private List<IObserver> _observers = new List<IObserver>(); // Lista de emails
    private readonly string _stockName;
    private double _stockPrice; 

    public StockMarketNotification(string stockName, double stockPrice)
    {
        _stockName = stockName;
        _stockPrice = stockPrice;
    }

    // Conferir se será necessário
    public void Attach(IObserver observer)
    {
        // Assim eu confiro antes se contem o observer na lista
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
    }

    // Conferir se será necessário
    // Talvez não seja necessário, mas manter o padrão do observer
    public void Detach(IObserver observer)
    {
        if (_observers.Contains(observer))
        {
            _observers.Remove(observer);
        }
    }

    // Notifica os observadores
    public void Notify(double stockPrice)
    {
        foreach (var observer in _observers)
        {
            observer.Update(stockPrice);
        }
    }

    // Ainda não tenho certeza de como será usado e se será necessário
    // Como referência para uso do display dos dados
    public void UpdateStockPrice(double stockPrice)
    {
        _stockPrice = stockPrice;

        foreach (var observer in _observers)
        {
            (observer as IObserver<decimal>)?.OnNext((decimal)stockPrice);
        }
    }

    public string GetStockName()
    {
        return _stockName;
    }

    // Para pegar o preço e então conseguir comparar com o limite definido e disparar o email
    public double GetStockPrice()
    {
        return _stockPrice;
    }
}