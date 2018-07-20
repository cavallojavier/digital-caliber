using System.Security.Claims;
using System.Threading.Tasks;

namespace digital.caliber.spa.Auth
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);

        Task<ClaimsIdentity> GenerateClaimsIdentity(string userName, string id);
    }
}
