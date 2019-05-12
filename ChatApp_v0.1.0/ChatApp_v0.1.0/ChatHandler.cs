using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;

namespace ChatApp_v0._1._0
{
    // TODO
    public class ChatHandler
    {
        private ArrayAdapter<string> listAdapter;

        private ListView listView;
        private Context context;

        public ChatHandler(Context ctxt, ListView lv)
        {
            listView = lv;
            context = ctxt;

            Initialize();
        }

        private void Initialize()
        {
            listAdapter = new ArrayAdapter<string>(context, Resource.Layout.list_item);
            listView.Adapter = listAdapter;

            listAdapter.AddAll("Welcome");
        }

        public void AddMessage(string msg)
        {
            listAdapter.Add(msg);
        }
    }
}