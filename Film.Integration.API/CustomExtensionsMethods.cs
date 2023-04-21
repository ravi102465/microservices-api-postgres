using Azure.Core;
using Film.Integration.API;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Film.Integration.API
{
    public static class CustomExtensionsMethods
    {
        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());

            hcBuilder.AddDbContextCheck<FilmDatabaseContext>();
            return services;
        }

        public static IServiceCollection AddblobStorageClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAzureClients(builder =>
            {
                builder.AddBlobServiceClient(configuration.GetConnectionString("BlobStorageConnectionString"))
               .ConfigureOptions(options =>
                    {
                        options.Retry.Mode = RetryMode.Exponential;
                        options.Retry.MaxRetries = 3;
                    }).WithName("filmBlobStorage");
                });

            return services;
        }
    }
}
