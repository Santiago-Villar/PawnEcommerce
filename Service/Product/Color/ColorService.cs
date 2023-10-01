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
            try
            {
                var color = _colorRepository.GetById(id) ?? throw new ModelException($"Color with ID {id} not found.");
                return color;
            }
            catch (RepositoryException ex)
            {
                throw new ModelException($"Error retrieving color with ID {id}. Message: {ex.Message}");
            }
        }
    }
}

