using StudentAPI.Helper;

namespace StudentAPI.Model
{
    public class Student
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }
        [MinimumAgeAttribute(18,ErrorMessage="Age must be Atleast 18")]
        public int Age { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
    }
}
