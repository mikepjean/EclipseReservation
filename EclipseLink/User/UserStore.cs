using EclipseLink.Persistence;
using System.Collections.Generic;

namespace EclipseLink.UserManagement
{
        public interface IUserStore
        {
            User? GetUserById(int user_id); 
            void Save(User user);
            void Delete(string username);
            User? GetUserByUserName(string username);
    }


        public class UserStore : IUserStore
        {
            // SQL Server DB
            private readonly ApplicationDbContext _context;

        public UserStore(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a User by their unique ID.
        /// </summary>
        /// <param name="user_id">The unique ID of the user.</param>
        /// <returns>The User object if found, otherwise null.</returns>
        public User? GetUserById(int user_id)
        {
            return _context.Users.Find(user_id);
        }

        /// <summary>
        /// Retrieves a User by their username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>The User object if found, otherwise null.</returns>
        public User? GetUserByUserName(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }


        /// <summary>
        /// Saves or updates a User in the store.
        /// </summary>
        /// <param name="user">The User object to save.</param>
        public void Save(User user)
        {
            if (_context.Users.Any(u => u.User_Id == user.User_Id))
            {
                _context.Users.Update(user);
            }
            else
            {
                User _user = new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    ReceiveNotifications = user.ReceiveNotifications
                };
                _context.Users.Add(_user);
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Deletes a User by their username.
        /// </summary>
        /// <param name="username">The username of the user to delete.</param>
        public void Delete(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
        }

}
