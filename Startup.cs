using Imagegram.Functions.Repositories;
using Imagegram.Functions.Repositories.Entities;
using Imagegram.Functions.Storages;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(Imagegram.Functions.Startup))]
namespace Imagegram.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();

            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<IRepository<PostResource>, Repository<PostResource>>();
            builder.Services.AddScoped<IRepository<Comment>, Repository<Comment>>();
            builder.Services.AddScoped<IRepository<ResourceType>, Repository<ResourceType>>();


            if (Environment.GetEnvironmentVariable("RunningEnvironment") == "AzureEnv")
            {
                builder.Services.AddScoped<IStorage, AzureBlobStorage>();
            }
            else
            {
                builder.Services.AddScoped<IStorage, LocalStorage>();
            }
        }
    }
}
