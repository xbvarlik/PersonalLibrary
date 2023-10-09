using PersonalLibrary.API;
using PersonalLibrary.Cache;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInMemoryCache(builder.Configuration);
builder.Services.ConfigureApplicationSettings();
builder.Services.AddApplicationDatabases(builder.Configuration);
builder.Services.AddApplicationServicesAndMappers();
builder.Services.AddIdentityService(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();