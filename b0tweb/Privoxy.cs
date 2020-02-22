using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace b0tweb
{
    class Privoxy
    {
        /// <summary>
        /// The process to bind the Privoxy to.
        /// </summary>
        private Process _process;

        /// <summary>
        /// The SubPath to the Privoxy executable.
        /// </summary>
        private const string PrivoxyPath = @"\Privoxy\privoxy.exe";

        /// <summary>
        /// Constructs a new Privoxy.
        /// </summary>
        public Privoxy()
        {
            this._process = new Process();
        }

        /// <summary>
        /// Setup the proxy, the connection will be created
        /// on port 8118.
        /// </summary>
        public void Establish()
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.WindowStyle = ProcessWindowStyle.Hidden;

            string basePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            info.FileName = basePath + Privoxy.PrivoxyPath;
            info.WorkingDirectory = basePath + @"\Privoxy";

            this._process.StartInfo = info;
            this._process.Start();
        }

        /// <summary>
        /// Disconnect from the proxy by killing the process where
        /// it runs in.
        /// </summary>
        public void Disconnect()
        {
            if (!this._process.HasExited)
            {
                this._process.Kill();
            }
        }
    }
}
