using Microsoft.AspNetCore.Identity;

namespace Blog.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
