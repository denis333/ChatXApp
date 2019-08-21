using System;

namespace ChatApp_v0._1._0.Model
{
    public class MsgItem
    {
        public MsgItem(string msgContent, DateTime dt)
        {
            Content = msgContent;
            When = dt;
        }

        public MsgItem(string msgContent) : this(msgContent, DateTime.UtcNow)
        {
            Who = new User();
            Who.Name = "TestName";
            Who.ImageUrl = "images/defaultAvatar1.jpg";
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