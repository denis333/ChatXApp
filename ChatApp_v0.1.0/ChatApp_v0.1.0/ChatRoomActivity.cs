using System;
using Android.App;
using Android.OS;
using Android.Widget;
using Matrix.Xmpp;
using Matrix.Xmpp.Client;

namespace ChatApp_v0._1._0
{
    [Activity(Label = "ChatRoomActivity")]
    public class ChatRoomActivity : Activity
    {
        ChatHandler chatHandler;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_chatRoom);
            
            EditText etMsgToSend = FindViewById<EditText>(Resource.Id.etMsgToSend);
            Button btnSendMsg = FindViewById<Button>(Resource.Id.btnMsgToSend);
            ListView lvChatHistory = FindViewById<ListView>(Resource.Id.lvChatHistory);

            chatHandler = new ChatHandler(this, lvChatHistory);

            // Binding receive messages handling
            if (MainActivity.xmppClient != null)
            {
                MainActivity.xmppClient.OnMessage += XmppClientMessageReceived;
            }

            btnSendMsg.Click += (sender, e) => 
            {
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
                    chatHandler.AddMessage("You: " + msg.Body);
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
            chatHandler?.AddMessage("From: " + e.Message.From + " " + e.Message.Body);
        }
        #endregion
    }
}