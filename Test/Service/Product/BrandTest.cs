using Service.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Service.Product;
using System.Threading.Tasks;
using Service;
using Service.Exception;

namespace Test;

[TestClass]
public class BrandTest
{
    public static Brand aBrand = new Brand()
    {
        Name = "Kova",
        Id = 231
    };
    [TestMethod]
    public void BrandIsNotNull()
    {
        Assert.IsNotNull(aBrand);
    }

    [TestMethod]
    [ExpectedException(typeof(ModelException))]
    public void BrandNameIsEmpty()
    {
        Brand otherBrand = new Brand()
        {
            Name = "",
            Id = 21
        };
    }
    [TestMethod]
    public void BrandIdIsOk()
    {
        Assert.AreEqual(aBrand.Id, 231);
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
