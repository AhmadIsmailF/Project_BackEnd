using Project_API.Models;

namespace Project_API.Repository.IRepository
{
    public interface ICoinRepository
    {
       Task< List<Coin>> GetCoins();
       Task< Coin> Getcoin(int coinId);
    }
}
