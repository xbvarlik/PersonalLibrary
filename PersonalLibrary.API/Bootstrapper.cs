using Microsoft.EntityFrameworkCore;
using PersonalLibrary.Repository;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API;

public static class Bootstrapper
{
    public static void AddSqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(x =>
        {
            x.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
        });
    }

    public static void AddIdentityService(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>().AddEntityFrameworkStores<AppDbContext>();
    }
}