﻿using b0tweb.Commands;
using Meebey.SmartIrc4net;

namespace b0tweb.MessageHandlers
{
    class CommandMessageHandler : AbstractMessageHandler
    {
        protected override void BuildHandler(object sender, IrcEventArgs e)
        {
            string response = CommandRegistry.ExecuteSync(e.Data.Message);

            if (e.Data.Irc.JoinedChannels.Count > 0)
            {
                e.Data.Irc.SendMessage(SendType.Message, e.Data.Irc.JoinedChannels[0], response);
            }
        }
    }
}
