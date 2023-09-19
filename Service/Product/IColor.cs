using System.Runtime.CompilerServices;

namespace Service.Product
{
    public interface IColor
    {
        string Name { get; set; }

        bool Equals(object Other);
    }
}