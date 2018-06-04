using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace AspNetCore.Jwt.Sample.Helpers
{
    /// <summary>
    /// Enumeration Attribute Helper
    /// </summary>
    public static class EnumAttributeHelper
    {
        /// <summary>
        /// Attempts to get the description attribute value of an enum value
        /// </summary>
        /// <param name="type">Enum type</param>
        /// <param name="value">Enum value as represented by an integer</param>
        /// <returns>Description attribute valuye if the enum value has a description attribute, otherwise empty string</returns>
        public static string GetDescription(this Type type, int value)
        {
            var descriptionAttribute = GetAttribute<DescriptionAttribute>(type, value);
            if (descriptionAttribute == null) return string.Empty;

            return descriptionAttribute.Description;
        }

        /// <summary>
        /// Get attributes of a specific type for an enum field
        /// </summary>
        /// <typeparam name="T">Attribute type</typeparam>
        /// <param name="type">Enum type</param>
        /// <param name="value">Enum value as represented by an integer</param>
        /// <returns>Enumerable collection of the attribute</returns>
        public static IEnumerable<T> GetAttributes<T>(this Type type, int value)
            where T : Attribute
        {
            var enumField = GetEnumField(type, value);
            if (enumField == null) return new List<T>();

            return enumField.GetCustomAttributes<T>();
        }

        /// <summary>
        /// Get attribute of a specific type for an enum field
        /// </summary>
        /// <typeparam name="T">Attribute type</typeparam>
        /// <param name="type">Enum type</param>
        /// <param name="value">Enum value as represented by an integer</param>
        /// <returns>Attribute if it exists, otherwise null</returns>
        public static T GetAttribute<T>(this Type type, int value)
            where T : Attribute
        {
            var enumField = GetEnumField(type, value);
            if (enumField == null) return null;

            return enumField.GetCustomAttribute<T>();
        }

        private static FieldInfo GetEnumField(Type type, int value)
        {
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);
            return fields.FirstOrDefault(i => (int)i.GetValue(null) == value);
        }
    }
}
