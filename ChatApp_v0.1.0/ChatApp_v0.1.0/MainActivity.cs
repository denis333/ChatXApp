//#define TEST // when test deployment is done

using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using ChatApp_v0._1._0.XmppUtilities;
using Matrix;
using Matrix.Xmpp.Client;
using Plugin.Connectivity;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace ChatApp_v0._1._0
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private ProgressBar pbLoading;
        public static XmppClient xmppClient;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            SetupEvents();
        }

        private void SetupEvents()
        {
            // Events Subscription
            Button btnEnter = FindViewById<Button>(Resource.Id.btnChatEnter);
            EditText etUsername = FindViewById<EditText>(Resource.Id.etUsername);
            pbLoading = FindViewById<ProgressBar>(Resource.Id.pbWaitAction);

            btnEnter.Click += (sender, e) =>
            {
                string text = etUsername.Text;
                if (string.IsNullOrEmpty(text))
                {
                    Toast.MakeText(this.ApplicationContext, "Username field could not be empty", ToastLength.Short).Show();
                    return;
                }

                if (!CrossConnectivity.Current.IsConnected)
                {
                    Toast.MakeText(this.ApplicationContext, "Internet connection is missing", ToastLength.Short).Show();
                    return;
                }

                xmppClient = new XmppClient();
                xmppClient.SetUsername("GuestOne");
                xmppClient.XmppDomain = XmppConfiguration.DefaultDomain;
                xmppClient.Port = XmppConfiguration.DefaultPort;
                xmppClient.AnonymousLogin = true;

                xmppClient.SendPresence(Matrix.Xmpp.Show.Chat, "I'm chatty");
                xmppClient.OnLogin += XmppLoginExecuted;
                xmppClient.OnBind += XmppBindExecuted;
                xmppClient.OnBindError += XmppBindError;


                // used only in development process to resolve certification/security issues with the xmpp server
                ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertficate;
                xmppClient.OnValidateCertificate += xmppClient_OnValidateCertificate;

                string lic = @"eJxkkFtTwjAQhf8K4yujCQIFnDWjQMV6oUiniLytbcCUpilpgtVfLwqCl5ed
                                Pfvt5czCnYh4VvBKKdOsOD/CxXGh5uYVNT9Lt+iIwUir2EbGi1lgbCwUkEMF
                                HixmRpg3VgOyz6FnC6Mk1wyGKDlz15haNEoD+dLQUzLH7O0bCJVVdlaAfDNw
                                JYqUFZjy4uKHs5N407Rlm+b9oTCP0XC3zIXm/U3GTukppU3qAPmHwCv6XCpm
                                tN3s2gn4jL/max3q0CaQPwACscjQWM3ZVMQiCIZl8rTs3c9mPkbraLyaaOlN
                                XerXR+GNsa1x3hy8LKh4wEHbyrYj2vPbZ6w9qmpjUu0TGqzDlpwtkivVajpe
                                MnKiquqGyeN6NZnOeZ0H414jXF63adnxEp/eXcqp+768ffZcYrppXmRI8iR3
                                Iu57DaxRrLf0+3Uq4qFbdu5N1/UaJknCcyAH30B272YfAg==";

                string lic2 = @"eJxkkNtSIjEQhl/F8pbaTTgMh602pcAggzirCzjoXWACZsgkIQcYfPodFc83
                         Xf3314e/GsZ8yaRlJ0UupD07petfVq3cnhr2R7yiUwI3RqV+6aKUTJxPuQL0
                         UYFbT6Xj7kCqgN5z6HnrVM4MgZjmjIQ7Kjx1ygB60dBTuaby8Aa4kidHK4De
                         GIQ55YJYKpg9/+Tsd1o2vbKy+f3QTKfUsbDQ3LB+mZEarmHcxg1APxBEts9y
                         RZzx5a6jgOf4Zb7awR1cB/QNwISvJXXeMJIno9UsamSrcVhk89Fcdbflg7ob
                         fNg0J7u5HS0oMvR+lk5XA7UZ0m0ge9kiWUbJk5HXjx0aP5igmE42etEaNpKU
                         8n7YGFRaPZVFg1ucCK0f/lqMK9kgi/22Je7monlVCyr368J3m6ODuKzvruKL
                         ax/UeL1V3Wm71u3HS90ex516tt9Xh2JaCeQ/tz8D9OEb0PHd5L8A";

        Matrix.License.LicenseManager.SetLicense(lic2);

                // loading spinner


#if TEST

                var intent = new Intent(this, typeof(ChatRoomActivity));
                StartActivity(intent);

#else
                pbLoading.Visibility = Android.Views.ViewStates.Visible;
                xmppClient.Open();

#endif
            };
        }

#region Solving certifcation issues in the develepment phase
        private static bool ValidateServerCertficate(
            object sender,
            X509Certificate cert,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {

            return true;
        }

        private void xmppClient_OnValidateCertificate(object sender, CertificateEventArgs e)
        {
            // validate the certficate here accoding to your rules
            // or present it to your enduser in case of validation problems
            // in this demo code we just trust any certificate
            e.AcceptCertificate = true;
        }
#endregion

#region XMPPEvents
        private void XmppLoginExecuted(object sender, EventArgs e)
        {
            // Hide the loading spinner (run on UI thread)
            Application.SynchronizationContext.Post(_ => 
            {
                pbLoading.Visibility = Android.Views.ViewStates.Invisible;

                Toast.MakeText(this.ApplicationContext, "Logged", ToastLength.Short).Show();

                // TODO: check if action from below also shall run on UI thread
                // open the new page
                //SetContentView(Resource.Layout.activity_chatRoom);

                var intent = new Intent(this, typeof(ChatRoomActivity));
                StartActivity(intent);

            }, null);
        }

        private void XmppBindExecuted(object sender, EventArgs e)
        {
            Application.SynchronizationContext.Post(_ =>
            {
                Toast.MakeText(this.ApplicationContext, "Binded", ToastLength.Short).Show();
            }, null);
        }

        private void XmppBindError(object sender, EventArgs e)
        {    
            Application.SynchronizationContext.Post(_ =>
            {
                pbLoading.Visibility = Android.Views.ViewStates.Invisible;

                Toast.MakeText(this.ApplicationContext, "Connection failed", ToastLength.Short).Show();
            }, null);
        }
#endregion
    }
}