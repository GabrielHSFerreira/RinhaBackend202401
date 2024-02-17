using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RinhaBackend202401.WebApi.Contexts;
using Serilog;
using System;
using System.Text.Json;

namespace RinhaBackend202401.WebApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            CriarLoggerGlobal(builder.Environment);

            try
            {
                builder.Services.AddControllers()
                        .AddJsonOptions(options =>
                        {
                            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
                        });
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
                builder.Services.AddDbContext<RinhaContext>(options =>
                    options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));
                builder.Host.UseSerilog();

                var app = builder.Build();

                EFWarmUp(app);

                app.UseSerilogRequestLogging();
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }
                app.UseAuthorization();
                app.MapControllers();
                app.Run();
            }
            catch (Exception ex)
            {
                Log.Logger.Fatal(ex, "Application crashed.");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static void CriarLoggerGlobal(IWebHostEnvironment environment)
        {
            var configuration = new LoggerConfiguration();

            if (environment.IsProduction())
                configuration.MinimumLevel.Error();

            Log.Logger = configuration
                .WriteTo.Console()
                .CreateLogger();
        }

        private static void EFWarmUp(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            using var context = scope.ServiceProvider.GetService<RinhaContext>();
            _ = context!.Model;
        }
    }
}