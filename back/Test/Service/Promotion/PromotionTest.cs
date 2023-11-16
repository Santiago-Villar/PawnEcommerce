using System.Drawing;
using Service.Promotion.ConcreteStrategies;
using Moq;
using Service.Product;
using Service.Promotion;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Test.Service.Promotion;

[TestClass]
[ExcludeFromCodeCoverage]
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
             "3x1 Fidelidad",
             "3x2",
             "20% OFF"
        };

        var promotions = promotionsCollection.GetPromotions();

        var names = promotions.Select(promotion => promotion.Name).ToList();
        
        CollectionAssert.AreEquivalent(promotionsName, names);
    }
}