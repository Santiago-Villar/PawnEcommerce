using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using Service.Product;

namespace Test;

[TestClass]
public class ProductTest
{
    Product testProduct = new Product();
    [TestMethod]
    public void CreateProductOk()
    {
        Assert.IsNotNull(testProduct);
    }
}
