using System.Runtime.CompilerServices;

namespace Service.Product
{
    public interface IColor
    {

        public int Id { get; set; }  // Assuming you have an Id for Color
        public int ProductId { get; set; }  // Foreign Key to Product
        public Product Product { get; set; }  // Navigation property
        string Name { get; set; }



        bool Equals(object other);
    }
}