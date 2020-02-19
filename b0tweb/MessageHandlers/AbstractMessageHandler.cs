using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Meebey.SmartIrc4net;

namespace b0tweb.MessageHandlers
{
    /// <summary>
    /// Class <c>AbstractMessageHandler</c> provides an abstraction over the IrcEventHandler
    /// so that it would be easier to be implemented with our IRCConnectionController
    /// </summary>
    abstract class AbstractMessageHandler
    {
        private IrcEventHandler _eventHandler;

        public AbstractMessageHandler ()
        {
            this._eventHandler = new IrcEventHandler(BuildHandler);
        }

        public IrcEventHandler GetHandler()
        {
            return this._eventHandler;
        }

        /// <summary>
        /// The callback function to be executed during an IrcEvent
        /// </summary>
        protected abstract void BuildHandler(object sender, IrcEventArgs e);
    }
}
