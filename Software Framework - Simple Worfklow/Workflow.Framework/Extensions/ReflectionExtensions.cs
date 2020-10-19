using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Workflow.Framework
{
    /// <summary>
    /// Reflection extensions
    /// </summary>
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Scans assemblies for custom attributes.
        /// </summary>
        /// <typeparam name="T">The type of the custom attribute to search for.</typeparam>
        /// <param name="assemblies">Assemblies which will be searched.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Types which are decorated by custom attribute matching given predicate</returns>
        public static IEnumerable<Type> ScanTypesForCustomAttributes<T>(this IEnumerable<Assembly> assemblies, Func<T, bool> predicate)
            where T : Attribute
        {
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes().ScanTypesForCustomAttributes<T>(predicate))
                {
                    yield return type;
                }
            }
        }

        /// <summary>
        /// Scans types for custom attributes.
        /// </summary>
        /// <typeparam name="T">The type of the custom attribute to search for.</typeparam>
        /// <param name="types">Types which will be searched.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Types which are decorated by custom attribute matching given predicate</returns>
        public static IEnumerable<Type> ScanTypesForCustomAttributes<T>(this IEnumerable<Type> types, Func<T, bool> predicate)
            where T : Attribute
        {
            return types.Where(t => t.GetCustomAttribute<T>() != null && predicate(t.GetCustomAttribute<T>()));
        }
    }
}