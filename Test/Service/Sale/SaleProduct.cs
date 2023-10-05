using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Product;
using Service.Sale;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SaleProductTests
    {
        [TestMethod]
        public void Can_Create_SaleProduct_And_Set_Properties()
        {
            var saleProduct = new SaleProduct();

            saleProduct.SaleId = 1;
            saleProduct.ProductId = 2;
            saleProduct.Sale = new Sale();
            saleProduct.Product = new Product();

            Assert.AreEqual(1, saleProduct.SaleId);
            Assert.AreEqual(2, saleProduct.ProductId);
            Assert.IsNotNull(saleProduct.Sale);
            Assert.IsNotNull(saleProduct.Product);
        }
    }
}


