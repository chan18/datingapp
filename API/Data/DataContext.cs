using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    /*
        table name as users.
        users table have the props from AppUser class.
    */
    public DbSet<AppUser>? Users { get; set; }
}
