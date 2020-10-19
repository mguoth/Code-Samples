using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Workflow.Framework.Extensions
{
    /// <summary>
    /// Type extensions
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Scans types for custom attributes.
        /// </summary>
        /// <typeparam name="T">The type of the custom attribute to search for.</typeparam>
        /// <param name="types">The types.</param>
        /// <returns></returns>
        public static IEnumerable<AttributeInstance<T>> ScanTypesForCustomAttributes<T>(this IEnumerable<Type> types)
            where T : Attribute
        {
            return types.Select(t => new AttributeInstance<T>(t.GetCustomAttribute<T>(), t))
                .Where(t => t.Attribute != null);
        }

        /// <summary>
        /// Scans types for custom attributes.
        /// </summary>
        /// <typeparam name="T">The type of the custom attribute to search for.</typeparam>
        /// <param name="types">The types.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public static IEnumerable<AttributeInstance<T>> ScanTypesForCustomAttributes<T>(this IEnumerable<Type> types, Func<T, bool> predicate)
            where T : Attribute
        {
            return types.Select(t => new AttributeInstance<T>(t.GetCustomAttribute<T>(), t))
                .Where(t => t.Attribute != null && predicate(t.Attribute));
        }
    }
}
