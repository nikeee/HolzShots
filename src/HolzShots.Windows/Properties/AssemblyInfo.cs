using HolzShots;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("HolzShots.Windows")]
[assembly: AssemblyProduct("HolzShots.Windows")]
[assembly: AssemblyDescription("HolzShots Core Components")]

[assembly: AssemblyCulture("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyConfiguration("")]

[assembly: Guid("4f1b3465-2915-4e00-bb15-fc1808b01cd6")]

[assembly: ComVisible(false)]
[assembly: CLSCompliant(false)]

[assembly: AssemblyCompany(LibraryInformation.PublisherName)]
[assembly: AssemblyCopyright(LibraryInformation.Copyright)]
#if RELEASE && CI_BUILD
[assembly: AssemblyVersion(LibraryInformation.VersionFormal)]
[assembly: AssemblyFileVersion(LibraryInformation.VersionFormal)]
#else
[assembly: AssemblyVersion("1.0.0")]
[assembly: AssemblyFileVersion("1.0.0")]
#endif
[assembly: AssemblyInformationalVersion(LibraryInformation.FullVersionString)]

[assembly: InternalsVisibleTo("HolzShots.Core.Tests")]
