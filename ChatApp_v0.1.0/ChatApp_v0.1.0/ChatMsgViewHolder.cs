using Android.Widget;

namespace ChatApp_v0._1._0
{
    /// <summary>
    /// Custom View Holder Object
    /// </summary>
    public class ChatMsgViewHolder : Java.Lang.Object
    {
        public ImageView Avatar { get; set; }
        public TextView TextMessage { set; get; }
        public TextView Time { set; get; }
    }
}