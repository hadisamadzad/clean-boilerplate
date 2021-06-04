using Identity.Domain.Users;

namespace Identity.Api.Models.Requests.Users
{
    public class UpdateUserRequest
    {
        public string Email { get; set; }
        public UserState State { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
