using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PartyRoom.Infrastructure.Data;
using PartyRoom.WebAPI.Extensions;
using PartyRoom.WebAPI.Helpers;
using PartyRoom.WebAPI.MappingProfiles;
using PartyRoom.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomIdentity();
builder.Services.AddCustomAuthentication(jwtSettings);
builder.Services.AddCustomAuthorization();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    // �������� �������� ��� ����������� JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
});
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddAutoMapper(typeof(UserMappingProfile),typeof(RoomMappingProfile));
builder.Services.AddRepositories();
builder.Services.AddServices();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PartyRoom API v1");
        c.DocumentTitle = "PartyRoom API Documentation";
        c.DefaultModelsExpandDepth(-1);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
