using Service.User;
using Service.Exception;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly EcommerceContext _context;

        public UserRepository(EcommerceContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(User user)
        {
            if (user == null)
                throw new ServiceException("User cannot be null or empty.");

            _context.Users.Add(user);
            _context.SaveChanges();
        }


        public void Delete(User user)
        {
            if (user == null)
                throw new ServiceException("User cannot be null or empty.");

            _context.Users.Remove(user);
            _context.SaveChanges();
        }


        public User? Get(int id)
        {
            return _context.Users
                           .FirstOrDefault(u => u.Id == id);
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }


        public User? Get(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ServiceException("Email cannot be null or empty.");

            return _context.Users
                           .FirstOrDefault(u => u.Email == email);
        }


        public User Update(User updateUser)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Id == updateUser.Id);

            if (existingUser == null)
                throw new RepositoryException("User was not found.");

            _context.Entry(existingUser).CurrentValues.SetValues(updateUser);

            _context.SaveChanges();
            return existingUser;
        }

    }
}
