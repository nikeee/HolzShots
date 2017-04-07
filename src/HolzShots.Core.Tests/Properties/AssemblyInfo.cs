using HolzShots.Common;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("HolzShots.Core.Tests")]
[assembly: AssemblyProduct("HolzShots.Core.Tests")]
[assembly: AssemblyDescription("HolzShots Core Component Tests")]

[assembly: AssemblyCulture("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyConfiguration("")]

[assembly: Guid("56abb0f5-9c8b-4bc9-9d16-bc9598657b5d")]

[assembly: ComVisible(false)]
[assembly: CLSCompliant(false)]

[assembly: AssemblyCompany(LibraryInformation.PublisherName)]
[assembly: AssemblyCopyright(LibraryInformation.Copyright)]
#if RELEASE && CI_BUILD
[assembly: AssemblyVersion(LibraryInformation.VersionFormal)]
[assembly: AssemblyFileVersion(LibraryInformation.VersionFormal)]
#else
[assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyFileVersion("1.0.0")]
#endif
[assembly: AssemblyInformationalVersion(LibraryInformation.FullVersionString)]

[assembly: InternalsVisibleTo("HolzShots.Core.Tests")]

