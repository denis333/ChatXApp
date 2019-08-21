using ChatXApp.Model;
using ChatXApp.Services;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ChatXApp.Configs
{
    public static class DataConfiguration
    {
        /// <summary>
        /// Include available avatar images
        /// </summary>
        public static IAvatarManager AvatarMng { get; private set; } = new AvatarManager(new List<ImageSource>
            {
                "ScreenClip1.png",
                "ScreenClip2.png",
                "ScreenClip3.png",
                "ScreenClip4.png",
                "ScreenClip5.png",
                "ScreenClip6.png",
                "ScreenClip7.png",
                "ScreenClip8.png",
                "ScreenClip9.png",
                "ScreenClip10.png"
            });

        /// <summary>
        /// Actual User
        /// </summary>
        public static User AppUser { get; set; } = null;

        /// <summary>
        /// Acces to Xmpp client library
        /// </summary>
        public static IXmppWrapper xmppWrapper = null;

        /// <summary>
        /// Initialize the actual user with name and profile image
        /// </summary>
        /// <param name="name"></param>
        public static void InitAppUser(string name)
        {
            if (AppUser != null)
                AvatarMng.PutBack(AppUser.ImageUrl);

            AppUser = new User(name, AvatarMng.GetFreeImageUrl());
            xmppWrapper = DependencyService.Get<IXmppWrapper>();
        }

        /// <summary>
        /// Initialize the actual user with name and profile image 
        /// together with xmpp dependency
        /// </summary>
        /// <param name="name"></param>
        public static void InitAppUser(string name, string password, string domain, int port)
        {
            if (AppUser != null)
                AvatarMng.PutBack(AppUser.ImageUrl);

            AppUser = new User(name, AvatarMng.GetFreeImageUrl());
            xmppWrapper = DependencyService.Get<IXmppWrapper>();
            xmppWrapper.InitClient(name, password, domain, port);
        }
    }
}
