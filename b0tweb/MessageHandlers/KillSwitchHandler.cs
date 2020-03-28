using Meebey.SmartIrc4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace b0tweb.MessageHandlers
{
    class KillSwitchHandler : AbstractMessageHandler
    {
        /// <summary>
        /// Key word that should be part of the message to trigger the kill switch
        /// </summary>
        private const string KillKeyword = "life is suffering";

        /// <summary>
        /// The nickname of the current IRC bot user.
        /// </summary>
        private string _nick;

        public KillSwitchHandler(string nick)
        {
            this._nick = nick;
        }

        protected override void BuildHandler(object sender, IrcEventArgs e)
        {
            if (!MessageHandlerHelper.MatchesNick(e.Data.Message, this._nick))
            {
                return;
            }

            if (e.Data.Message.IndexOf(KillKeyword) != -1)
            {
                e.Data.Irc.RfcDie();
                e.Data.Irc.Disconnect();
            }   
        }
    }
}
