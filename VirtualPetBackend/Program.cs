using VirtualPetBackend.Data;
using VirtualPetBackend.EndPoints;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("VirtualPetBackend");
builder.Services.AddSqlite<VirtualPetBackendContext>(connString);

var app = builder.Build();

app.MapPetsEndPoints();
await app.MigrateDbAsync();

app.Run();
