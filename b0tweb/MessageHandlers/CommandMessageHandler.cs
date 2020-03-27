using b0tweb.Commands;
using Meebey.SmartIrc4net;

namespace b0tweb.MessageHandlers
{
    class CommandMessageHandler : AbstractMessageHandler
    {
        /// <summary>
        /// The nickname of the current IRC bot user.
        /// </summary>
        private string _nick;

        public CommandMessageHandler(string nick)
        {
            this._nick = nick;
        }

        protected override void BuildHandler(object sender, IrcEventArgs e)
        {
            string[] parts = e.Data.Message.Split(' ');

            //match *, nick {message} and nick: {message}
            if (parts[0] != "*" && parts[0] != this._nick && parts[0] != this._nick + ":")
            {
                return;
            }

            string response = CommandRegistry.ExecuteSync(e.Data.Message);
            if (e.Data.Irc.JoinedChannels.Count > 0)
            {
                e.Data.Irc.SendMessage(SendType.Message, e.Data.Irc.JoinedChannels[0], response);
            }
        }
    }
}
