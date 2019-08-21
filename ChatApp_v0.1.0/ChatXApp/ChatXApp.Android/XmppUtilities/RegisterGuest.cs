using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Matrix;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Register;

namespace ChatXApp.Droid.XmppUtilities
{
    /// <summary>
    /// Class responsible to wrapp the access to the xmpp library itself
    /// </summary>
    public class RegisterGuest
    {
        // Insert the license for Matrix.Android XMPP library
        private static string lic = @"eJxkkNtSIjEQhl/F8pbaTTgMh602pcAggzirCzjoXWACZsgkIQcYfPodFc83
                         Xf3314e/GsZ8yaRlJ0UupD07petfVq3cnhr2R7yiUwI3RqV+6aKUTJxPuQL0
                         UYFbT6Xj7kCqgN5z6HnrVM4MgZjmjIQ7Kjx1ygB60dBTuaby8Aa4kidHK4De
                         GIQ55YJYKpg9/+Tsd1o2vbKy+f3QTKfUsbDQ3LB+mZEarmHcxg1APxBEts9y
                         RZzx5a6jgOf4Zb7awR1cB/QNwISvJXXeMJIno9UsamSrcVhk89Fcdbflg7ob
                         fNg0J7u5HS0oMvR+lk5XA7UZ0m0ge9kiWUbJk5HXjx0aP5igmE42etEaNpKU
                         8n7YGFRaPZVFg1ucCK0f/lqMK9kgi/22Je7monlVCyr368J3m6ODuKzvruKL
                         ax/UeL1V3Wm71u3HS90ex516tt9Xh2JaCeQ/tz8D9OEb0PHd5L8A";

        /// <summary>
        /// Xmpp Client Handler
        /// </summary>
        public XmppClient GuestClient { get; private set; }

        /// <summary>
        /// Array that keeps a list of available events 
        /// </summary>
        public HashSet<string> KeyList { get; private set; } = new HashSet<string>()
        {
            // see xmpp library for more information...
            // TODO define more events if needed
            "OnRegister",
            "OnRegisterError",
            "OnLoginExecuted",
            "OnBindExecuted",
            "OnBindError",
            "OnReceived"
        };

        public Action<MessageEventArgs> MessageReceived { get; set; }

        /// <summary>
        /// Table of wrapped events
        /// used inside of XMPP library
        /// </summary>
        private readonly Dictionary<string, EventHandler> _eventTable = new Dictionary<string, EventHandler>();

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="username">Xmpp Client Username</param>
        /// <param name="defaultPassw">Xmpp Client Password</param>
        /// <param name="defaultDomain">Xmpp Server Domain(do not use IP)</param>
        public RegisterGuest(string username, string defaultPassw, string defaultDomain, int defaultPort)
        {
            GuestClient = new XmppClient
            {
                Username = username,
                Password = defaultPassw,
                XmppDomain = defaultDomain,
                Port = defaultPort
            };

            Initialize();
        }

        public RegisterGuest(XmppClient xmppClient)
        {
            GuestClient = xmppClient ?? throw new NullReferenceException();

            Initialize();
        }

        /// <summary>
        /// Execute Syncronous connection
        /// </summary>
        public void Connect()
        {
            // used only in development process to resolve certification/security issues with the xmpp server
            ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertficate;
            GuestClient.OnValidateCertificate += XmppClient_OnValidateCertificate;

            Matrix.License.LicenseManager.SetLicense(lic);

            GuestClient.Open();

            GuestClient.SendPresence(Matrix.Xmpp.Show.Chat, "I'm chatty");
        }

        /// <summary>
        /// Add a new event to be called by Xmpp library
        /// </summary>
        /// <param name="key">event name(see KeyTable property)</param>
        /// <param name="ev"></param>
        /// <returns>true if success, otherwise false</returns>
        public bool AddEvent(string key, EventHandler ev)
        {
            if (ev == null || _eventTable.ContainsValue(ev))
                return false;

            if (key == null || !KeyList.Contains(key))
                return false;

          return _eventTable.TryAdd(key, ev);
        }


        /// <summary>
        /// Remove an event from Xmpp library
        /// </summary>
        /// <param name="key">event name(see KeyTable property)</param>
        /// <returns>true if success, otherwise false</returns>
        public bool RemoveEvent(string key)
        {
            if (key != null && KeyList.Contains(key))
            {
                _eventTable.Remove(key);

                return true;
            }
            else
            {
                return false;
            }
        }

        private void Initialize()
        {
            GuestClient.OnRegister += new EventHandler<Matrix.EventArgs>(XmppClient_OnRegister);
            GuestClient.OnRegisterInformation += new EventHandler<RegisterEventArgs>(XmppClient_OnRegisterInformation);
            GuestClient.OnRegisterError += new EventHandler<IqEventArgs>(XmppClient_OnRegisterError);
            GuestClient.OnMessage += new EventHandler<MessageEventArgs>(XmppClient_OnMessageReceived);
            GuestClient.OnLogin += new EventHandler<Matrix.EventArgs>(XmppClient_OnLoginExecuted);
            GuestClient.OnBind += new EventHandler<JidEventArgs>(XmppClient_OnBindExecuted);
            GuestClient.OnBindError += new EventHandler<IqEventArgs>(XmppClient_OnBindError);

            GuestClient.AnonymousLogin = true;
            //GuestClient.RegisterNewAccount = true;
        }

        #region RegistrationEvents
        private void XmppClient_OnRegisterInformation(object sender, RegisterEventArgs e)
        {
            e.Register.RemoveAll<Matrix.Xmpp.XData.Data>();

            e.Register.Username = GuestClient.Username;
            e.Register.Password = GuestClient.Password;
        }

        private void XmppClient_OnRegister(object sender, System.EventArgs e)
        {
            // registration was successful
            _eventTable["OnRegister"]?.Invoke(sender, e);
        }

        private void XmppClient_OnRegisterError(object sender, IqEventArgs e)
        {
            // registration failed.
            _eventTable["OnRegisterError"]?.Invoke(sender, e);

            GuestClient.Close();
        }
        #endregion

        #region CertificateForDevelopmentPhase
        private static bool ValidateServerCertficate(
            object sender,
                X509Certificate cert,
                X509Chain chain,
                SslPolicyErrors sslPolicyErrors)
        {

            return true;
        }

        private void XmppClient_OnValidateCertificate(object sender, CertificateEventArgs e)
        {
            // validate the certficate here accoding to your rules
            // or present it to your enduser in case of validation problems
            // in this demo code we just trust any certificate
            e.AcceptCertificate = true;
        }
        #endregion

        #region XMPPEvents
        private void XmppClient_OnLoginExecuted(object sender, Matrix.EventArgs e)
        {
            _eventTable["OnLoginExecuted"]?.Invoke(sender, e);
        }

        private void XmppClient_OnBindExecuted(object sender, System.EventArgs e)
        {
            _eventTable["OnBindExecuted"]?.Invoke(sender, e);
        }

        private void XmppClient_OnBindError(object sender, System.EventArgs e)
        {
            _eventTable["OnBindError"]?.Invoke(sender, e);
        }

        private void XmppClient_OnMessageReceived(object sender, MessageEventArgs e)
        {
            //_eventTable["OnReceived"]?.Invoke(sender, e);
            MessageReceived.Invoke(e);
            //chatHandler?.AddMessage("From: " + e.Message.From + " " + e.Message.Body);
        }
        #endregion
    }
}