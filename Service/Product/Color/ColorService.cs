using System;
namespace Service.Product
{
	public class ColorService : IColorService
	{
        public IColorRepository _colorRepository { get; set; }

        public ColorService(IColorRepository repo)
        {
            _colorRepository = repo;
        }
    }
}

