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
                throw new RepositoryException("User cannot be null or empty.");

            _context.Users.Add(user);
            _context.SaveChanges();
        }


        public void Delete(User user)
        {
            if (user == null)
                throw new RepositoryException("User cannot be null or empty.");

            _context.Users.Remove(user);
            _context.SaveChanges();
        }


        public User? Get(int id)
        {
            return _context.Users
                           .Include(u => u.Sales) 
                           .FirstOrDefault(u => u.Id == id);
        }


        public User? Get(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new RepositoryException("Email cannot be null or empty.");

            return _context.Users
                           .Include(u => u.Sales) 
                           .FirstOrDefault(u => u.Email == email);
        }


        public void Update(User user)
        {
            if (user == null)
                throw new RepositoryException("User cannot be null or empty.");

            _context.Users.Update(user);
            _context.SaveChanges();
        }

    }
}
