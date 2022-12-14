using Microsoft.Extensions.DependencyInjection.Extensions;
using Recepati.Code.Models;
using Recepati.Database;
using Recepati.Db.Code;
using Recepati.Managers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<PublicConn, PublicConn>();
builder.Services.AddScoped<DB_Ingredient, DB_Ingredient>();
builder.Services.AddScoped<IngredientManager, IngredientManager>();
builder.Services.AddScoped<DB_Recipe, DB_Recipe>();
builder.Services.AddScoped<RecipeManager, RecipeManager>();
builder.Services.AddScoped<DB_User, DB_User>();
builder.Services.AddScoped<UserManager, UserManager>();
builder.Services.AddScoped<FridgeManager, FridgeManager>();
builder.Services.AddScoped<DB_Fridge, DB_Fridge>();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<SecurityManager, SecurityManager>();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
                      policy =>
                      {
                          policy.WithOrigins("http://127.0.0.1:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                      });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors();
}

app.MapControllers();

var migrator = new Migrator(new PublicConn());
migrator.RunUpgrade();

app.Run();

