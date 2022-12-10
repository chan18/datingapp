using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interface;
using Microsoft.IdentityModel.Tokens;

namespace API.Service;

public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey key;
    public TokenService(IConfiguration config)
    {
            key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"] ?? throw new Exception("Invalid Configuration")));
    }
    public string CreateToken(AppUser user)
    {
        // claim with username
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.NameId, user.UserName ?? throw new Exception("Invalid username")){}
        };

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}