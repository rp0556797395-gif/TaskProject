using Proyect;
using Proyect.Core.Repositories;
using Proyect.Core.Services;
using Proyect.Core.Models;
using Proyect.Data.Repoistories;
using Proyect.Service;
using Proyect.Data;
using Proyect.Core;
//1HOME
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IClientService, ClientServise>();
builder.Services.AddScoped<ITaskService, TaskServise>();
builder.Services.AddScoped<IClientRepositories, ClientRepository>();
builder.Services.AddScoped<ITaskReositories, TaskRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddDbContext<DataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();
app.UseShabbatMiddleware();
app.UseAuthorization();

app.MapControllers();

app.Run();
