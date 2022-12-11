using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountsController : BaseApiController
{
    private readonly DataContext context;
    private readonly ITokenService tokenService;

    public AccountsController(DataContext context, ITokenService tokenService)
    {
        this.context = context;
        this.tokenService = tokenService;
    }

    [HttpPost("register")] // POST: api/accounts/register
    public async Task<ActionResult<UserDto>> Regsiter(RegisterDto registerDto)
    {
        if(await UsersExists(registerDto.Username))
        {
            return BadRequest("Username is taken");
        }

        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            UserName = registerDto.Username,
            PasswordHash  = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key,
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

       return new UserDto
        {
            UserName = user.UserName ?? throw new Exception("Invalid username"),
            Token = tokenService.CreateToken(user),
        };
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);        
        
        if (user is null) return Unauthorized("Invalid username");

        // password validation.
        if (user.PasswordSalt is byte[] && user.PasswordHash is byte[])
        {
            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }
        }

        return new UserDto
        {
            UserName = user.UserName ?? throw new Exception("Invalid username"),
            Token = tokenService.CreateToken(user),
        };
    }

    private async Task<bool> UsersExists(string username)
    {
        return await context.Users.AnyAsync(user => user.UserName == username.ToLower());
    }
}
