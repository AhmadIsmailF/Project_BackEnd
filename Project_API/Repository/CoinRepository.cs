using Microsoft.EntityFrameworkCore;
using Project_API.Data;
using Project_API.Models;
using Project_API.Repository.IRepository;

namespace Project_API.Repository
{
    public class CoinRepository : ICoinRepository
    {
        private readonly ApplicationDbContext _db;

        public CoinRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task< Coin> Getcoin(int coinId)
        {
            return await _db.Coins.FirstOrDefaultAsync(a => a.CoinId == coinId);
        }

        public async Task<List<Coin>> GetCoins()
        {
            return await _db.Coins.ToListAsync();
        }
    }
}
