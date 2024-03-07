namespace Project_API.Models.Dto
{
    public class ConvertDto
    {
        public int WalletId { get; set; }
        public int FromCoinId { get; set; }
        public double Amount { get; set; }
        public int ToCoinId { get; set; }
    }
}
