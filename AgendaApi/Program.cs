
using agenda.Api.Configurations;
using agenda.Application.Interfaces;
using agenda.Application.Services;
using Agenda.Application.DTOS.InputModels;
using Agenda.Application.Interfaces;
using Agenda.Application.Mappings;
using Agenda.Application.Services;
using Agenda.Application.Validators;
using Agenda.Infrastructure.Data;
using Agenda.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AgendaDbContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));


builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddScoped<IContatoService, ContatoService>();
builder.Services.AddScoped<IValidator<CreateContatoInputModel>, ContatoInputValidator>();
builder.Services.AddScoped<IValidator<UpdateContatoInput>, UpdateContatoInputValidator>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Agenda API",
        Version = "v1",
        Description = "API para gerenciamento de contatos em uma agenda.",
        Contact = new OpenApiContact
        {
            Name = "Sandokan Alves",
            Email = "sandokan.alves.oliveira@example.com",
            Url = new Uri("https://github.com/Sandokanalves/AgendaApi")
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no formato: Bearer {seu-token-here}"
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
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});


builder.Services.AddAutoMapper(typeof(ContatoMappingProfile));

builder.Services.AddFluentValidationAutoValidation() 
    .AddFluentValidationClientsideAdapters() 
    .AddValidatorsFromAssemblyContaining<Program>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]))
        };
    });
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Agenda API v1"));

}
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.Run();
