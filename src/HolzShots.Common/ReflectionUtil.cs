using System;
using System.Reflection;

namespace HolzShots.Common
{
    static class ReflectionUtil
    {
        /// <summary>
        /// Uses reflection to get the field value from an object.
        /// </summary>
        /// <param name="instance">The instance object.</param>
        /// <param name="fieldName">The field's name which is to be fetched.</param>
        /// <returns>The field value from the object.</returns>
        internal static TField GetInstanceField<TU, TField>(TU instance, string fieldName)
            where TU : class
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            const BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo field = typeof(TU).GetField(fieldName, bindFlags);
            if (field != null)
                return (TField)field.GetValue(instance);
            return default(TField);
        }

        internal static void SetInstanceField<TU, TField>(TU instance, string fieldName, TField value)
            where TU : class
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            const BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo field = typeof(TU).GetField(fieldName, bindFlags);
            if(field != null)
                field.SetValue(instance, value);
            else
                throw new ArgumentException();
        }
    }
}
