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
    Category aCategory = new Category();
    [TestMethod]
    public void CategoryIsNotNull()
    {
        Assert.IsNotNull(aCategory);

    }
}
