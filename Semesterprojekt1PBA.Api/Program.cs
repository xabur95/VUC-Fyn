using MediatR;
using Microsoft.EntityFrameworkCore;
using Semesterprojekt1PBA.Application.Features.Users.Commands.CreateAdmin;
using Semesterprojekt1PBA.Application.Helpers;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Interfaces;
using Semesterprojekt1PBA.Infrastructure.Database;
using Semesterprojekt1PBA.Infrastructure.Database.Repositories;
using Semesterprojekt1PBA.Api;
using Semesterprojekt1PBA.Presentation.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddExceptionHandler<ErrorExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITopicRepository, TopicRepository>(); 
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<ISchoolRepository, SchoolRepository>(); 
builder.Services.AddScoped<IClassRepository, ClassRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(MediatorPipelineBehavior<,>));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateAdminCommandHandler).Assembly));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Semesterprojekt1PBA.DatabaseMigration")));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();

app.MapUserEndpoints();
app.Run();