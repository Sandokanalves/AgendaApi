
using Agenda.Application.DTOS.InputModels;
using Agenda.Application.Mappings;
using Agenda.Application.Services;
using Agenda.Application.Validators;
using Agenda.Domain.Interfaces;
using Agenda.Infrastructure.Data;
using Agenda.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AgendaDbContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));


builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddScoped<IContatoService, ContatoService>();
builder.Services.AddScoped<IValidator<CreateContatoInputModel>, ContatoInputValidator>();
builder.Services.AddScoped<IValidator<UpdateContatoInput>, UpdateContatoInputValidator>();




// Add services to the container.

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
});

builder.Services.AddAutoMapper(typeof(ContatoMappingProfile));

builder.Services.AddFluentValidationAutoValidation() 
    .AddFluentValidationClientsideAdapters() 
    .AddValidatorsFromAssemblyContaining<Program>();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Agenda API v1"));

}
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

// Aplicar as migrações automaticamente ao iniciar o projeto
//using (var scope = app.Services.CreateScope())
//{
    //var dbContext = scope.ServiceProvider.GetRequiredService<AgendaDbContext>();
    //dbContext.Database.Migrate();
//}


app.Run();
