using ChatXApp.Model;
using System;
using System.Collections.ObjectModel;

namespace ChatXApp.Services
{
    public interface IXmppWrapper
    {
        /// <summary>
        /// Retrieve xmpp User Nickname
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Retrieve xmpp Domain
        /// </summary>
        string Domain { get; }

        /// <summary>
        /// Init Xmpp Client
        /// </summary>
        /// <param name="name">user name</param>
        /// <param name="passw">xmpp server password</param>
        /// <param name="domain">xmpp server domain</param>
        /// <param name="port">xmpp server port</param>
        void InitClient(string name, string passw, string domain, int port);

        /// <summary>
        /// Connect the xmpp client to the xmpp server
        /// Note: InitClient shall be called before
        /// </summary>
        void ConnectClient();

        /// <summary>
        /// where received and transmited messages are stored
        /// </summary>
        /// <param name="msgCollection"></param>
        void SetMessageCollection(ObservableCollection<MsgItem> msgCollection);

        /// <summary>
        /// Send message item to xmpp server
        /// </summary>
        /// <param name="msg"></param>
        void Send(MsgItem msg);

        /// <summary>
        /// Set xmpp events for client-server interaction
        /// </summary>
        /// <param name="key"></param>
        /// <param name="ev"></param>
        /// <returns></returns>
        bool SetEvent(string key, EventHandler ev);

        /// <summary>
        /// Remove an event
        /// </summary>
        /// <param name="key">event name to be resetted</param>
        /// <returns></returns>
        bool ResetEvent(string key);
    }
}