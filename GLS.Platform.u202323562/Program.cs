using Cortex.Mediator;
using GLS.Platform.u202323562.Contexts.Assignments.Application.CommandServices;
using GLS.Platform.u202323562.Contexts.Assignments.Application.EventHandlers;
using GLS.Platform.u202323562.Contexts.Assignments.Application.QueryServices;
using GLS.Platform.u202323562.Contexts.Assignments.Domain.Repositories;
using GLS.Platform.u202323562.Contexts.Assignments.Domain.Services;
using GLS.Platform.u202323562.Contexts.Assignments.Infrastructure.Persistence.Repositories;
using GLS.Platform.u202323562.Contexts.Shared.Domain.Repositories;
using GLS.Platform.u202323562.Contexts.Shared.Infrastructure.Persistence.Configuration;
using GLS.Platform.u202323562.Contexts.Shared.Infrastructure.Repositories;
using GLS.Platform.u202323562.Contexts.Tracking.Application.CommandServices;
using GLS.Platform.u202323562.Contexts.Tracking.Domain.Repositories;
using GLS.Platform.u202323562.Contexts.Tracking.Domain.Services;
using GLS.Platform.u202323562.Contexts.Tracking.Infrastructure.Persistence.Repositories;
using GLS.Platform.u202323562.Contexts.Tracking.Interfaces.REST.ACL;
using GLS.Platform.u202323562.Contexts.Tracking.Interfaces.REST.ACL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "GLS Platform API",
        Description = "RESTful API for GLS RocketPad Pro platform",
        Contact = new OpenApiContact
        {
            Name = "Oliver Villogas Medina",
            Email = "u202323562@upc.edu.pe"
        }
    });
    
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<GLSContext>(options =>
{
    options.UseMySQL(connectionString);

    if (builder.Environment.IsDevelopment())
        options.LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    else if (builder.Environment.IsProduction())
        options.LogTo(Console.WriteLine, LogLevel.Error)
            .EnableDetailedErrors();
});

// Cortex.Mediator - IMPORTANTE: Registrar antes de los servicios
builder.Services.AddMediator(options =>
{
    // Register event handlers from the assembly
    options.AddEventHandlersFromAssembly(typeof(DataRecordRegisteredEventHandler).Assembly);
});

// Shared
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Tracking Context
builder.Services.AddScoped<IDataRecordRepository, DataRecordRepository>();
builder.Services.AddScoped<IDataRecordCommandService, DataRecordCommandService>();
builder.Services.AddScoped<IAssignmentsContextFacade, AssignmentsContextFacade>();

// Assignments Context
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IDeviceQueryService, DeviceQueryService>();
builder.Services.AddScoped<IDeviceCommandService, DeviceCommandService>();

// Event Handler - IMPORTANTE: Registrar explícitamente
builder.Services.AddScoped<DataRecordRegisteredEventHandler>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<GLSContext>();
    context.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GLS Platform API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();