using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Service.Filter
{
    public class PriceFilterCriteria : IFilterCriteria
    {
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }

        public PriceFilterCriteria(int minPrice, int maxPrice)
        {
            MinPrice = minPrice;
            MaxPrice = maxPrice;
        }
    }
}
