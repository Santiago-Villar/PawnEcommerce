using System.Runtime.CompilerServices;

namespace Service.Product
{
    public interface IColor
    {

        public int Id { get; set; }  
        string Name { get; set; }
        string Code { get; set; }
        public ICollection<ProductColor> ProductColors {  get; set; }



        bool Equals(object other);
    }
}