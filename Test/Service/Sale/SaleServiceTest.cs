using Moq;
using Service.Sale;

namespace Test.Service.Sale;

[TestClass]
public class SaleServiceTest
{
    [TestMethod]
    public void CanCreateSaleService_Ok()
    {
        var mockRepository = new Mock<ISaleRepository>();
        var saleService = new SaleService(mockRepository.Object);
        Assert.IsNotNull(saleService);
    }

}