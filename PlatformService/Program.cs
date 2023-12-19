using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Configure the HTTP request pipeline.
// if (builder.Environment.IsProduction())
// {
Console.WriteLine("--> Using SqlServer Db");
builder.Services.AddDbContext<AppDbContext>(
    opt => opt.UseSqlServer(builder.Configuration["Platforms Conn"])
);

// }
// else
// {
//     Console.WriteLine("--> Using InMem Db");
//     // Add services to the container.
//     builder.Services.AddDbContext<AppDbContext>(opt =>
//     {
//         opt.UseInMemoryDatabase("InMem");
//     });
// }

builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

Console.WriteLine($"--> Platform Service Endpint: {builder.Configuration["CommandService"]}");

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// PrepDb.PrepPopulation(app, app.Environment.IsProduction());

app.Run();
