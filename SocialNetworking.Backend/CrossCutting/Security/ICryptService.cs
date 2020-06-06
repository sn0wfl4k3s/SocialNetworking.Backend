using System.Threading.Tasks;

namespace CrossCutting.Security
{
    public interface ICryptService
    {
        string CreateHash(string text);
        Task<string> CreateHashAsync(string text);
    }
}
