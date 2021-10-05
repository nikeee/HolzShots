using System.IO;

namespace HolzShots.IO
{
    public static class FileEx
    {
        /// <summary> Same as System.IO.File.Move, but appends (2) etc when the file already exists. </summary>
        public static void MoveAndRenameInsteadOfOverwrite(string sourceFileName, string destFileName)
        {
            var newDestFileName = GetUnusedFileNameFromCandidate(destFileName);
            File.Move(sourceFileName, newDestFileName);
        }

        /// <summary>Gets a file name that is not in use by appending (2) etc when the file name already exists. </summary>
        public static string GetUnusedFileNameFromCandidate(string fileName)
        {
            // This entire code contains race conditions that may fail under extreme circumstances.
            // But we're living with this and do not implement atomic operations.
            if (!File.Exists(fileName))
                return fileName;


            int copyCounter = 1;
            string unusedFileName;
            do
            {
                unusedFileName = DeriveFileNameWithCounter(fileName, copyCounter);
                ++copyCounter;
            }
            while (File.Exists(unusedFileName));

            return unusedFileName;
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
