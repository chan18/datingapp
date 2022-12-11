using API.DTOs;
using API.Entities;
using API.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class UserRepository : IUserRepository
{
    private readonly DataContext context;
    private readonly IMapper mapper;

    public UserRepository(DataContext context,IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<MembersDto>> GetMemberAsync() =>
    (context.Users is { } usersData &&
            await usersData.ProjectTo<MembersDto>(mapper.ConfigurationProvider)
            .ToListAsync() is { } users) ?
        users : 
        new();

    public async Task<MembersDto> GetMemberAsync(string username) =>
        (context.Users is { } users &&
            await users.Where(x => x.UserName == username)
            .ProjectTo<MembersDto>(mapper.ConfigurationProvider)
            .SingleOrDefaultAsync() is { } user) ?
        user : 
        new();

    public async Task<IEnumerable<AppUser>> GetUserAsync() =>
    (context.Users is { } users && await users.Include(p => p.Photos).ToListAsync() is { } allUsers) ?  allUsers : new List<AppUser>();

    public async Task<AppUser> GetUserByIdAsync(int id) =>
    (context.Users is { } users && await users.FindAsync(id) is { } user) ? user : new();

    public async Task<AppUser> GetUserByUserNameAsync(string username) => 
    context.Users is { } users && await users.Include(p => p.Photos).SingleOrDefaultAsync(x => x.UserName == username) is { } user ? user : new();

    // save changes will hold how many got saved into the database.
    public async Task<bool> SaveAllAsync() => await context.SaveChangesAsync() > 0;

    // just in case method, To explicitly trigger entity framework model update.
    public void Update(AppUser user) => context.Entry(user).State = EntityState.Modified;
}
