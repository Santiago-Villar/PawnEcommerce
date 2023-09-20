﻿using Service;
using Service.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Exception;

namespace Test;

[TestClass]
public class CategoryTest
{
    Category aCategory = new Category()
    {
        Name = "Casual"
    };
    [TestMethod]
    public void CategoryIsNotNull()
    {
        Assert.IsNotNull(aCategory);
    }
    [TestMethod]
    [ExpectedException(typeof(ModelException))]
    public void CategoryNameIsEmpty()
    {
        Category otherCategory = new Category()
        {
            Name = ""
        };
    }
    [TestMethod]
    public void CategoryNameIsOk()
    {
        Assert.AreEqual(aCategory.Name,"Casual");
    }

    public void CategoryNameFails()
    {
        Assert.AreNotEqual(aCategory.Name, "Outwear");
    }
}
