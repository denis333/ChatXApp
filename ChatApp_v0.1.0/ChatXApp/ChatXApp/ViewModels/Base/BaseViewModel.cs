using System;
using Xamarin.Forms;

namespace ChatXApp.ViewModels.Base
{
    /// <summary>
    /// This class is used only to get rid of Xamarin.Forms
    /// reference in other view models
    /// </summary>
    public class BaseViewModel : BindableObject
    {
        /// <summary>
        /// Execute async an action on UI thread
        /// </summary>
        /// <param name="action"></param>
        public void InvokeOnUiThread(Action action)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                action.Invoke();
            });
        }

        /// <summary>
        /// Use the system's timer capabilities
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="callback"></param>
        public void UseTimer(TimeSpan interval, Func<bool> callback)
        {
            Device.StartTimer(interval, callback);
        }

        public void ShowMessage()
        {
            // TODO DisplayAlert
        }
    }
}
