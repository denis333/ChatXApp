using System;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Register;

namespace ChatApp_v0._1._0.XmppUtilities
{
    public class RegisterGuest
    {
        private XmppClient guestClient;

        public RegisterGuest(string username)
        {
            guestClient = new XmppClient
            {
                Username = username,
                Password = XmppConfiguration.DefaultGuestPassword,
                XmppDomain = XmppConfiguration.DefaultDomain
            };

            Initialize();
        }

        public RegisterGuest(XmppClient xmppClient)
        {
            if (xmppClient == null) throw new NullReferenceException();

            guestClient = xmppClient;

            Initialize();
        }

        private void Initialize()
        {
            guestClient.OnRegister += new EventHandler<Matrix.EventArgs>(xmppClient_OnRegister);
            guestClient.OnRegisterInformation += new EventHandler<RegisterEventArgs>(xmppClient_OnRegisterInformation);
            guestClient.OnRegisterError += new EventHandler<IqEventArgs>(xmppClient_OnRegisterError);

            guestClient.RegisterNewAccount = true;
        }

        #region RegistrationEvents
        private void xmppClient_OnRegisterInformation(object sender, RegisterEventArgs e)
        {
            e.Register.RemoveAll<Matrix.Xmpp.XData.Data>();

            e.Register.Username = guestClient.Username;
            e.Register.Password = guestClient.Password;
        }

        private void xmppClient_OnRegister(object sender, EventArgs e)
        {
            // registration was successful
            // TODO
        }

        private void xmppClient_OnRegisterError(object sender, IqEventArgs e)
        {
            // registration failed.
            // TODO
            guestClient.Close();
        }
        #endregion
    }
}