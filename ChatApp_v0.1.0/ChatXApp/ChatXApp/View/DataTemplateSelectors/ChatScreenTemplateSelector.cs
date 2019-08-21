using ChatXApp.View.ViewCells;
using Xamarin.Forms;
using ChatXApp.Model;
using ChatXApp.Configs;
using System;

namespace ChatXApp.View.DataTemplateSelectors
{
    public class ChatScreenTemplateSelector : DataTemplateSelector
    {
        DataTemplate inputMessageDT;
        DataTemplate outputMessageDT;

        public ChatScreenTemplateSelector()
        {
            inputMessageDT = new DataTemplate(typeof(InputViewCell));
            outputMessageDT = new DataTemplate(typeof(OutputViewCell));
        }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (DataConfiguration.AppUser == null) throw new NullReferenceException("AppUser");

            var messageDirection = item as MsgItem;

            return (messageDirection.Who.Name == DataConfiguration.AppUser.Name) ? outputMessageDT : inputMessageDT;
        }
    }
}
