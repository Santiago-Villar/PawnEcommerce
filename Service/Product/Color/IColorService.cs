using System;
namespace Service.Product
{
	public interface IColorService
	{
        public IColorRepository _colorRepository { get; set; }

        public List<Color> GetAll();
    }
}

