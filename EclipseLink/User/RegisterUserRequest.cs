namespace EclipseLink.UserManagement
{
    public class RegisterUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public bool ReceiveNotifications { get; set; }
    }
}
