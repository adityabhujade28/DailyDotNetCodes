using TicTacToe.Api.Services;
using TicTacToe.Api.Services.Interfaces;
using TicTacToe.Api.Stores;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Api.Data;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "TicTacToe API", Version = "v1" }));

// Configure EF DbContext (SQL Server)
builder.Services.AddDbContext<TicTacToe.Api.Data.AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Server=(localdb)\\mssqllocaldb;Database=TicTacToeDb;Trusted_Connection=True;MultipleActiveResultSets=true"));

// Register stores and services
builder.Services.AddScoped<TicTacToe.Api.Stores.IUserStore, TicTacToe.Api.Stores.EfUserStore>();
builder.Services.AddScoped<TicTacToe.Api.Stores.IGameStore, TicTacToe.Api.Stores.EfGameStore>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IGameService, GameService>();

var app = builder.Build();

// Serve frontend static files from ../../Frontend (project layout: Tic-Tac-Toe/Backend/TicTacToe.Api)
var frontendPath = Path.Combine(builder.Environment.ContentRootPath, "..", "..", "Frontend");
Console.WriteLine($"Resolved frontendPath={frontendPath} (exists={Directory.Exists(frontendPath)})");
if (Directory.Exists(frontendPath))
{
    var provider = new PhysicalFileProvider(frontendPath);
    app.UseDefaultFiles(new DefaultFilesOptions { FileProvider = provider, DefaultFileNames = new List<string> { "index.html" } });
    app.UseStaticFiles(new StaticFileOptions { FileProvider = provider });

    // ensure root returns index.html explicitly
    app.MapGet("/", async ctx =>
    {
        var file = Path.Combine(frontendPath, "index.html");
        if (File.Exists(file)) await ctx.Response.SendFileAsync(file);
        else ctx.Response.StatusCode = 404;
    });
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();