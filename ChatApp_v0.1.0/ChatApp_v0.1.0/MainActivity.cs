using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Matrix;
using ChatApp_v0._1._0.XmppUtilities;
using Matrix.Network;
using Matrix.Srv;

namespace ChatApp_v0._1._0
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
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
            ProgressBar pbLoading = FindViewById<ProgressBar>(Resource.Id.pbWaitAction);

            btnEnter.Click += async (sender, e) =>
            { 
                string text = etUsername.Text;
                if (string.IsNullOrEmpty(text))
                {
                    Toast.MakeText(this.ApplicationContext, "Username field could not be empty", ToastLength.Short).Show();
                    return;
                }


                pbLoading.Visibility = Android.Views.ViewStates.Visible;

                // connect to xmpp server
                XmppClient xmppClient = new XmppClient()
                {
                    Username = "admin",
                    Password = "1",
                    XmppDomain = "192.168.8.103",
                    Port = 5222,

                    // setting the resolver to use the Srv resolver is optional, but recommended
                    HostnameResolver = new SrvNameResolver()
                };

                // used only in development process
                xmppClient.CertificateValidator = new AlwaysAcceptCertificateValidator();
                //xmppClient.RegistrationHandler = new RegisterGuest(xmppClient);


                //Activity.IsRunning
                await xmppClient.ConnectAsync();

                pbLoading.Visibility = Android.Views.ViewStates.Invisible;

                // open the new page
                SetContentView(Resource.Layout.activity_chatRoom);
            };
        }
    }
}