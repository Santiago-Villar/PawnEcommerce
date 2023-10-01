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

        public List<Color> GetAll()
        {
            return _colorRepository.GetAll();
        }

        public Color Get(int id)
        {
            return _colorRepository.GetById(id);
        }
    }
}

