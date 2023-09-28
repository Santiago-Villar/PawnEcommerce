using System.Runtime.CompilerServices;

namespace Service.Product
{
    public interface IColor
    {

        public int Id { get; set; }  
        public int ProductId { get; set; } 
        public Product Product { get; set; }  
        string Name { get; set; }
        string Code { get; set; }



        bool Equals(object other);
    }
}