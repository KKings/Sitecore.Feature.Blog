namespace Sitecore.Feature.Blog.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void AddMvcControllersInCurrentAssembly(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMvcControllers(Assembly.GetCallingAssembly());
        }

        /// <summary>
        /// Adds all types implementing <see cref="IController"/> into the <see cref="serviceCollection"/> as Transient filtered by
        /// a list of matching names
        /// </summary>
        /// <param name="serviceCollection">The Service Collection</param>
        /// <param name="assemblyFilters">The Assembly Filters</param>
        public static void AddMvcControllers(this IServiceCollection serviceCollection, params string[] assemblyFilters)
        {
            var assemblyNames = new HashSet<string>(assemblyFilters.Where(filter => !filter.Contains('*')));
            var wildcardNames = assemblyFilters.Where(filter => filter.Contains('*')).ToArray();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(assembly =>
            {
                var nameToMatch = assembly.GetName().Name;
                if (assemblyNames.Contains(nameToMatch)) return true;

                return wildcardNames.Any(wildcard => ServiceCollectionExtensions.IsWildcardMatch(nameToMatch, wildcard));
            })
            .ToArray();

            serviceCollection.AddMvcControllers(assemblies);
        }

        /// <summary>
        /// Adds all types implementing <see cref="IController"/> into the <see cref="serviceCollection"/> as Transient
        /// </summary>
        /// <param name="serviceCollection">The Service Collection</param>
        /// <param name="assemblies">The Assemblies</param>
        public static void AddMvcControllers(this IServiceCollection serviceCollection, params Assembly[] assemblies)
        {
            var controllers = ServiceCollectionExtensions.GetTypesImplementing<IController>(assemblies)
                .Where(controller => controller.Name.EndsWith("Controller", StringComparison.Ordinal));

            foreach (var controller in controllers)
            {
                serviceCollection.AddTransient(controller);
            }
        }

        /// <summary>
        /// Gets all types implementing a <see cref="T"/>
        /// </summary>
        /// <typeparam name="T">The Type</typeparam>
        /// <param name="assemblies">Assemblies</param>
        /// <returns>The Types implementing <see cref="T"/></returns>
        public static Type[] GetTypesImplementing<T>(params Assembly[] assemblies)
        {
            if (assemblies == null || assemblies.Length == 0)
            {
                return new Type[0];
            }

            var targetType = typeof(T);

            return assemblies
                .Where(assembly => !assembly.IsDynamic)
                .SelectMany(ServiceCollectionExtensions.GetExportedTypes)
                .Where(type => !type.IsAbstract && !type.IsGenericTypeDefinition && targetType.IsAssignableFrom(type))
                .ToArray();
        }

        /// <summary>
        /// Gets all types that are publically available outside of the assembly
        /// </summary>
        /// <param name="assembly">The Assembly</param>
        /// <returns>The Types that are public</returns>
        private static IEnumerable<Type> GetExportedTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetExportedTypes();
            }
            catch (NotSupportedException)
            {
                // A type load exception would typically happen on an Anonymously Hosted DynamicMethods
                // Assembly and it would be safe to skip this exception.
                return Type.EmptyTypes;
            }
            catch (ReflectionTypeLoadException ex)
            {
                // Return the types that could be loaded. Types can contain null values.
                return ex.Types.Where(type => type != null);
            }
            catch (Exception ex)
            {
                // Throw a more descriptive message containing the name of the assembly.
                throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture,
                    "Unable to load types from assembly {0}. {1}", assembly.FullName, ex.Message), ex);
            }
        }

        /// <summary>
        /// Checks if a string matches a wildcard argument (using regex)
        /// </summary>
        private static bool IsWildcardMatch(string input, string wildcards)
        {
            return Regex.IsMatch(input, "^" + Regex.Escape(wildcards).Replace("\\*", ".*").Replace("\\?", ".") + "$", RegexOptions.IgnoreCase);
        }
    }
}