﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ChatApp_v0._1._0.ViewModel
{   // TODO; redundancy, extract in basic commnd
    public class ChatEnterCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Func<bool> canExecute;
        private readonly Action<string> sendAction;

        public ChatEnterCommand(Func<bool> canExecute, Action<string> sendAction)
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