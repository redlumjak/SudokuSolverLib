using Microsoft.EntityFrameworkCore;
using SudokuSolverApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<TurnContext>(opt =>
    opt.UseInMemoryDatabase("SudokuSolver"));
builder.Services.AddDbContext<GameContext>(opt =>
    opt.UseInMemoryDatabase("SudokuSolver"));
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
