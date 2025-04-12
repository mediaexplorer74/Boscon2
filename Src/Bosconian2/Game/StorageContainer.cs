using System;
using System.IO;

namespace Starkiller
{
    internal class StorageContainer
    {
        private Stream fileStream;
        internal Stream CreateFile(string filename)
        {
            // Fix for CS0161: Ensure all code paths return a value.  
            fileStream = File.Create(filename);
            return fileStream;
        }

        internal void DeleteFile(string filename)
        {
            File.Delete(filename);
        }

        internal void Dispose()
        {
            fileStream.Dispose();
        }

        internal bool FileExists(string filename)
        {
            return File.Exists(filename);
        }

        internal Stream OpenFile(string filename, FileMode open)
        {
            return File.Open(filename, open);
        }
    }
}