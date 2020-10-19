using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Globalization;
using System.Linq;

namespace Workflow.Framework.Extensions
{
    /// <summary>
    /// Assembly extensions
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Scans types for custom attributes.
        /// </summary>
        /// <typeparam name="T">The type of the custom attribute to search for.</typeparam>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        public static IEnumerable<AttributeInstance<T>> ScanTypesForCustomAttributes<T>(this Assembly assembly)
            where T : Attribute
        {
            return assembly.GetTypes().ScanTypesForCustomAttributes<T>();
        }

        /// <summary>
        /// Scans types for custom attributes.
        /// </summary>
        /// <typeparam name="T">The type of the custom attribute to search for.</typeparam>
        /// <param name="assembly">The assembly.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public static IEnumerable<AttributeInstance<T>> ScanTypesForCustomAttributes<T>(this Assembly assembly, Func<T, bool> predicate)
            where T : Attribute
        {
            return assembly.GetTypes().ScanTypesForCustomAttributes<T>(predicate);
        }

        /// <summary>
        /// Scans types for custom attributes.
        /// </summary>
        /// <typeparam name="T">The type of the custom attribute to search for.</typeparam>
        /// <param name="assemblies">The assemblies.</param>
        /// <returns></returns>
        public static IEnumerable<AttributeInstance<T>> ScanTypesForCustomAttributes<T>(this IEnumerable<Assembly> assemblies)
            where T : Attribute
        {
            foreach (Assembly assembly in assemblies)
            {
                foreach (AttributeInstance<T> attribute in assembly.ScanTypesForCustomAttributes<T>())
                {
                    yield return attribute;
                }
            }
        }

        /// <summary>
        /// Scans types for custom attributes.
        /// </summary>
        /// <typeparam name="T">The type of the custom attribute to search for.</typeparam>
        /// <param name="assemblies">The assemblies.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public static IEnumerable<AttributeInstance<T>> ScanTypesForCustomAttributes<T>(this IEnumerable<Assembly> assemblies, Func<T, bool> predicate)
            where T : Attribute
        {
            foreach (Assembly assembly in assemblies)
            {
                foreach (AttributeInstance<T> attribute in assembly.ScanTypesForCustomAttributes<T>(predicate))
                {
                    yield return attribute;
                }
            }
        }
    }
}