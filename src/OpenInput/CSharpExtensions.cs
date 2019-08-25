namespace OpenInput
{
    using System;
    using System.Reflection;

    internal static class CSharpExtensions
    {
        public static bool HasCustomAttribute<T>(this PropertyInfo propertyInfo)
            where T : Attribute
        {
            return propertyInfo.GetCustomAttributes(typeof(T), true).Length > 0;
        }

        public static T GetCustomAttribute<T>(this PropertyInfo propertyInfo)
            where T : Attribute
        {
            return (T)propertyInfo.GetCustomAttribute(typeof(T), true);
        }
    }
}