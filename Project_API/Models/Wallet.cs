using System.ComponentModel.DataAnnotations.Schema;

namespace Project_API.Models
{
    public class Wallet
    {
        public int WalletId { get; set; }
        public double Balance { get; set;}
        
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        [ForeignKey("CoinId")]
        public int  CoinId { get; set; }
    }
}


//stable 1
//bitcoin  67113 66958
//ethereum    3724 3874
