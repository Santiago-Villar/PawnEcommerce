﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Product;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ProductColorTest
    {
        [TestMethod]
        public void ProductColor_HasProduct()
        {
            var product = new Product
            {
                Id = 1,
                Name = "TestProduct",
                Price = 100
            };

            var color = new Color(1)
            {
                Name = "Red"
            };

            var productColor = new ProductColor
            {
                ProductId = product.Id,
                Product = product,
                ColorId = color.Id,
                Color = color
            };

            Assert.AreEqual(productColor.Product.Id, product.Id);
            Assert.AreEqual(productColor.Product.Name, product.Name);
        }

        [TestMethod]
        public void ProductColor_HasColor()
        {
            var product = new Product
            {
                Id = 1,
                Name = "TestProduct",
                Price = 100
            };

            var color = new Color(1)
            {
                Name = "Red"
            };

            var productColor = new ProductColor
            {
                ProductId = product.Id,
                Product = product,
                ColorId = color.Id,
                Color = color
            };

            Assert.AreEqual(productColor.Color.Id, color.Id);
            Assert.AreEqual(productColor.Color.Name, color.Name);
        }

        [TestMethod]
        public void ProductColor_AssociatesCorrectly()
        {
            var product = new Product
            {
                Id = 1,
                Name = "TestProduct",
                Price = 100
            };

            var color = new Color(1)
            {
                Name = "Red"
            };

            var productColor = new ProductColor
            {
                ProductId = product.Id,
                Product = product,
                ColorId = color.Id,
                Color = color
            };

            Assert.AreEqual(productColor.Product.Id, productColor.ProductId);
            Assert.AreEqual(productColor.Color.Id, productColor.ColorId);
        }
    }
}

