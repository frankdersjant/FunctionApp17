using DAL;
using FunctionApp17;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: WebJobsStartup(typeof(StartUp))]

namespace FunctionApp17
{
    public class StartUp : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            var config = new ConfigurationBuilder()
                         .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                         .AddEnvironmentVariables()
                         .Build();

            builder.Services.AddDbContext<ApplicationDbContext>(options1 =>
            {
                options1.UseSqlServer(
                  config["ConnectionStrings:DefaultConnection"],
                  builder =>
                  {
                      builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                      builder.CommandTimeout(10);
                  }
                );
            });
        }
    }
}
