using Microsoft.EntityFrameworkCore;
using System.Configuration;
using WheelFactory.Models;

namespace WheelFactory;
public class Program

{

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container

        builder.Services.AddControllers();
        builder.Services.AddDbContext<WheelContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("wheel")));
       

        
        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}