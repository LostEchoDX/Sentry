using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SentryAPI.Data;
using SentryAPI.Selenium;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SentryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SentryContext") ?? throw new InvalidOperationException("Connection string 'SentryContext' not found.")));
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

    var context = services.GetRequiredService<SentryContext>();
    //context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
    //TestScript.ChromeSession();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
