using Android.Content;
using Android.Widget;
using ChatApp_v0._1._0.Model;
using System.Collections.Generic;

namespace ChatApp_v0._1._0
{
    // TODO
    // make it singleton and send message to server here
    public class ChatHandler
    {
        private ArrayAdapter<string> listAdapter;
        private MsgItemsAdapter msgItemsAdapter;

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
             //listAdapter = new ArrayAdapter<string>(context, Resource.Layout.list_item);
            // listView.Adapter = listAdapter;
            // AddMessage("Hwllsdof");

            //ListAdapter.AddAll("Welcome");
             msgItemsAdapter = new MsgItemsAdapter(new List<MsgItem>() { new MsgItem("Short message 1"), new MsgItem("Short message 2"), new MsgItem("Long message sldkfsdlflsdfjsdlfjsdlfsd") });
            listView.Adapter = msgItemsAdapter;
           
        }

        public void AddMessage(string msg)
        {
            msgItemsAdapter.Add(new MsgItem(msg));
            //listAdapter.Add(msg);
        }


        //public void SendMessage(string msg)
        //{ TODO
        //    AddMessage(msg);
        //}
    }
}