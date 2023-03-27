using Lagalt_Backend.Models;
using Lagalt_Backend.Services.Messages;
using Lagalt_Backend.Services.ImageServices;
using Lagalt_Backend.Services.ApplicationServices;
using Lagalt_Backend.Services.Projects;
using Lagalt_Backend.Services.UserServices;
using Microsoft.EntityFrameworkCore;
using Lagalt_Backend.Services.Skills;
using Lagalt_Backend.Services.PortfolioServices;
using Lagalt_Backend.Services.Tags;
using Lagalt_Backend.Services.Links;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<LagAltDbContext>(
                opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("lagalt"))
            );
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IProjectService, ProjectService>();
builder.Services.AddTransient<IMessageService, MessageService>();
builder.Services.AddTransient<IImageService, ImageService>();
builder.Services.AddTransient<ISkillService, SkillService>();
builder.Services.AddTransient<ITagService, TagService>();
builder.Services.AddTransient<IPortfolioService, PortfolioService>();
builder.Services.AddTransient<IApplicationService, ApplicationService>();
builder.Services.AddTransient<ILinkService, LinkService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:audience"],
            ValidIssuer = builder.Configuration["JWT:issuer"],
            IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
            {
                var client = new HttpClient();
                var keyuri = builder.Configuration["JWT:key-url"];
                var response = client.GetAsync(keyuri).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(responseString);
                return keys.Keys;
            }
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
