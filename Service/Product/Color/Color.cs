using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Exception;

namespace Service.Product
{
    public class Color : IColor
    {
        public int Id { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (value.Equals("")) throw new ModelException("Color Name must not be empty");
                _name = value;
            }
        }

        public string Code { get; set; }
        public Color(int id)
        {
            Id = id;
        }

        public ICollection<ProductColor> ProductColors { get; set; } = new List<ProductColor>();

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            Color otherColor = (Color)obj;
            return Name == otherColor.Name;
        }
    }

}
