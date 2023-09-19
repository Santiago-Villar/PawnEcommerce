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
}