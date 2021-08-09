using System;

namespace XyzCase.Tweeldata.ApiClient.Models
{
    public class PriceInfo
    {
        public DateTime Datetime { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Volume { get; set; }
    }
}
