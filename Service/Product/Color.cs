using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Product
{
    public class Color:IColor
    {
        public string Name { get; set; }


        public override bool Equals(object obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Color otherColor = (Color)obj;
            return Name == otherColor.Name;
        }
    }
}
