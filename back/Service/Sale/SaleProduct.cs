using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Service.Product;

namespace Service.Sale
{
    public class SaleProduct:ISaleProduct
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public int SaleId { get; set; }
        [JsonIgnore]
        public Sale Sale { get; set; } 
        public int ProductId { get; set; }
        public Service.Product.Product Product { get; set; } 
    }
}

