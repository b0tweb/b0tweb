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
        const string KillKeyword = "life is suffering";

        protected override void BuildHandler(object sender, IrcEventArgs e)
        {
            if (e.Data.Message.IndexOf(KillKeyword) != -1)
            {
                e.Data.Irc.RfcDie();
                e.Data.Irc.Disconnect();
            }
            
        }
    }
}
