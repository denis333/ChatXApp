using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
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

                if (!CrossConnectivity.Current.IsConnected)
                {
                    Toast.MakeText(this.ApplicationContext, "Internet connection is missing", ToastLength.Short).Show();
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
                    //HostnameResolver = new SrvNameResolver()
                };

                xmppClient.Status = "I'm chatty";
                xmppClient.Show = Matrix.Xmpp.Show.Chat;
                xmppClient.OnLogin += SuccessLogin;
                xmppClient.OnBind += BindSuccess;
                xmppClient.OnBindError += ErrorLogin;
                //xmppClient.OnLogin += new System.EventHandler<EventArgs>(this, SuccessLogin);
                //xmppClient.SendPresence = new Presence(Matrix.Xmpp.Show.Chat);
                // used only in development process to resolve certification/security issues with the xmpp server
                ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertficate;
                xmppClient.OnValidateCertificate += xmppClient_OnValidateCertificate;

                //xmppClient.CertificateValidator = new AlwaysAcceptCertificateValidator();
                //xmppClient.RegistrationHandler = new RegisterGuest(xmppClient);

                //await xmppClient.ConnectAsync();

                string lic = @"eJxkkFtTwjAQhf8K4yujCQIFnDWjQMV6oUiniLytbcCUpilpgtVfLwqCl5ed
                                Pfvt5czCnYh4VvBKKdOsOD/CxXGh5uYVNT9Lt+iIwUir2EbGi1lgbCwUkEMF
                                HixmRpg3VgOyz6FnC6Mk1wyGKDlz15haNEoD+dLQUzLH7O0bCJVVdlaAfDNw
                                JYqUFZjy4uKHs5N407Rlm+b9oTCP0XC3zIXm/U3GTukppU3qAPmHwCv6XCpm
                                tN3s2gn4jL/max3q0CaQPwACscjQWM3ZVMQiCIZl8rTs3c9mPkbraLyaaOlN
                                XerXR+GNsa1x3hy8LKh4wEHbyrYj2vPbZ6w9qmpjUu0TGqzDlpwtkivVajpe
                                MnKiquqGyeN6NZnOeZ0H414jXF63adnxEp/eXcqp+768ffZcYrppXmRI8iR3
                                Iu57DaxRrLf0+3Uq4qFbdu5N1/UaJknCcyAH30B272YfAg==";
                Matrix.License.LicenseManager.SetLicense(lic);
                xmppClient.Open();
     
                pbLoading.Visibility = Android.Views.ViewStates.Invisible;

                // open the new page
                SetContentView(Resource.Layout.activity_chatRoom);
            };
        }

        #region Certifcation issues for devProcess
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

        private void SuccessLogin(object sender, EventArgs e)
        {
            //Toast.MakeText(this.ApplicationContext, "Logged", ToastLength.Short).Show();
        }

        private void BindSuccess(object sender, EventArgs e)
        {
            //Toast.MakeText(this.ApplicationContext, "Binded", ToastLength.Short).Show();
        }

        private void ErrorLogin(object sender, EventArgs e)
        {
           // Toast.MakeText(this.ApplicationContext, "Error", ToastLength.Short).Show();
        }

        #endregion

    }
}