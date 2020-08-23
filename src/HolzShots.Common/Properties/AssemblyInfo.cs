using HolzShots;
using System;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("HolzShots.Common")]
[assembly: AssemblyProduct("HolzShots.Common")]
[assembly: AssemblyDescription("HolzShots Common Library")]

[assembly: AssemblyCulture("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyConfiguration("")]

[assembly: Guid("16ba7b52-3ccc-461e-802e-2016571c0ecc")]

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
