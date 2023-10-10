using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using PersonalLibrary.API.Constants;
using PersonalLibrary.API.Filters;
using PersonalLibrary.API.Mappings;
using PersonalLibrary.API.Services;
using PersonalLibrary.Core.Entities;
using PersonalLibrary.Repository;
using PersonalLibrary.Repository.MongoDB;
using PersonalLibrary.Utilities;

namespace PersonalLibrary.API;

public static class Bootstrapper
{
    public static void AddApplicationDatabases(this IServiceCollection services , IConfiguration configuration)
    {
        services.AddSqlContext(configuration);
        services.AddMongoDb(configuration);
    }

    public static void ConfigureApplicationSettings(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddApplicationControllersConfig();
        
        services.AddSwagger();
        services.AddEndpointsApiExplorer();
    }

    public static void AddApplicationServicesAndMappers(this IServiceCollection services)
    {
        services.AddApplicationServices();
        services.AddApplicationMappers();
        services.AddUtilities();
    }
    
    private static void AddApplicationControllersConfig(this IServiceCollection services)
    {
        services.AddControllers(
            options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().AddRequirements(new SessionExistsRequirement()).Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }
        );
        
        services.AddScoped<IAuthorizationHandler, SessionExistsHandler>();
    }
    
    private static void AddSqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(x =>
        {
            x.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
        });
    }

    public static void AddIdentityService(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtBearerTokenSection = configuration.GetSection(nameof(JwtBearerTokenSettings));
        var jwtBearerTokenSettings = jwtBearerTokenSection.Get<JwtBearerTokenSettings>();
        var jwtSecretKey = Encoding.ASCII.GetBytes(jwtBearerTokenSettings!.SecurityKey);
        
        services.AddIdentity<User, Role>(x =>
            {
                x.User.RequireUniqueEmail = true;
                x.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(15);
                x.Lockout.MaxFailedAccessAttempts = 3;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        
        services.Configure<DataProtectionTokenProviderOptions>(x =>
        {
            x.TokenLifespan = TimeSpan.FromHours(2);
        });
        
        services.Configure<JwtBearerTokenSettings>(jwtBearerTokenSection);

        services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new()
                {
                    ValidIssuer = jwtBearerTokenSettings.Issuer,
                    ValidAudience = jwtBearerTokenSettings.Audiences[0],
                    IssuerSigningKey = new SymmetricSecurityKey(jwtSecretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
    }
    
    private static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoDbSettings = configuration.GetSection(nameof(MongoDbSettings));

        if (mongoDbSettings is null) throw new MongoException("MongoDbSettings is null");

        services.Configure<MongoDbSettings>(mongoDbSettings);
    }
    
    private static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<AuthService>();
        services.AddScoped<UserService>();
        services.AddScoped<UserRoleService>();
        services.AddScoped<RoleService>();
        
        services.AddScoped<SessionService>();
        services.AddScoped<SessionCacheService>();
        services.AddScoped<UserSessionService>();
        
        services.AddScoped<TokenService>();
        
        services.AddScoped<AuthorService>();
        services.AddScoped<BookService>();
        services.AddScoped<BooksOfUserService>();
        services.AddScoped<GenreService>();
        services.AddScoped<PublisherService>();
        services.AddScoped<StatusService>();
        services.AddScoped<TagsOfUserService>();
        
    }

    private static void AddApplicationMappers(this IServiceCollection services)
    {
        services.AddScoped<AuthorMapper>();
        services.AddScoped<BookMapper>();
        services.AddScoped<BooksOfUserMapper>();
        services.AddScoped<GenreMapper>();
        services.AddScoped<PublisherMapper>();
        services.AddScoped<RoleMapper>();
        services.AddScoped<StatusMapper>();
        services.AddScoped<TagsOfUserMapper>();
        services.AddScoped<UserMapper>();
    }
    
    private static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
            {
                Description = "Standard Authorization header using the Bearer scheme(\"bearer {token}\")",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Id = "oauth2",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }
            });
        });
    }
}