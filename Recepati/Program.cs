using Recepati.Code.Models;
using Recepati.Db.Code;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<PublicConn, PublicConn>();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Origins",
                      policy =>
                      {
                          policy.WithOrigins("*")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.


//app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("Origins");
app.MapControllers();

var migrator = new Migrator(new PublicConn());
migrator.RunUpgrade();

app.Run();

