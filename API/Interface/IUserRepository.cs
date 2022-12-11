using API.DTOs;
using API.Entities;

namespace API.Interface;
public interface IUserRepository
{
    void Update(AppUser user);

    Task<bool> SaveAllAsync();

    Task<IEnumerable<AppUser>> GetUserAsync();
    Task<AppUser> GetUserByIdAsync(int id);
    Task<AppUser> GetUserByUserNameAsync(string username);

    Task<IEnumerable<MembersDto>> GetMemberAsync();
    Task<MembersDto> GetMemberAsync(string username);
}
