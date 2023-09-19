namespace Test;
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
}
