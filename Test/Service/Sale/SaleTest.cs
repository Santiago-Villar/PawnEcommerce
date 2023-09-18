
namespace Test.Service.Sale;
using Service;

[TestClass]
public class SaleTest
{
    [TestMethod]
    public void CanCreateSale_Ok()
    {
        Sale s = new Sale();
        Assert.IsNotNull(s);
    }
}
