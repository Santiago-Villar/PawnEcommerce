using Service.Promotion.ConcreteStrategies;

namespace Test.Promotion;

[TestClass]
public class PromotionTest
{
    [TestMethod]
    public void CanCreateTotalLook_Ok()
    {
        var totalLook = new TotalLook();
        Assert.IsNotNull(totalLook);
    }
    
    public void CanCreateThreeForOne_Ok()
    {
        var threeForOne = new ThreeForOne();
        Assert.IsNotNull(threeForOne);
    }
}