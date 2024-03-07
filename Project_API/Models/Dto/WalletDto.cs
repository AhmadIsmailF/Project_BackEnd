using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Project_API.Models.Dto
{
    public class WalletDto
    {
        public int WalletId { get; set; }
        [JsonIgnore]
        public double Balance { get; set; }
        public double Amount { get; set; }
        //public double SellAmount { get; set; }
        public int CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int CVV { get; set; }
        public int UserId { get; set; }
        public int CoinId { get; set; }
    }
}
