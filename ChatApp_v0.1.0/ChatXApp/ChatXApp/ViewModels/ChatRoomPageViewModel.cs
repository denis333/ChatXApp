using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Collections.Generic;
using ChatXApp.Model;
using ChatXApp.ViewModels.Base;


namespace ChatXApp.ViewModels
{
    // TODO: implement error validation with IDataError interfaces
    // TODO: think how to get rid of redundant 'ChangeCanExecute' calls without external libs
    public class ChatRoomPageViewModel : BaseViewModel
    {
        /// <summary>
        /// Command triggered when users clicks "Enter or Send message button
        /// </summary>
        public ICommand SendMsgCommand { get; set; }

        /// <summary>
        /// Command that scrolls the listview to the last message
        /// </summary>
        public ICommand ScrollToLastMsgCommand { get; set; }

        /// <summary>
        /// Command triggered when message entry field is changed TODO: check if not obsolete
        /// </summary>
        public ICommand TextMessageChanged { get; set; }

        /// <summary>
        /// Command...
        /// </summary>
        public ICommand MsgAppearingCommand { get; set; }

        /// <summary>
        /// Command...
        /// </summary>
        public ICommand MsgDisappearingCommand { get; set; }

        /// <summary>
        /// Collection of messages from the Chat Room
        /// </summary>
        public ObservableCollection<MsgItem> Messages { get; set; } = new ObservableCollection<MsgItem>();

        /// <summary>
        /// Indicates if the last message of the list is currently visible
        /// </summary>
        public bool LastMessageVisible { get; set; } = true;

        /// <summary>
        /// Indicates the number of messages received while scrolling on previous messages
        /// </summary>
        public int PendingMessageCount { get; set; } = 0;

        /// <summary>
        /// Indicates if should show or not the count of received messages 
        /// while scrolling on previous messages based on if there’s any
        /// </summary>
        public bool PendingMessageCountVisible { get { return PendingMessageCount > 0; } }

        /// <summary>
        /// Queue to save the new messages received while scrolling the list of messages.
        /// </summary>
        public Queue<MsgItem> DelayedMessages { get; set; } = new Queue<MsgItem>();

        private bool _showScrollTap = false;
        /// <summary>
        /// Indicates when the scroll to last message icon should show
        /// </summary>
        public bool ShowScrollTap
        {
            get => _showScrollTap;

            set
            {
                if (_showScrollTap == value)
                    return;

                _showScrollTap = value;

                OnPropertyChanged();
            }
        }

        private string _messageToSend;
        /// <summary>
        /// The outgoing message
        /// </summary>
        public string MessageToSend
        {           
            get
            {
                return _messageToSend;
            }
            set
            {
                if (_messageToSend == value)
                    return;

                _messageToSend = value;

                OnPropertyChanged();
                // Update CanExecute for SendMsgCommand
                ((SendMsgCommand) as BaseCommand).ChangeCanExecute();
            }
        }

        /// <summary>
        /// Command Status
        /// </summary>
        private bool _isBusy = false;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                if (_isBusy == value)
                    return;

                _isBusy = value;

                OnPropertyChanged();
                // Update CanExecute for SendMsgCommand
                ((SendMsgCommand) as BaseCommand).ChangeCanExecute();
            }
        }

        public ChatRoomPageViewModel()
        {
            // TODO: not used in demo mode
            Configs.DataConfiguration.xmppWrapper?.SetMessageCollection(Messages);

            SendMsgCommand = new BaseCommand(
                SendingAsync,
                (param) => !(IsBusy || string.IsNullOrEmpty(MessageToSend)));

            MsgAppearingCommand = new BaseCommand<MsgItem>(OnMessageAppearing);
            MsgDisappearingCommand = new BaseCommand<MsgItem>(OnMessageDisappearing);

            // TODO: Used only for test purposes
            // generate a new test message each 5seconds
            User mario = new User("Mario");
            mario.ImageUrl = Configs.DataConfiguration.AvatarMng.GetFreeImageUrl();
            UseTimer(TimeSpan.FromSeconds(5), () =>
            {
                var msg = new MsgItem("New message test", mario);
                HandleNewMessage(msg);

                Configs.DataConfiguration.xmppWrapper?.Send(msg);

                return true;
            });
        }

        private void OnMessageAppearing(MsgItem message)
        {
            var idx = Messages.IndexOf(message);
            if (idx <= 6)
            {
                InvokeOnUiThread(() =>
                {
                    while (DelayedMessages.Count > 0)
                    {
                        Messages.Insert(0, DelayedMessages.Dequeue());
                    }
                    ShowScrollTap = false;
                    LastMessageVisible = true;
                    PendingMessageCount = 0;
                });
            }
        }

        private void OnMessageDisappearing(MsgItem message)
        {
            var idx = Messages.IndexOf(message);
            if (idx >= 6)
            {
                InvokeOnUiThread(() =>
                {
                    ShowScrollTap = true;
                    LastMessageVisible = false;
                });
            }
        }

        private void SendingAsync(object param)
        {
            // TODO : beaware of 'void' return when implementing this method
            try
            {
                IsBusy = true;

                // prepare
                var item = new MsgItem(MessageToSend, DateTime.UtcNow, Configs.DataConfiguration.AppUser);

                // add to the collection
                Messages.Insert(0, item);

                // clear the entry
                MessageToSend = string.Empty;

            }
            finally
            {
                IsBusy = false;
            }
        }

        private void HandleNewMessage(MsgItem msgItem)
        {
            if (LastMessageVisible)
            {
                Messages.Insert(0, msgItem);
            }
            else
            {
                DelayedMessages.Enqueue(msgItem);
                PendingMessageCount++;
            }
        }
    }
}