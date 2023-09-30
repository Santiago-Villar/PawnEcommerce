using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using global::Service.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Test
{


    [TestClass]
    public class ProductRepositoryTests
    {
        private Mock<EcommerceContext> _mockContext;
        private Mock<DbSet<Product>> _mockDbSet;
        private ProductRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product A" },
            new Product { Id = 2, Name = "Product B" }
        }.AsQueryable();

            _mockDbSet = new Mock<DbSet<Product>>();
            _mockDbSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(products.Provider);
            _mockDbSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(products.Expression);
            _mockDbSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(products.ElementType);
            _mockDbSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(products.GetEnumerator());

            _mockContext = new Mock<EcommerceContext>();
            _mockContext.Setup(c => c.Products).Returns(_mockDbSet.Object);

            _repository = new ProductRepository(_mockContext.Object);
        }

        [TestMethod]
        public void AddProduct_WithValidProduct_ShouldCallAddAndSaveChanges()
        {
            var newProduct = new Product { Id = 3, Name = "Product C" };

            _repository.AddProduct(newProduct);

            _mockDbSet.Verify(m => m.Add(It.IsAny<Product>()), Times.Once());
            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }



    }
}
