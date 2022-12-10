using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountsController : BaseApiController
{
    private readonly DataContext context;

    public AccountsController(DataContext context)
    {
        this.context = context;
    }

    [HttpPost("register")] // POST: api/accounts/register
    public async Task<ActionResult<AppUser>> Regsiter(RegisterDto registerDto)
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

        return user;
    }

    private async Task<bool> UsersExists(string username)
    {
        return await context.Users.AnyAsync(user => user.UserName == username.ToLower());
    }
}
