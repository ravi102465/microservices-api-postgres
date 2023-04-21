using Film.Integration.API;
using Film.Integration.API.Providers;
using Film.Integration.API.Repositories;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IFilmRepository, FilmRepository>();
builder.Services.AddScoped<IBlobStorageProvider, AzureBlobStorageProvider>();
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<FilmDatabaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("FilmDBCOntextConnection"))
);

builder.Services.AddHealthChecks(builder.Configuration);
builder.Services.AddblobStorageClient(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
    endpoints.MapControllers();
    endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
    endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
    {
        Predicate = r => r.Name.Contains("self")
    });
});
app.Logger.LogInformation($"Connection string {app.Configuration.GetConnectionString("FilmDBCOntextConnection")}");
using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<FilmDatabaseContext>();
await dbContext.Database.MigrateAsync();

app.Run();
