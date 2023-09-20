using System.Drawing;
using Service.Promotion.ConcreteStrategies;
using Moq;
using Service.Product;
using Service.Promotion;

namespace Test.Service.Promotion;

[TestClass]
public class PromotionTest
{
    [TestMethod]
    public void CanCreatePromotionsCollection_Ok()
    {
        var promotionsCollection = new PromotionCollection();
        Assert.IsNotNull(promotionsCollection);
    }
    
    [TestMethod]
    public void GetPromotions_Ok()
    {
        var promotionsCollection = new PromotionCollection();
        
        var promotionsName = new List<string>()
        {
            "Total Look",
             "Three For One",
             "Three For Two",
             "Twenty Percent Discount"
        };

        var promotions = promotionsCollection.GetPromotions();

        var names = promotions.Select(promotion => promotion.Name).ToList();
        
        CollectionAssert.AreEquivalent(promotionsName, names);
    }
}