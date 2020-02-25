using System.Diagnostics;

namespace b0tweb
{
    /// <summary>
    /// Manages a tor proxy connection.
    /// </summary>
    class TorProxy
    {
        /// <summary>
        /// The process to bind the Tor proxy to.
        /// </summary>
        private Process _process;
        
        /// <summary>
        /// The SubPath to the Tor executable.
        /// </summary>
        private const string TorSubPath = @"\Tor\tor.exe";

        /// <summary>
        /// Constructs a new Tor proxy.
        /// </summary>
        public TorProxy()
        {
            this._process = null;
        }

        /// <summary>
        /// Establish a connection to Tor, the connection will be created
        /// on port 9050.
        /// </summary>
        public void Establish()
        {
            if (Processes.IsRunning("tor")) return;

            this._process = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.WindowStyle = ProcessWindowStyle.Hidden;

            string basePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            info.FileName = basePath + TorProxy.TorSubPath;

            this._process.StartInfo = info;
            this._process.Start();
        }

        /// <summary>
        /// Disconnect from the tor proxy by killing the process where
        /// it runs in.
        /// </summary>
        public void Disconnect()
        {
            if (this._process != null  && !this._process.HasExited)
            {
                this._process.Kill();
            }
        }
    }
}
