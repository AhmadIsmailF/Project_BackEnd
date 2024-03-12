using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_API.Models
{
    public class History
    {
        [Key] 
        public int HistoryId { get; set; }

        public int UserId { get; set; }
        public string Text { get; set; }
    }
}
