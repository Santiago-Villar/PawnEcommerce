using Service;
using Service.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test;

[TestClass]
public class CategoryTest
{
    Category aCategory = new Category("Casual");
    [TestMethod]
    public void CategoryIsNotNull()
    {
        Assert.IsNotNull(aCategory);
    }
    [TestMethod]
    [ExpectedException(typeof(ServiceException))]
    public void CategoryNameIsEmpty()
    {
        Category otherCategory = new Category("");
    }
    [TestMethod]
    public void CategoryNameIsOk()
    {
        Assert.AreEqual(aCategory.Name,"Casual");
    }
}
