using System;
using System.Collections.ObjectModel;
using ChatXApp.Droid.XmppUtilities;
using ChatXApp.Model;
using ChatXApp.Services;
using Matrix.Xmpp;

[assembly: Xamarin.Forms.Dependency(typeof(XmppWrapper))]
namespace ChatXApp.Droid.XmppUtilities
{
    // TODO: export this class to xamarin.forms...(backdoor)
    public class XmppWrapper : IXmppWrapper
    {
        public string Name => _registerGuest.GuestClient.Username;

        public string Domain => _registerGuest.GuestClient.XmppDomain;

        private RegisterGuest _registerGuest;
        private ObservableCollection<MsgItem> _msgCollection;
        private bool isInit = false;

        public XmppWrapper()
        {

        }

        public void InitClient(string name, string passw, string domain, int port)
        {
            _registerGuest = new RegisterGuest(name, passw, domain, port);

            _registerGuest.MessageReceived = (param) => 
            {
                _msgCollection.Add(
                    new MsgItem(param.Message.Body, 
                    new User(param.Message.From)));
            };

            isInit = true;
        }

        public void ConnectClient()
        {
            if (isInit != null)
            {
                _registerGuest.Connect();
            }
            else
                throw new Exception("Xmpp was not initialized");
        }

        public bool SetEvent(string key, EventHandler ev)
        {
            return _registerGuest.AddEvent(key, ev);
        }

        public bool ResetEvent(string key)
        {
            return _registerGuest.RemoveEvent(key);
        }

        public void Send(MsgItem msg)
        {
            // TODO: time and profile avatar to handle
            _registerGuest.GuestClient.Send(PrepareMessage(msg));
        }

        public void SetMessageCollection(ObservableCollection<MsgItem> msgCollection)
        {
            _msgCollection = msgCollection;
        }

        private Matrix.Xmpp.Client.Message PrepareMessage(MsgItem msg)
        {
            var msgToSend = new Matrix.Xmpp.Client.Message
            {
                Type = MessageType.Chat,
                To = Configs.XmppConfiguration.DefaultDomain,
                Body = msg.Content,
                //Data = msg.Who.ImageUrl,
                XHtml = new Matrix.Xmpp.XHtmlIM.Html
                {
                    Body = new Matrix.Xmpp.XHtmlIM.Body
                    {
                        InnerXHtml = $"<p>{msg.Content}</p>"
                    }
                }
            };

            return msgToSend;
        }
        private Matrix.Xmpp.Client.Message PrepareMessage(MsgItem msg)
        {
            var msgToSend = new Matrix.Xmpp.Client.Message
            {
                Type = MessageType.Chat,
                To = Configs.XmppConfiguration.DefaultDomain,
                Body = msg.Content,
                //Data = msg.Who.ImageUrl,
                XHtml = new Matrix.Xmpp.XHtmlIM.Html
                {
                    Body = new Matrix.Xmpp.XHtmlIM.Body
                    {
                        InnerXHtml = $"<p>{msg.Content}</p>"
                    }
                }
            };

            return msgToSend;
        }
    }
}