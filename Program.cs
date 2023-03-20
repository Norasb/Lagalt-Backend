using Lagalt_Backend.Models;
using Lagalt_Backend.Services.Messages;
using Lagalt_Backend.Services.ImageServices;
using Lagalt_Backend.Services.Projects;
using Lagalt_Backend.Services.UserServices;
using Microsoft.EntityFrameworkCore;
using Lagalt_Backend.Services.Skills;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<LagAltDbContext>(
                opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("lagalt"))
            );

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IProjectService, ProjectService>();
builder.Services.AddTransient<IMessageService, MessageService>();
builder.Services.AddTransient<IImageService, ImageService>();
builder.Services.AddTransient<ISkillService, SkillService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
