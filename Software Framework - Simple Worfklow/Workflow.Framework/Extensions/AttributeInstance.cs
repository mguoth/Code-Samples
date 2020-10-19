using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Workflow.Framework.Extensions
{
    /// <summary>
    /// Attribute instance
    /// </summary>
    /// <typeparam name="T">The type of custom attribute.</typeparam>
    public class AttributeInstance<T>
        where T : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeInstance{T}" /> class.
        /// </summary>
        /// <param name="attribute">The custom attribute.</param>
        /// <param name="type">The type which declares the custom attribute.</param>
        public AttributeInstance(T attribute, Type type)
        {
            this.Attribute = attribute;
            this.Type = type;
        }

        /// <summary>
        /// Gets the type which declares the custom attribute.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Gets the custom attribute.
        /// </summary>
        public T Attribute { get; }
    }
}
