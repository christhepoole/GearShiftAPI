namespace GearShiftAPI.Models
{
    public class LoginModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class AuthResponse
    {
        public bool IsAuthSuccessful { get; set; }

        public string? ErrorMessage { get; set; }

        public string? Token { get; set; }
    }
}
