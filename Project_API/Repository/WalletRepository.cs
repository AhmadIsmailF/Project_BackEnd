using AutoMapper;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Project_API.Data;
using Project_API.Migrations;
using Project_API.Models;
using Project_API.Models.Dto;
using Project_API.Repository.IRepository;

namespace Project_API.Repository
{
    public class WalletRepository : IWalletRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public WalletRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;

        }
        public async Task Buy(int walletId, double amount)
        {
            //var userobj = _mapper.Map<User>(userDto);

            var wallet = await _db.Wallets.FirstOrDefaultAsync(u => u.WalletId == walletId);

            if (wallet != null)
            {
                wallet.Balance += amount;
                Save();
            }
            else
                throw new ArgumentException("Wallet not found for the user.");
        }

        public async Task Sell(int walletId, double amount)
        {
            var wallet = await _db.Wallets.FirstOrDefaultAsync(u => u.WalletId == walletId);

            if (wallet != null)
            {
                wallet.Balance =wallet.Balance - amount;
                await Save();
            }
            else
                throw new ArgumentException("Wallet not found for the user.");
        }

        public async Task Convert(double fromCoin1, double toCoin2, int walletId)
        {
            //var bitCoin = 66958;
            //var ethureum = 3874;

            var wallet =await _db.Wallets.FirstOrDefaultAsync(u=>u.WalletId==walletId);
            var coin = await _db.Coins.FirstOrDefaultAsync(x => x.CoinId == wallet.CoinId);
            ////var rate = coin.CurrentPrice


            if ( coin.Name == "Bitcoin")
                toCoin2 =coin.CurrentPrice * 17.5;

            if (coin.Name == "Ethereum")
                toCoin2 =coin.CurrentPrice / 17.5;

        }

        public async Task Deposit(int walletId, double amount)
        {
            var wallet = await _db.Wallets.FirstOrDefaultAsync(u => u.WalletId == walletId);

            if (wallet != null)
            {
                wallet.Balance += amount;
                Save();
            }
            else
                throw new ArgumentException("Wallet not found for the user.");
            
        }

        public async Task Withdraw(int walletId, double amount)
        {
            var wallet =await _db.Wallets.FirstOrDefaultAsync(u => u.WalletId == walletId);

            if (wallet != null)
            {
                wallet.Balance -= amount;
                Save();
            }
            else
                throw new ArgumentException("Wallet not found for the user.");
        }

        public async Task<  bool> Save()
        {
            return await  _db.SaveChangesAsync() >= 0 ? true : false;
        }

        public bool WalletExists(int id)
        {
            return _db.Wallets.Any(a => a.WalletId == id);
        }

        public async Task< bool> CreateWallet(Wallet wallet)
        {
             _db.Wallets.Add(wallet);
            return await  Save();
        }

        public async Task< bool> UpdateWallet(Wallet wallet)
        {
            _db.Wallets.Update(wallet);
            return  await Save();
        }
    }
}
