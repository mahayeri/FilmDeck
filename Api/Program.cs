using Api;
using Api.Shared.Slices;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container
builder.Services.AddProblemDetails();
builder.Services.AddHttpClient();

// Add custom services
builder.Services.RegisterApplicationServices();
builder.Services.RegisterPersistenceServices(builder.Configuration);

builder.Services.AddCors();
var app = builder.Build();

app.UseHttpsRedirection();
// Configure development env
if (app.Environment.IsDevelopment())
{
}

app.UseCors(opt =>
{
    opt.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        .WithOrigins("https://localhost:3000");
});

app.MapSliceEndpoints();

app.Run();
