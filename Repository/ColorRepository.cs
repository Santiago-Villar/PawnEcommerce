using Service.Exception;
using Service.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ColorRepository
    {
        private readonly EcommerceContext _context;

        public ColorRepository(EcommerceContext context)
        {
            _context = context;
        }

        public List<Color> GetAll()
        {
            return _context.Colors.ToList();
        }

        public Color GetById(int id)
        {
            var color = _context.Colors.FirstOrDefault(c => c.Id == id);
            if (color == null)
            {
                throw new ModelException($"No color found with ID {id}");
            }
            return color;
        }
    }
}
