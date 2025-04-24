using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using VirtualPetBackend.Data;
using VirtualPetBackend.EndPoints;
using VirtualPetBackend.Settings;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("VirtualPetBackend");
builder.Services.AddSqlite<VirtualPetBackendContext>(connString);
builder.Services.AddSingleton<IPasswordHelper, PasswordHelper>();
builder.Services.AddScoped<LoginUser>();

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings!.Issuer, //Ignoring possible null case because idk what I am doing.
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
        };
    });

var app = builder.Build();

app.MapPetsEndPoints();
app.MapUserEndpoints();
app.UseAuthentication();
app.UseAuthorization();
await app.MigrateDbAsync();

app.Run();
