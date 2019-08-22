using System;
using Xamarin.Forms;

namespace ChatXApp.ViewModels.Base
{
    /// <summary>
    /// This class is used only to get rid of Xamarin.Forms
    /// reference in view models
    /// </summary>
    public class BaseCommand : Command
    {
        public BaseCommand(Action<object> execute) : base(execute) { }
        public BaseCommand(Action execute) : base(execute) {}
        public BaseCommand(Action<object> execute, Func<object, bool> canExecute) : base(execute, canExecute) { }
        public BaseCommand(Action execute, Func<bool> canExecute) : base(execute, canExecute) { }
    }

    /// <summary>
    /// This class is used only to get rid of Xamarin.Forms
    /// reference in view models
    /// </summary>
    public class BaseCommand<T> : Command
        where T : class
    {
        public BaseCommand(Action<T> execute) : base((param) => execute.Invoke(param as T)) { }
    }
}
