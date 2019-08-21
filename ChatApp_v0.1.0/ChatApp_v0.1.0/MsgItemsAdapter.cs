using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;
using Humanizer;
using ChatApp_v0._1._0.Model;

namespace ChatApp_v0._1._0
{
    public class MsgItemsAdapter : BaseAdapter<MsgItem>
    {
        protected Activity context = null;
        protected List<MsgItem> listOfItems;

        public MsgItemsAdapter(Activity context, IEnumerable<MsgItem> services)
        {
            this.context = context;
            listOfItems = services.ToList();
        }

        public MsgItemsAdapter(IEnumerable<MsgItem> services)
        {
            listOfItems = services.ToList();
        }

        public override int Count => listOfItems.Count;

        public override MsgItem this[int position] => listOfItems[position];

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ChatMsgViewHolder holder;
            var view = convertView;

            if (view == null)
            {
                holder = new ChatMsgViewHolder();
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.msgRow, parent, false);

                holder.Avatar = view.FindViewById<ImageView>(Resource.Id.ivAvatarImg);
                holder.TextMessage = view.FindViewById<TextView>(Resource.Id.tvTextMsg);
                holder.Time = view.FindViewById<TextView>(Resource.Id.tvTimeMsg);

                view.Tag = holder;
            }
            else
            {
                holder = view.Tag as ChatMsgViewHolder;
            }

            MsgItem item = listOfItems[position];

            holder.TextMessage.Text = item.Content;
            holder.Time.Text = item.When.Humanize();
            holder.Avatar.SetImageDrawable(ImageCacher.Get(parent.Context, item.Who.ImageUrl));

            return view;
        }

        public void Add(MsgItem item)
        {
            listOfItems.Add(item);
        }
    }
}