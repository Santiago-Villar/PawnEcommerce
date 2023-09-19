namespace Test;

using Service;
using Service.Product;

[TestClass]
public class ColorTest
{
    Color aColor = new Color()
    {
        Name = "Green"
    };

    [TestMethod]
    public void ColorIsNotNull()
    {
        Assert.IsNotNull(aColor);
    }

    [TestMethod]
    [ExpectedException(typeof(ServiceException))]
    public void CreateEmptyColor()
    {
        Color color = new Color()
        {
            Name=""
        };
    }
}
