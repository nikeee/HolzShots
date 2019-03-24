using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit.Sdk;

namespace HolzShots.Core.Tests
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class FileStringContentDataAttribute : DataAttribute
    {
        public string FileName { get; }
        public FileStringContentDataAttribute(string fileName) => FileName = fileName;

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            if (testMethod == null)
                throw new ArgumentNullException(nameof(testMethod));
            yield return new [] { File.ReadAllText(GetFullFilename(FileName)) };
        }

        private static string GetFullFilename(string filename)
        {
            var executable = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(executable), filename));
        }
    }
}
