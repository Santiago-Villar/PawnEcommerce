using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Service.User;
using Service.User.Role;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class UserRepositoryTests
    {
        private EcommerceContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<EcommerceContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            var context = new EcommerceContext(options);
            context.Database.EnsureDeleted();
            return context;
        }

        private User CreateSampleUser()
        {
            return new User
            {   Id=1,
                Email = "sample@example.com",
                PasswordHash="12345",
                Address = "Sample Address",
                Roles = new List<RoleType> { RoleType.User }
            };
        }

        [TestMethod]
        public void AddUser_ShouldWork()
        {
            using var context = GetInMemoryDbContext();
            var repository = new UserRepository(context);

            var user = CreateSampleUser();
            repository.Add(user);

            var userInDb = context.Users.FirstOrDefault(u => u.Email == "sample@example.com");
            Assert.IsNotNull(userInDb);
            Assert.AreEqual("Sample Address", userInDb.Address);
        }

        [TestMethod]
        public void DeleteUser_ShouldWork()
        {
            using var context = GetInMemoryDbContext();
            var repository = new UserRepository(context);

            var user = CreateSampleUser();
            context.Users.Add(user);
            context.SaveChanges();

            repository.Delete(user);

            var userInDb = context.Users.FirstOrDefault(u => u.Email == "sample@example.com");
            Assert.IsNull(userInDb);
        }

        [TestMethod]
        public void GetUserById_ShouldReturnCorrectUser()
        {
            using var context = GetInMemoryDbContext();
            var repository = new UserRepository(context);

            var user = CreateSampleUser();
            context.Users.Add(user);
            context.SaveChanges();

            var fetchedUser = repository.Get(user.Id);
            Assert.IsNotNull(fetchedUser);
            Assert.AreEqual("Sample Address", fetchedUser.Address);
        }

        [TestMethod]
        public void GetUserByEmail_ShouldReturnCorrectUser()
        {
            using var context = GetInMemoryDbContext();
            var repository = new UserRepository(context);

            var user = CreateSampleUser();
            context.Users.Add(user);
            context.SaveChanges();

            var fetchedUser = repository.Get("sample@example.com");
            Assert.IsNotNull(fetchedUser);
            Assert.AreEqual("Sample Address", fetchedUser.Address);
        }

        [TestMethod]
        public void UpdateUser_ShouldUpdateExistingUser()
        {
            using var context = GetInMemoryDbContext();
            var repository = new UserRepository(context);

            var user = CreateSampleUser();
            context.Users.Add(user);
            context.SaveChanges();

            user.Address = "Updated Address";
            repository.Update(user);

            var updatedUser = context.Users.FirstOrDefault(u => u.Email == "sample@example.com");
            Assert.IsNotNull(updatedUser);
            Assert.AreEqual("Updated Address", updatedUser.Address);
        }

    }
}
