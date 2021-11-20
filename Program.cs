using System;
using System.IO;

namespace Projet
{
    class Program
    {
        static void Main(string[] args)
        {
            // Copy from the current directory, include subdirectories.
            DirectoryCopy(@"D:\Source", @"D:\Destination", true);
        }

        private static void DirectoryCopy(string src, string dest, bool copySubDir)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(src);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + src);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(dest);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(dest, file.Name);
                file.CopyTo(tempPath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDir)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(dest, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDir);
                }
            }
        }
    }
}
