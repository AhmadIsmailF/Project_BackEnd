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
                await Save();
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

        public async Task Convert(int Coin1Id, int Coin2Id, double Amount, int userId)
        {
            var wallet = await _db.Wallets.ToListAsync();
            var userWallets = wallet.Where(z => z.UserId == userId).ToList();
            var coinFrom = wallet.SingleOrDefault(z => z.CoinId == Coin1Id);
            var coinTo = wallet.SingleOrDefault(z => z.CoinId == Coin2Id);

            var coin1 = await _db.Coins.FirstOrDefaultAsync(z => z.CoinId == Coin1Id);
            var coin2 = await _db.Coins.FirstOrDefaultAsync(z => z.CoinId == Coin2Id);
            var rate = coin1.CurrentPrice / coin2.CurrentPrice;
            if (Coin1Id ==1)
            {
                coinFrom.Balance = coinFrom.Balance - Amount;
                coinTo.Balance = coinTo.Balance + Amount * rate;
            }

            if (Coin2Id ==2) 
            {
                coinFrom.Balance = coinFrom.Balance - Amount;
                coinTo.Balance = coinTo.Balance + Amount / rate;
            }
             
            await Save();

        }

        public async Task Deposit(int userId, double amount)
        {
            var wallet = await _db.Wallets.ToListAsync();
            var userWallets=wallet.Where(z=>z.UserId == userId).ToList();
            var stableCoin=userWallets.Where(z=>z.CoinId == 3).FirstOrDefault();

            if (wallet != null)
            {
                stableCoin.Balance += amount;
                await Save();
            }
            else
                throw new ArgumentException("Wallet not found for the user.");
        }

        public async Task Withdraw(int userId, double amount)
        {
            var wallet = await _db.Wallets.ToListAsync();
            var userWallets = wallet.Where(z => z.UserId == userId).ToList();
            var stableCoin = userWallets.Where(z => z.CoinId == 3).FirstOrDefault();

            if (wallet != null)
            {
                stableCoin.Balance = stableCoin.Balance - amount;
                await Save();
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
