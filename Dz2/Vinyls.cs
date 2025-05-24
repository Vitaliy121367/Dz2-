using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz2
{
    public class Vinyl
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Publisher { get; set; }
        public int TrackCount { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
        public bool IsOnSale { get; set; }
        public int DiscountPercentage { get; set; }
        public int SoldCount { get; set; }
        public bool IsReserved { get; set; }
        public int? ReservedByUserId { get; set; }
    }

}
