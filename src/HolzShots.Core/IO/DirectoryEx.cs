using System.IO;
using System.Threading.Tasks;

namespace HolzShots.IO
{
    public static class DirectoryEx
    {

        /// <summary>
        /// We are doing this synchronously, assuming the application is not located on a network drive.
        /// See: https://stackoverflow.com/a/20596865
        /// </summary>
        /// <exception cref="System.UnauthorizedAccessException" />
        /// <exception cref="System.IO.PathTooLongException" />
        public static void EnsureDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public static Task<bool> ExistsAsync(string path) => Task.Run(() => Directory.Exists(path));
    }
}
