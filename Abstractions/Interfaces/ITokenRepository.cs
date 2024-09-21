using mediAPI.Models;

namespace mediAPI.Abstractions.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(Account acc);
    }
}
