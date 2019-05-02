using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;

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

            btnEnter.Click += (sender, e) => SetContentView(Resource.Layout.activity_chatRoom);
        }
    }
}