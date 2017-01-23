// MIT License
// 
// Copyright (c) 2017 Kyle Kingsbury
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
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