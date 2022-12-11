using API.Data;
using API.Entities;
using API.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class UsersController : BaseApiController
{
    private readonly IUserRepository userRepository;

    public UsersController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() => Ok(await userRepository.GetUserAsync());
    

    [HttpGet("{username}")]
    public async Task<ActionResult<AppUser>> GetUsers(string username) => await userRepository.GetUserByUserNameAsync(username);
}