namespace ConsoleApp1.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        //public string Password { get; set; }

        public User(string username)
        {
            Username = username;
        }
    }

}
//設定用戶類別，包含Id和Username