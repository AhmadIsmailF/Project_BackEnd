namespace Project_API.Models.Dto
{
    public class ConvertDto
    {
        public int WalletId { get; set; }
        public int Coin1Id { get; set; }
        public int Coin2Id { get; set; }
        public double Amount { get; set; }
        public int UserId { get; set; }
    }
}
