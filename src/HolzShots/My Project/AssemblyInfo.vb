Imports System.Reflection
Imports System.Resources
Imports System.Runtime.InteropServices
Imports HolzShots.Common

<Assembly: AssemblyTitle(LibraryInformation.Name)>
<Assembly: AssemblyProduct(LibraryInformation.Name)>
<Assembly: AssemblyDescription("A screenshot utility")>

<Assembly: AssemblyCulture("")>
<Assembly: AssemblyTrademark("")>
<Assembly: AssemblyConfiguration("")>

<Assembly: Guid("58bacefa-a46d-4a0a-9cd6-4a487149923e")>

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

<Assembly: NeutralResourcesLanguage("de")>
