using Xamarin.Forms;

namespace ChatXApp.Services
{
    public interface IAvatarManager
    {
        /// <summary>
        /// Take an available image source from the collection
        /// </summary>
        /// <returns></returns>
        ImageSource GetFreeImageSrc();

        /// <summary>
        /// Take an available image url from the collection
        /// </summary>
        /// <returns></returns>
        string GetFreeImageUrl();

        /// <summary>
        /// Put back a taken image to the collection
        /// </summary>
        /// <param name="imgUrl"></param>
        void PutBack(string imgUrl);

        /// <summary>
        /// Add an image source to the collection
        /// </summary>
        /// <param name="imgSrc"></param>
        void Add(ImageSource imgSrc);
    }
}