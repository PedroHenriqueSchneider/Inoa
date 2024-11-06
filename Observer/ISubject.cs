namespace InoaB3.Observer
{
    public interface ISubject
    {
        void Attach(IObserver<double> observer); // Adiciona um observador
        void UpdateStockPrice(double stockPrice); // Notifica todos os observadores com o preço atualizado
    } 
}