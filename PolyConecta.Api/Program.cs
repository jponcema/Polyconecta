using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using PolyConecta.Infrastructure.Outbox;
using PolyConecta.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "PolyConecta Operational API",
        Version = "v1",
        Description = "Operational Routing, Work Centers & CONTPAQi Integration Engine"
    });
});

builder.Services.AddDbContext<PolyDbContext>(options =>
    options.UseInMemoryDatabase("PolyConectaInMemory")); // Using InMemory for development/validation

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddSingleton<IOutboxPublisher, OutboxPublisher>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PolyDbContext>();
    db.Database.EnsureCreated();
}

app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
