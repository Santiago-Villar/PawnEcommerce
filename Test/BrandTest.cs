using Service.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test;

[TestClass]
public class BrandTest
{
    [TestMethod]
    public void BrandIsNotNull()
    {
        Assert.IsNotNull(aBrand);
    }
}
