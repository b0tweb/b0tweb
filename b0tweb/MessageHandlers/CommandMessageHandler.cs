using Meebey.SmartIrc4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using b0tweb.Commands;

namespace b0tweb.MessageHandlers
{
    class CommandMessageHandler : AbstractMessageHandler
    {
        protected override void BuildHandler(object sender, IrcEventArgs e)
        {
            CommandRegistry.ExecuteSync(e.Data.Message);
        }
    }
}
