using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Matrix.Xmpp;
using Matrix.Xmpp.Client;

namespace ChatApp_v0._1._0
{
    [Activity(Label = "ChatRoomActivity")]
    public class ChatRoomActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_chatRoom);
            
            EditText etMsgToSend = FindViewById<EditText>(Resource.Id.etMsgToSend);
            Button btnSendMsg = FindViewById<Button>(Resource.Id.btnMsgToSend);
            ListView lvChatHistory = FindViewById<ListView>(Resource.Id.lvChatHistory);

            var chat = new ChatHandler(this, lvChatHistory);

            // Binding receive messages handling
            if (MainActivity.xmppClient != null)
            {
                MainActivity.xmppClient.OnMessage += XmppClientMessageReceived;
            }

            btnSendMsg.Click += (sender, e) => 
            {
                // TODO 
                Toast.MakeText(this.ApplicationContext, "Sending the message", ToastLength.Short).Show();

                var msg = new Matrix.Xmpp.Client.Message
                {
                    Type = MessageType.Chat,
                    To = XmppUtilities.XmppConfiguration.DefaultDomain,
                    Body = etMsgToSend.Text,
                    XHtml = new Matrix.Xmpp.XHtmlIM.Html
                    {
                        Body = new Matrix.Xmpp.XHtmlIM.Body
                        {
                            InnerXHtml = $"<p>{etMsgToSend.Text}</p>"
                        }
                    }
                };
                
                try
                {
                    MainActivity.xmppClient.Send(msg);
                }
                catch(Exception exception)
                {
                    Toast.MakeText(this.ApplicationContext, "Error at sending -> " + exception.Message, ToastLength.Short).Show();
                }
            };
        }

        #region XmppEvents
        private void XmppClientMessageReceived(object sender, MessageEventArgs e)
        {
            //Debug.WriteLine(string.Format("OnMessage from {0}", e.Message.From));
            //Debug.WriteLine(string.Format("Body {0}", e.Message.Body));
            //Debug.WriteLine(string.Format("Type {0}", e.Message.Type));

            Application.SynchronizationContext.Post(_ =>
            {
                Toast.MakeText(this.ApplicationContext, "OnMessage from" + e.Message.From + " : " + e.Message.Body, ToastLength.Short).Show();
            }, null);
        }
        #endregion
    }
}