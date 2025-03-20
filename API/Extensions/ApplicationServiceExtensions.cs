using System;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    //this lets us extend 
    public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration config)
    {
        // Add services to the container.

        services.AddControllers();

        //check db context
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });

        services.AddCors();
        //interesting points on testing... easier if you have an interface vecause u can mock it
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
