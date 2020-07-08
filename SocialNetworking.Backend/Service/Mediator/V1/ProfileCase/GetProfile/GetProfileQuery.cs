using Core.Service.Core;
using Domain.Entity;

namespace Service.Mediator.V1.ProfileCase.GetProfile
{
    public class GetProfileQuery : IRequestUser<GetProfileVM>
    {
        public User User { get; set; }
        public string Username { get; set; }
    }
}
