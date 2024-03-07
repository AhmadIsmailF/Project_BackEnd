namespace Project_API.Models.Dto
{
    public class WalletDBDto
    {
        public int WalletId { get; set; }
        public double Balance { get; set; }
        public int UserId { get; set; }
        public int CoinId { get; set; }
    }
}
