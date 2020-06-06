using System;

namespace Service.Mediator.V1.AccountCase.Login
{
    public class LoginUserVM
    {
        public string Access_token { get; set; }
        public string Token_type { get; set; }
        public DateTime Expires_in { get; set; }
    }
}
