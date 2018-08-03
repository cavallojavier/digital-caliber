using System.Security.Claims;
using System.Threading.Tasks;

namespace digital.caliber.spa.Auth
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, string userId, ClaimsIdentity identity);

        Task<ClaimsIdentity> GenerateClaimsIdentity(string userName, string id);
    }
}
