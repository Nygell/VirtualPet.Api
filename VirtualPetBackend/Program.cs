using VirtualPetBackend.Data;
using VirtualPetBackend.EndPoints;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("VirtualPetBackend");
builder.Services.AddSqlite<VirtualPetBackendContext>(connString);
builder.Services.AddSingleton<IPasswordHelper, PasswordHelper>();
builder.Services.AddScoped<LoginUser>();

var app = builder.Build();

app.MapPetsEndPoints();
app.MapUserEndpoints();
await app.MigrateDbAsync();

app.Run();
