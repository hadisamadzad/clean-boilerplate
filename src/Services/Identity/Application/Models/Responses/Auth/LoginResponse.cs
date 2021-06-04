namespace Identity.Application.Models.Auth
{
    public class LoginResponse
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}