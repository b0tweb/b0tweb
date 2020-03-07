using Meebey.SmartIrc4net;

namespace b0tweb.MessageHandlers
{
    /// <summary>
    /// Class <c>AbstractMessageHandler</c> provides an abstraction over the IrcEventHandler
    /// so that it would be easier to be implemented with our IRCConnectionController
    /// </summary>
    abstract class AbstractMessageHandler
    {
        /// <summary>
        /// The irc event handler
        /// </summary>
        private IrcEventHandler _eventHandler;

        /// <summary>
        /// Initializes the <c>IrcEventHandler</c> providing <see cref="BuildHandler"/> method as callback
        /// </summary>
        public AbstractMessageHandler()
        {
            this._eventHandler = new IrcEventHandler(BuildHandler);
        }

        /// <summary>
        /// Getter for the SmartIrc4Net <c>IrcEventHandler</c> defined by this message handler
        /// </summary>
        /// <returns>SmartIrc4Net handler defined by this Message Handler</returns>
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
