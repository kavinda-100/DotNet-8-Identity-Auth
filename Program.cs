using DotNet_8_Identity_Auth.data;
using DotNet_8_Identity_Auth.Extensions;
using DotNet_8_Identity_Auth.models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//* prevent object cycle
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

//* Add DbContext (database connection)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseUrl"));
});

//* Identity configuration as an extension method
builder.Services
    .AddIdentityHandlersAndStoresExtension()
    .AddConfigureIdentityOptionsExtension()
    .AddIdentityAuthenticationExtension(builder.Configuration);

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

// Identity configuration api endpoints
app.MapGroup("/api").MapIdentityApi<AppUser>();

app.Run();