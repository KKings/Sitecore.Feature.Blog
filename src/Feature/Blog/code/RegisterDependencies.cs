namespace Sitecore.Feature.Blog
{
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;
    using Providers;
    using Repositories;
    using DependencyInjection;
    using Extensions;

    public class RegisterDependencies : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IDatabaseProvider, DefaultDatabaseProvider>();
            serviceCollection.AddScoped<IIndexProvider, DefaultIndexProvider>();
            serviceCollection.AddScoped<IBlogRepository, BlogRepository>();
            serviceCollection.AddMvcControllersInCurrentAssembly();
        }
    }
}