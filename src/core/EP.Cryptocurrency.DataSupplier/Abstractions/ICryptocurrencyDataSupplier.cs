using System.Threading.Tasks;

namespace EP.Cryptocurrency.DataSupplier.Abstractions
{
    public interface ICryptocurrencyDataSupplier
    {
        Task Supply();
    }
}
