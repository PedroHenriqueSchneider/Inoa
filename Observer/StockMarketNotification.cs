namespace InoaB3.Observer;

public class StockMarketNotification : ISubject 
{
    private List<IObserver<double>> _observers = new List<IObserver<double>>(); // Lista de emails
    private readonly string _stockName;
    private double _stockPrice; 

    public StockMarketNotification(string stockName, double stockPrice)
    {
        _stockName = stockName;
        _stockPrice = stockPrice;
    }

    public void Attach(IObserver<double> observer)
    {
        try
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao adicionar observador: {ex.Message}");
        }
    }

    public void Detach(IObserver<double> observer)
    {
        if (_observers.Contains(observer))
        {
            _observers.Remove(observer);
        }
    }

    public void UpdateStockPrice(double stockPrice)
    {
        try
        {
            Console.WriteLine("Necessário atualizar o preço da ação");
            _stockPrice = stockPrice;

            foreach (var observer in _observers)
            {
                observer.OnNext(stockPrice); // Notifica todos os observadores
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao atualizar o preço da ação: {ex.Message}");
        }
    }

    public string GetStockName() => _stockName;

    public double GetStockPrice() => _stockPrice;
}