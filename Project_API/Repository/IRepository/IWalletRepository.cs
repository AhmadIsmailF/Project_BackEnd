using Project_API.Models;
using Project_API.Models.Dto;

namespace Project_API.Repository.IRepository
{
    public interface IWalletRepository
    {
        Task<bool> Save();
        Task Deposit(int walletId, double amount);
        Task Withdraw(int walletId,double amount);
        Task Buy(int walletId, double amount);
        Task Sell(int walletId, double amount);
        Task Convert(int Coin1Id,int Coin2Id,double Amount,int userId);
        bool WalletExists(int id);
        Task<bool> CreateWallet(Wallet wallet);
        Task<bool> UpdateWallet(Wallet wallet);
        ICollection<Wallet> GetWallets();
        Wallet GetWallet(int walletId);
    }
}
