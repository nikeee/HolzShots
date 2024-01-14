using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit.Sdk;

namespace HolzShots.Core.Tests;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public sealed class FileStringContentDataAttribute(params string[] fileNames) : DataAttribute
{
    public IReadOnlyCollection<string> FileNames { get; } = fileNames;

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        ArgumentNullException.ThrowIfNull(testMethod);
        yield return FileNames
            .Select(f => File.ReadAllText(GetFullFilename(f)))
            .ToArray();
    }

    private static string GetFullFilename(string filename)
    {
        var executable = new Uri(Assembly.GetExecutingAssembly().Location).LocalPath!;
        return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(executable)!, filename));
    }
}
