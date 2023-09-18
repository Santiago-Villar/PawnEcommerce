using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using Service.User;

namespace Test;

[TestClass]
public class UserTest
{
    User user = new User();

    [TestMethod]
    public void CreateUserOk()
    {
        Assert.IsNotNull(user);
    }
}
