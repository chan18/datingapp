using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")] // GET /api/users
public class UsersController : ControllerBase
{
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<AppUser>> GetUsers()
    {
        var users = _context.Users.ToList();
        return users;
    }

    [HttpGet("{id}")]
    public ActionResult<AppUser> GetUsers(int id)
    {
        if(_context.Users.Find(id) is { } user)
        {
            return user;
        }
        else
        {
            return NotFound();
        }    
    }
}