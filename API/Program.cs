using API.Data;
using API.Extentions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationService(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

//app.UseHttpsRedirection();

app.UseCors(builder => 
builder.AllowAnyHeader()
       .AllowAnyMethod()
       .WithOrigins("*"));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scop = app.Services.CreateScope();
var service = scop.ServiceProvider;
try
{
       var context = service.GetRequiredService<DataContext>();
       await context.Database.MigrateAsync();
       await Seed.SeedUsers(context);
} 
catch (Exception ex)
{
       if (service.GetService<ILogger<Program>>() is { } logger)
       {
              logger.LogError(ex, "An error occurred during migrations");
       }
}

app.Run();
