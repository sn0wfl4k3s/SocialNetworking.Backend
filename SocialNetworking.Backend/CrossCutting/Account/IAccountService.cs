using System.Threading.Tasks;

namespace CrossCutting.Account
{
    public interface IAccountService
    {
        string GenerateUsername(string name, string lastname);
        Task<string> GenerateUsernameAsync(string name, string lastname);
    }
}
