using System.Reflection;

namespace HolzShots;

static class ReflectionUtil
{
    /// <summary>
    /// Uses reflection to get the field value from an object.
    /// </summary>
    /// <param name="instance">The instance object.</param>
    /// <param name="fieldName">The field's name which is to be fetched.</param>
    /// <returns>The field value from the object.</returns>
    /// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is null.</exception>
    internal static TField? GetInstanceField<TU, TField>(TU instance, string fieldName)
        where TU : class
    {
        ArgumentNullException.ThrowIfNull(instance);

        const BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.NonPublic;
        var field = typeof(TU).GetField(fieldName, bindFlags);
        return field == null
            ? default
            : (TField?)field.GetValue(instance);
    }

    /// <summary>Sets a non-public instance field via reflection.</summary>
    /// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is null.</exception>
    /// <exception cref="System.ArgumentException">The field does not exist.</exception>
    internal static void SetInstanceField<TU, TField>(TU instance, string fieldName, TField value)
        where TU : class
    {
        ArgumentNullException.ThrowIfNull(instance);

        const BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.NonPublic;
        var field = typeof(TU).GetField(fieldName, bindFlags);

        if (field == null)
            throw new ArgumentException($"{nameof(field)} is null");

        field.SetValue(instance, value);
    }
}
