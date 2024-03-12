using Project_API.Models;

namespace Project_API.Repository.IRepository
{
    public interface InHistoryRepository
    {
        Task Insert(int UserId, string text);
        ICollection<History> GetHistories();

    }
}
