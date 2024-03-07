using System.ComponentModel.DataAnnotations.Schema;

namespace Project_API.Models
{
    public class Coin
    {
        public int CoinId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double CurrentPrice { get; set; }
    }
}
