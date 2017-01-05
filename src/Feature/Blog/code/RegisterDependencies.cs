namespace Sitecore.Feature.Blog
{
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;
    using Providers;
    using Repositories;
    using DependencyInjection;
    using Extensions;
    using Services;

    public class RegisterDependencies : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IDatabaseProvider, DefaultDatabaseProvider>();
            serviceCollection.AddScoped<IIndexProvider, DefaultIndexProvider>();
            serviceCollection.AddScoped<IRenderingService, RenderingService>();
            serviceCollection.AddScoped<IBlogRepository, BlogRepository>();
            serviceCollection.AddScoped<ITagRepository, TagRepository>();
            serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();
            serviceCollection.AddScoped<IBlogService, BlogService>();
            serviceCollection.AddMvcControllersInCurrentAssembly();
        }
    }
}