using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class UsersController : BaseApiController
{
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        if (_context.Users is { } users)
        {
            return await users.ToListAsync();
        }

        return BadRequest("Please Loging");
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>> GetUsers(int id)
    {
        if( _context.Users is { } users &&
            await users.FindAsync(id) is { } user)
        {
            return user;
        }
        else
        {
            return NotFound();
        }    
    }
}