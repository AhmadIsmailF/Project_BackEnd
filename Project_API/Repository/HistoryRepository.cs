using Microsoft.Extensions.Options;
using Project_API.Data;
using Project_API.Models;
using Project_API.Repository.IRepository;

namespace Project_API.Repository
{
    public class HistoryRepository : InHistoryRepository
    {
        private readonly ApplicationDbContext _db;

        public HistoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Insert(int UserId, string text)
        {
                var history = new History
                {
                    UserId = UserId,
                    Text = text
                };
                await _db.Histories.AddAsync(history);
                await _db.SaveChangesAsync();
        }

        public ICollection<History> GetHistories()
        {
            return _db.Histories.OrderBy(a => a.UserId).ToList();
        }

       

        
    }

}
