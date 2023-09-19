using Service.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Service.Product;
using System.Threading.Tasks;
using Service;

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

    [TestMethod]
    [ExpectedException(typeof(ServiceException))]
    public void BrandNameIsEmpty()
    {
        Brand otherBrand = new Brand("");
    }
    [TestMethod]
    public void BrandNameIsOk()
    {
        Assert.AreEqual(aBrand.Name, "Kova");
    }
    [TestMethod]

    public void BrandNameFails()
    {
        Assert.AreNotEqual(aBrand.Name, "Puma");
    }
}
