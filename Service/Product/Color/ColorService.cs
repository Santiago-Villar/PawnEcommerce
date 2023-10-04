using System;
using Service.Exception;

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
            var color = _colorRepository.GetById(id) ?? throw new RepositoryException($"Color with ID {id} not found.");
            return color;
        }
    }
}

