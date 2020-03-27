namespace b0tweb.MessageHandlers
{
    /// <summary>
    /// Helper functions for the message handlers.
    /// </summary>
    class MessageHandlerHelper
    {
        /// <summary>
        /// Checks if a given command string matches the nickname.
        /// </summary>
        /// <param name="message">The IRC message.</param>
        /// <param name="nick">The nickname of the bot user.</param>
        /// <returns>If the message is for the given nick.</returns>
        public static bool MatchesNick(string message, string nick)
        {
            string[] parts = message.Split(' ');

            //match *, nick {message} and nick: {message}
            return !(parts.Length < 2 || !(parts[0] == "*" || parts[0] == nick || parts[0] == nick + ":"));
        }
    }
}
