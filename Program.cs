using LWAApi.Models;
using LWAApi.Repositories;
using LWAApi.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;




var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Info("LWApi Launched");



var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
    //User
builder.Services.AddDbContext<DatabaseContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("dbConnection")));

    //student
    builder.Services.AddDbContext<DatabaseContexta>
    (option => option.UseSqlServer(builder.Configuration.GetConnectionString("dbConnection2")));

    //Player
    builder.Services.AddDbContext<DatabaseContextb>
    (option => option.UseSqlServer(builder.Configuration.GetConnectionString("dbConnection3")));

    builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200"
                                             ).AllowAnyHeader().AllowAnyMethod();
                      });
});

builder.Services.AddTransient<IUserRepository, UserRepository>();
    builder.Services.AddTransient<IStudentrepo, Studentrepo>();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

    // NLog: Setup NLog for Dependency injection
    //builder.Logging.ClearProviders();
    //builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    //builder.Host.UseNLog();

    var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors();

app.MapControllers();

app.Run();
