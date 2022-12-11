using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Seed
{
    public static async Task SeedUsers(DataContext context)
    {
        if(context.Users is { } users && await users.AnyAsync()) return;
        
        var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
        var usersData = JsonSerializer.Deserialize<List<AppUser>>(await File.ReadAllTextAsync("Data/UserSeedData.json")) ?? new();

        foreach (var user in usersData)
        {
            using var hmac = new HMACSHA512();

            user.UserName = user.UserName?.ToLower();
            // since it is a seed data for testing in local development, 12345 passowrd is fine.
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("12345"));
            user.PasswordSalt = hmac.Key;

            context.Users?.Add(user);
        }

        await context.SaveChangesAsync();
    }
}
