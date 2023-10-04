using Service.Exception;
using Service.Product;

namespace Test;

[TestClass]
public class ColorTest
{
    Color aColor = new Color(4)
    {
        Name = "Green",
        Code = 	"#008000"
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
        Color color = new Color(1)
        {
            Name=""
        };
    }


    [TestMethod]
    public void ColorEqualsOk() {
        Color anotherColor = new Color(2)
        {
            Name = "Green",
            Code = "#008000"

        };
        Assert.AreEqual(aColor,anotherColor);
        Assert.AreEqual(aColor.Code, anotherColor.Code);
    }

    [TestMethod]
    public void ColorEqualsFails()
    {
        Color anotherColor = new Color(3)
        {
            Name = "Blue"
        };
        Assert.AreNotEqual(aColor, anotherColor);
    }
}
