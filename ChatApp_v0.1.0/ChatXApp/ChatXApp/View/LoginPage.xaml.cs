using ChatXApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatXApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage, INavigationHandler
    {
        public LoginPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
            this.BackgroundImageSource = "LoginPage.png";
            this.BindingContext = new LoginPageViewModel(this);
        }

        public async void NavigateToItemDetail(int itemID)
        {
            if (itemID == ItemsDefinitionsToNavigate.RoomPage)
            {
                await Navigation.PushAsync(new ChatRoomPage());
            }
        }
    }
}