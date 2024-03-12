using System.Text.Json.Serialization;

namespace Project_API.Models.Dto
{
    public class WithdrawDto
    {
        public int WalletId { get; set; }
        [JsonIgnore]
        public double Balance { get; set; }
        public double Amount { get; set; }
        //public double SellAmount { get; set; }
        public int UserId { get; set; }
        public int CoinId { get; set; }
    }
}
