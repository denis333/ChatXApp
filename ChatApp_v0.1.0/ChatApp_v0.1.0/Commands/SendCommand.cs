using System;
using System.Windows.Input;

namespace ChatApp_v0._1._0.ViewModel
{
    public class SendMessageCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Func<bool> canExecute;
        private readonly Action<string> sendAction;

        public SendMessageCommand(Func<bool> canExecute, Action<string> sendAction)
        {
            this.canExecute = canExecute;
            this.sendAction = sendAction;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute();
        }

        public void Execute(object parameter)
        {
            if (canExecute())
            {
                var message = parameter as string;

                if (message != null) sendAction(message);
            }
        }
    }
}