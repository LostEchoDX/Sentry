using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SentryAPI.Data;
using SentryAPI.Repositories;
using SentryAPI.Selenium;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SentryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SentryContext") ?? throw new InvalidOperationException("Connection string 'SentryContext' not found.")));
builder.Services.AddScoped<IRepository, PoIRepository>();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMvc();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var repo = services.GetRequiredService<IRepository>();
    //context.Database.EnsureCreated();
    DbInitializer.Initialize(repo);
    //TestScript.ChromeSession();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
