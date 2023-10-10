using Microsoft.Extensions.DependencyInjection;
using PersonalLibrary.Utilities.Accessors;
using PersonalLibrary.Utilities.Managers;

namespace PersonalLibrary.Utilities;

public static class Bootstrapper
{
    public static void AddUtilities(this IServiceCollection services)
    {
        services.AddAccessors();
        services.AddManagers();
    }
    private static void AddAccessors(this IServiceCollection services)
    {
        services.AddScoped<ISessionAccessor, SessionAccessor>();
    }
    
    private static void AddManagers(this IServiceCollection services)
    {
        services.AddScoped<EmailManager>();
    }
}