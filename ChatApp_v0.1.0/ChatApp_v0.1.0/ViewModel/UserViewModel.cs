using ChatApp_v0._1._0.ViewModel;
using System.Windows.Input;

namespace ChatApp_v0._1._0.model
{
    public class ActualUser : BasicViewModel
    {
        /// <summary>
        /// Command triggered when users clicks "Enter or Send message button
        /// </summary>
        ICommand SendMsgCommand;

        /// <summary>
        /// Actual username
        /// </summary>
        string Username
        {
            get
            {
                return Username;
            }
            set
            {
                Username = value;
                NotifyPropertyChanged();
            }
        }
    }
}