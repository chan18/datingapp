using System.Security.Claims;
using API.DTOs;
using API.Entities;
using API.Extentions;
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
    private readonly IPhotoService photoService;

    public UsersController(IUserRepository userRepository,IMapper mapper, 
        IPhotoService photoService)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
        this.photoService = photoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MembersDto>>> GetUsers() => Ok(await userRepository.GetMemberAsync());
    

    [HttpGet("{username}")]
    public async Task<ActionResult<MembersDto>> GetUsers(string username) =>  await userRepository.GetMemberAsync(username);

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {
        if(await userRepository.GetUserByUserNameAsync(User.GetUsername()) is { } user)
        {
            mapper.Map(memberUpdateDto, user);    
            if(await userRepository.SaveAllAsync()) return NoContent();
            return BadRequest();
        }
        return NotFound();
    }

    [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
    {
        if (await userRepository.GetUserByUserNameAsync(User.GetUsername()) is { } user)
        {
            var result = await photoService.AddPhotoAsync(file);

            if (result is { } sucessfullResult) 
            {
                var photo = new Photo()
                {
                    Url = sucessfullResult.SecureUrl.AbsoluteUri,
                    PublicId = sucessfullResult.PublicId, 
                    IsMain = user.Photos.Count == 0 ? true : false,
                }; 
                user.Photos.Add(photo);

                if (await userRepository.SaveAllAsync()) 
                    return CreatedAtAction(
                            nameof(GetUsers),
                            new {username = user.UserName},
                            mapper.Map<PhotoDto>(photo));

                return BadRequest("Problem adding photo");
            }
            else if (result is { } errors)
            {
                return BadRequest(errors.Error.Message);
            }
        }

        return NotFound();
    }

    [HttpPut("set-main-photo/{photoId}")]
    public async Task<ActionResult> SetMainPhoto(int photoId)
    {
        if (await userRepository.GetUserByUserNameAsync(User.GetUsername()) is { } user &&
            user.Photos.FirstOrDefault(x => x.Id == photoId) is { } photo)
        {
            if (photo.IsMain) return BadRequest("this is already your main photo");

            if (user.Photos.FirstOrDefault(x => x.IsMain) is { } currentMain) {
                currentMain.IsMain = false;
            }

            photo.IsMain = true;

            if (await userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Problem setting the main ptoto");            
        }

        return NotFound();
    }
}