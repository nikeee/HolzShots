Imports System.Reflection
Imports System.Runtime.InteropServices
Imports HolzShots.Common

<Assembly: AssemblyTitle("HolzShots.UI")>
<Assembly: AssemblyProduct("HolzShots.UI")>
<Assembly: AssemblyDescription("HolzShots UI Components")>

<Assembly: AssemblyCulture("")>
<Assembly: AssemblyTrademark("")>
<Assembly: AssemblyConfiguration("")>

<Assembly: Guid("91ac979f-105b-4b13-954d-67d476ff1c97")>

<Assembly: ComVisible(False)>
<Assembly: CLSCompliant(False)>

<Assembly: AssemblyCompany(LibraryInformation.PublisherName)>
<Assembly: AssemblyCopyright(LibraryInformation.Copyright)>
#If RELEASE And CI_BUILD Then
<Assembly: AssemblyVersion(LibraryInformation.VersionFormal)>
<Assembly: AssemblyFileVersion(LibraryInformation.VersionFormal)>
#Else
<Assembly: AssemblyVersion("1.0.*")>
<Assembly: AssemblyFileVersion("1.0.0")>
#End If
<Assembly: AssemblyInformationalVersion(LibraryInformation.FullVersionString)>
