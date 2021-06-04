namespace Identity.Api.Models.Requests.Users
{
    public class UpdateUserPasswordRequest
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
