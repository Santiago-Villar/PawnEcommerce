using Service.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Service.Product;
using System.Threading.Tasks;

namespace Test;

[TestClass]
public class BrandTest
{
    public static Brand aBrand = new Brand("Kova");
    [TestMethod]
    public void BrandIsNotNull()
    {
        Assert.IsNotNull(aBrand);
    }
}
