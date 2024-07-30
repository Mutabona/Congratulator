using Congratulator.API.Controllers;
using Congratulator.AppServices.Persons.Repositories;
using Congratulator.AppServices.Persons.Services;
using Congratulator.ComponentRegistrar;
using Congratulator.Contracts.Persons;
using Congratulator.DataAccess;
using Congratulator.DataAccess.Persons.Repository;
using Congratulator.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Congratulator API", Version = "V1" });
    options.IncludeXmlComments(Path.Combine(Path.Combine(AppContext.BaseDirectory, $"{typeof(PersonController).Assembly.GetName().Name}.xml")));
    options.IncludeXmlComments(Path.Combine(Path.Combine(AppContext.BaseDirectory, $"{typeof(PersonDto).Assembly.GetName().Name}.xml")));
    options.IncludeXmlComments(Path.Combine(Path.Combine(AppContext.BaseDirectory, $"{typeof(AddPersonRequest).Assembly.GetName().Name}.xml")));
});

builder.Services.AddServices();
builder.Services.AddTransient<IPersonService, PersonService>();

builder.Services.AddScoped<IPersonRepository, PersonRepository>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddScoped<DbContext>(s => s.GetRequiredService<ApplicationDbContext>());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
