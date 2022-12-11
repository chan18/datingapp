using API.Data;
using API.DTOs;
using API.Entities;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<ActionResult<IEnumerable<MembersDto>>> GetUsers() => 
    Ok(await userRepository.GetUserAsync() is { } users ? mapper.Map<IEnumerable<MembersDto>>(users) : BadRequest());
    

    [HttpGet("{username}")]
    public async Task<ActionResult<MembersDto>> GetUsers(string username) => await userRepository.GetUserByUserNameAsync(username) is { } user ? mapper.Map<MembersDto>(user) : BadRequest();
}