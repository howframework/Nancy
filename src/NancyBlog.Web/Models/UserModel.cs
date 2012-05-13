namespace NancyBlog.Web.Models
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }

        public UserModel()
        {
        }

        public UserModel(string username)
        {
            Username = username;
        }
    }
}