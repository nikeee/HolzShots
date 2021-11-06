using System.IO;

namespace HolzShots
{
    public static class HolzShotsResources
    {
        public async static Task<string> ReadResourceAsString(string name)
        {
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            using var defaultSettingsTemplateStream = asm.GetManifestResourceStream(name);
            using var sr = new StreamReader(defaultSettingsTemplateStream!);
            return await sr.ReadToEndAsync().ConfigureAwait(false);
        }
    }
}
