using API.Entities;

namespace API.Interface;
public interface ITokenService
{
    string CreateToken(AppUser user);
}
