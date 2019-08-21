using ChatApp_v0._1._0.ViewModel;
using ChatApp_v0._1._0.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;

namespace ChatApp_v0._1._0.model
{
    public class ChatRoomPageViewModel : BasicViewModel
    {
        /// <summary>
        /// Command triggered when users clicks "Enter or Send message button
        /// </summary>
        ICommand SendMsgCommand;

        /// <summary>
        /// Collection of messages from the Chat Room
        /// </summary>
        public ObservableCollection<MsgItem> Messages;

        /// <summary>
        /// Actual username
        /// </summary>
        public string ActualUserName
        {
            get
            {
                return ActualUserName;
            }
            set
            {
                if (ActualUserName == value)
                    return;

                ActualUserName = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The outgoing message
        /// </summary>
        public string MessageToSend
        {           
            get
            {
                return MessageToSend;
            }
            set
            {
                if (MessageToSend == value)
                    return;

                MessageToSend = value;

                NotifyPropertyChanged();
            }
        }

        public ChatRoomPageViewModel()
        {
            // TODO SendMsgCommand = new SendMessageCommand();
        }

        private void SendingAsync(string text)
        {
            // TODO

            var time = DateTime.UtcNow;
        }
    }
}