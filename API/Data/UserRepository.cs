using API.Entities;
using API.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class UserRepository : IUserRepository
{
    private readonly DataContext context;

    public UserRepository(DataContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<AppUser>> GetUserAsync() =>
    (context.Users is { } users && await users.ToListAsync() is { } allUsers) ?  allUsers : new List<AppUser>();

    public async Task<AppUser> GetUserByIdAsync(int id) =>
    (context.Users is { } users && await users.FindAsync(id) is { } user) ? user : new();

    public async Task<AppUser> GetUserByUserNameAsync(string username) => 
    context.Users is { } users && await users.SingleOrDefaultAsync(x => x.UserName == username) is { } user ? user : new();

    // save changes will hold how many got saved into the database.
    public async Task<bool> SaveAllAsync() => await context.SaveChangesAsync() > 0;

    // just in case method, To explicitly trigger entity framework model update.
    public void Update(AppUser user) => context.Entry(user).State = EntityState.Modified;
}
