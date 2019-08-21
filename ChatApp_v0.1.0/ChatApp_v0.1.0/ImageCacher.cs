using Android.Content;
using Android.Graphics.Drawables;
using System.Collections.Generic;

namespace ChatApp_v0._1._0
{
    /// <summary>
    /// "ImageCacher" class provides avatar(image) caching feature
    /// </summary>
    public static class ImageCacher
    {
        static Dictionary<string, Drawable> cache = new Dictionary<string, Drawable>();

        public static Drawable Get(Context ctxt, string imgUrl)
        {
            if (!cache.ContainsKey(imgUrl))
            {
                var drawable = Drawable.CreateFromStream(ctxt.Assets.Open(imgUrl), null);

                cache.Add(imgUrl, drawable);
            }

            return cache[imgUrl];
        }
    }
}