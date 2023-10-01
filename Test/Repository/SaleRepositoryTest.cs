using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Sale;
using Service.User;
using System.Linq;

namespace Test
{
    [TestClass]
    public class SaleRepositoryTest
    {
        private ISaleRepository _saleRepository;

        [TestInitialize]
        public void SetUp()
        {
            _saleRepository = new SaleRepository();
        }

        [TestMethod]
        public void AddSale_ShouldWork()
        {
            var sale = new Sale { UserId = 1 };
            _saleRepository.Add(sale);

            var addedSale = _saleRepository.GetAll().FirstOrDefault();
            Assert.IsNotNull(addedSale);
            Assert.AreEqual(1, addedSale.UserId);
        }

        [TestMethod]
        public void GetUserSales_ShouldReturnUserSales()
        {
            var sale1 = new Sale { UserId = 1 };
            var sale2 = new Sale { UserId = 2 };
            _saleRepository.Add(sale1);
            _saleRepository.Add(sale2);

            var userSales = _saleRepository.GetUserSales(1);
            Assert.AreEqual(1, userSales.Count);
            Assert.AreEqual(1, userSales.First().UserId);
        }

        [TestMethod]
        public void GetAllSales_ShouldReturnAllSales()
        {
            var sale1 = new Sale { UserId = 1 };
            var sale2 = new Sale { UserId = 2 };
            _saleRepository.Add(sale1);
            _saleRepository.Add(sale2);

            var allSales = _saleRepository.GetAll();
            Assert.AreEqual(2, allSales.Count);
        }
    }
}

