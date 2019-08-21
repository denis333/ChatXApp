// If you have an XMPP server, you can
// (1) - use the application with the xmpp client wired up(comment line #5 '#define APPCONFIG_XMPPSERVER'),
// (2) - otherwise, use the demo mode (comment line #6 '#define APPCONFIG_DEMO') ''this is the default mode''

//#define APPCONFIG_DEMO
#define APPCONFIG_XMPPSERVER
using ChatXApp.ViewModels.Base;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ChatXApp.Configs;

namespace ChatXApp.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        public LoginPageViewModel()
        {
            EnterCommand = new BaseCommand(async (param) => await ConnectAsync(), 
                (p) => 
                !(IsBusy || string.IsNullOrEmpty(NickName)));
        }

        public LoginPageViewModel(INavigationHandler nav) : this()
        {
            Navigation = nav;
        }

        public ICommand EnterCommand { get; set; }

        public INavigationHandler Navigation { get; set; }

        #region Properties
        /// <summary>
        /// Chat Room Enter Command
        /// </summary>

        /// <summary>
        /// User's entered nickname
        /// </summary>
        private string _nickname;
        public string NickName
        {          
            get
            {
                return _nickname;
            }
            set
            {
                if (_nickname == value)
                    return;

                _nickname = value;
                
                OnPropertyChanged();
                (EnterCommand as BaseCommand).ChangeCanExecute();
            }
        }

        /// <summary>
        /// Shows if a process is executing now
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
            }
        }
        #endregion

        private async Task ConnectAsync()
        {
            if (Navigation == null)
                throw new NullReferenceException("Navigation");

            IsBusy = true;

#if APPCONFIG_XMPPSERVER//if App is configured in demo, xmpp is not used, otherwise comment #define
            /*Busy flag is reseted in the xmpp OnLoginExecuted event*/
            DataConfiguration.InitAppUser(NickName, XmppConfiguration.DefaultGuestPassword, XmppConfiguration.DefaultDomain, XmppConfiguration.DefaultPort);
            SetupXmppEvents();

            DataConfiguration.xmppWrapper.ConnectClient();
#elif APPCONFIG_DEMO
            try
            {
                DataConfiguration.InitAppUser(NickName);
                // simulate a waiting process
                await Task.Delay(200);

                // Go to Chat Room page
                Navigation.NavigateToItemDetail(ItemsDefinitionsToNavigate.RoomPage);
            }
            finally
            {
                IsBusy = false;
            }

#elif (!APPCONFIG_DEMO && !APPCONFIG_XMPPSERVER)
            throw new Exception("Please define in which mode do you want to run the application 'APPCONFIG_DEMO' or 'APPCONFIG_XMPP'");
#endif
        }

        /// <summary>
        /// Setup XMPP library events for login
        /// Note: this method is called only if 'APPCONFIG_XMPPSERVER' is defined.
        /// </summary>
        private void SetupXmppEvents()
        {
            DataConfiguration.xmppWrapper.SetEvent(
                                "OnLoginExecuted",
                                (sender, e) =>
                                {
                                    // Go to Chat Room page
                                    InvokeOnUiThread(
                                        () => 
                                        {
                                            Navigation.NavigateToItemDetail(ItemsDefinitionsToNavigate.RoomPage);
                                            IsBusy = false;
                                        }); 
                                });
            DataConfiguration.xmppWrapper.SetEvent(
                            "OnBindExecuted",
                            (sender, e) =>
                            {
                                // todo show a message box at least
                                IsBusy = false;
                            });
            DataConfiguration.xmppWrapper.SetEvent(
                            "OnBindErrorExecuted",
                            (sender, e) =>
                            {
                                // todo show a message box at least
                                IsBusy = false;
                            });
        }
    }
}
