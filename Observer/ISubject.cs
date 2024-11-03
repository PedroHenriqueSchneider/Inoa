namespace InoaB3.Observer

{
   public interface ISubject
    {
        void Attach(IObserver observer); // Adiciona um observador
        void Detach(IObserver observer); // Remove um observador
        void Notify(double stockPrice); // Notifica todos os observadores com o preço atualizado
    } 
}