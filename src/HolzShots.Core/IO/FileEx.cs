using System.IO;

namespace HolzShots.IO
{
    public static class FileEx
    {
        /// <summary> Same as System.IO.File.Move, but appends (2) etc when the file already exists. </summary>
        public static void MoveAndRenameInsteadOfOverwrite(string sourceFileName, string destFileName)
        {
            // This entire code contains race conditions that may fail under extreme circumstances.
            // But we're living with this and do not implement atomic operations.
            if (!File.Exists(destFileName))
            {
                File.Move(sourceFileName, destFileName);
                return;
            }

            int copyCounter = 1;
            while (File.Exists(DeriveFileNameWithCounter(destFileName, copyCounter)))
                ++copyCounter;

            var newDestFileName = DeriveFileNameWithCounter(destFileName, copyCounter);
            File.Move(sourceFileName, newDestFileName);
        }

        public static string DeriveFileNameWithCounter(string fileName, int counter)
        {
            var path = Path.GetDirectoryName(fileName);

            var extension = Path.GetExtension(fileName);
            var newFileName = Path.GetFileNameWithoutExtension(fileName) + $" ({counter})";

            var fileNameWithExtension = newFileName + extension;
            return path == null
                    ? fileNameWithExtension
                    : Path.Combine(path, fileNameWithExtension);
        }
    }
}
