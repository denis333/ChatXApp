namespace ChatXApp.ViewModels
{
    /// <summary>
    /// Interface used to navigate through 'View' Items,
    /// it is used to save the ViewModel decoupled from View
    /// </summary>
    public interface INavigationHandler
    {
        void NavigateToItemDetail(int itemID);
    }
}