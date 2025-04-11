using System.Collections.Generic;

namespace EclipseLink.User
{
        public interface IUserStore
        {
            User? GetUserById(int user_id); 
            void Save(User user);
            void Delete(string username);
        }


        public class UserStore : IUserStore
        {
            // In-memory database keyed by username
            private static readonly Dictionary<string, User> Database = new();

        /// <summary>
        /// Retrieves a User by their unique ID.
        /// </summary>
        /// <param name="user_id">The unique ID of the user.</param>
        /// <returns>The User object if found, otherwise null.</returns>
        public User? GetUserById(int user_id)
        {
            if(Database.Values == null)
            {
                return null;
            }
            return Database.Values.FirstOrDefault(user => user.User_Id == user_id);
        }

        /// <summary>
        /// Saves or updates a User in the store.
        /// </summary>
        /// <param name="user">The User object to save.</param>
        public void Save(User user) =>
                Database[user.Username] = user;

            /// <summary>
            /// Deletes a User by their username.
            /// </summary>
            /// <param name="username">The username of the user to delete.</param>
            public void Delete(string username) =>
                Database.Remove(username);
        }

}
