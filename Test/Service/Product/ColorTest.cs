using Service.Exception;
using Service.Product;

namespace Test;

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
    [ExpectedException(typeof(ModelException))]
    public void CreateEmptyColor()
    {
        Color color = new Color()
        {
            Name=""
        };
    }


    [TestMethod]
    public void ColorEqualsOk() {
        Color anotherColor = new Color()
        {
            Name = "Green"
        };
        Assert.AreEqual(aColor,anotherColor);
    }

    [TestMethod]
    public void ColorEqualsFails()
    {
        Color anotherColor = new Color()
        {
            Name = "Blue"
        };
        Assert.AreNotEqual(aColor, anotherColor);
    }
}
