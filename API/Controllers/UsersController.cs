using System.Security.Claims;
using API.DTOs;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class UsersController : BaseApiController
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public UsersController(IUserRepository userRepository,IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MembersDto>>> GetUsers() => Ok(await userRepository.GetMemberAsync());
    

    [HttpGet("{username}")]
    public async Task<ActionResult<MembersDto>> GetUsers(string username) =>  await userRepository.GetMemberAsync(username);

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        var user = await userRepository.GetUserByUserNameAsync(username);

        if(user is null) return NotFound();

        mapper.Map(memberUpdateDto, user);

        if(await userRepository.SaveAllAsync()) return NoContent();

        return BadRequest();
    }
}