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
        public List<string> MessagesHistory { get; private set; } = new List<string>();

        private ArrayAdapter listAdapter;
        private ListView listView;

        public ChatHandler(Context ctxt, ListView lv)
        {
            listView = lv;
            listAdapter = new ArrayAdapter(ctxt, Resource.Layout.list_item, MessagesHistory);

            Initialize();
        }

        private void Initialize()
        {
            listView.SetAdapter(listAdapter);

            MessagesHistory.Add("You have entered in the anonymous chat room");
            MessagesHistory.Add("message x");
            MessagesHistory.Add("message y");
            MessagesHistory.Add("message z");
            MessagesHistory.Add("message u");
        }

        public void AddMessage(string name, string msg)
        {
            // TODO
            //listView.AddView()
        }
    }
}