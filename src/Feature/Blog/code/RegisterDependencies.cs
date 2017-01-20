namespace Sitecore.Feature.Blog
{
    using Caching;
    using Microsoft.Extensions.DependencyInjection;
    using Providers;
    using Repositories;
    using DependencyInjection;
    using Extensions;
    using Resolvers;
    using Resolvers.Configuration;
    using Search;
    using Search.Results;
    using Services;
    using Tokens;
    using Tokens.Configuration;

    public class RegisterDependencies : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ITokenService, TokenService>();
            serviceCollection.AddScoped<IResolverService, ResolverService>();
            serviceCollection.AddSingleton<IResolverReader, ResolverReader>();
            serviceCollection.AddSingleton<ITokenReader, TokenReader>();
            serviceCollection.AddTransient<BaseTransientCache, TransientCache>();
            serviceCollection.AddScoped<IDatabaseProvider, DefaultDatabaseProvider>();
            serviceCollection.AddTransient<IIndexProvider, DefaultIndexProvider>();
            serviceCollection.AddScoped<IPaginationService, PaginationService>();
            serviceCollection.AddScoped<IRenderingService, RenderingService>();
            serviceCollection.AddScoped<IBlogContextRepository, BlogContextRepository>();
            serviceCollection.AddTransient<IContentRespository<SearchResultItem>, DefaultContentRepository<SearchResultItem>>();
            serviceCollection.AddTransient<IContentRespository<BlogSearchResultItem>, DefaultContentRepository<BlogSearchResultItem>>();
            serviceCollection.AddTransient<IContentRespository<TagSearchResultItem>, DefaultContentRepository<TagSearchResultItem>>();
            serviceCollection.AddTransient<IContentRespository<CategorySearchResultItem>, DefaultContentRepository<CategorySearchResultItem>>();
            serviceCollection.AddSingleton<ILocatorService, LocatorService>();
            serviceCollection.AddTransient<IBlogService, BlogService>();
            serviceCollection.AddTransient<IBlogPostService, BlogPostService>();
            serviceCollection.AddTransient<ITagService, TagService>();
            serviceCollection.AddTransient<ICategoryService, CategoryService>();


            serviceCollection.AddMvcControllersInCurrentAssembly();
        }
    }
}