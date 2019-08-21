using System;

namespace ChatXApp.Model
{
    public class MsgItem
    {
        public MsgItem(string msgContent, DateTime dt, User msgAuthor)
        {
            Content = msgContent;
            When = dt;
            Who = msgAuthor;
        }

        public MsgItem(string msgContent, DateTime dt) : this(msgContent, dt, new User())
        {
        }

        public MsgItem(string msgContent, User msgAuthor) : this(msgContent, DateTime.UtcNow, msgAuthor)
        {
        }


        public MsgItem(string msgContent) : this(msgContent, DateTime.UtcNow, new User())
        {
        }

        /// <summary>
        /// Message Content(
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Who sent the message
        /// </summary>
        public User Who { get; set; }

        /// <summary>
        /// Time when message was created
        /// </summary>
        public DateTime When { get; set; }
    }
}