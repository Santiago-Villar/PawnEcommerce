using Service.Exception;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Service.Product
{
    public class Product : IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        private int _price;
        public int Price
        {
            get => _price;
            set
            {
                if (value < 0) throw new ModelException("Price must be a positive integer.");
                _price = value;
            }
        }

        private int _stock;

        public int Stock
        {
            get => _stock;
            set
            {
                if (value < 0) throw new ModelException("Stock must be a positive integer.");
                _stock = value;
            }
        }

        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [JsonIgnore]
        public ICollection<ProductColor> ProductColors { get; set; } = new List<ProductColor>();

        [NotMapped]
        public IEnumerable<Color> Colors => ProductColors?.Select(pc => pc.Color);

        public void AddColor(Color color)
        {
            if (color == null) throw new ModelException("Color must not be null");

            if (!ProductColors.Any(pc => pc.Color.Id == color.Id))
            {
                var productColor = new ProductColor { Color = color, Product = this };
                ProductColors.Add(productColor);
            }
        }


    }
}