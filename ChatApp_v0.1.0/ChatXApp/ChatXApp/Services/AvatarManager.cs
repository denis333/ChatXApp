using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ChatXApp.Services
{
    /// <summary>
    /// Avatar Images manager class
    /// </summary>
    public class AvatarManager : IAvatarManager
    {
        #region Constructors
        private AvatarManager()
        {

        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="amount">Image Source Collection</param>
        /// <param name="rnd">Random seed</param>
        /// /// <param name="maxImagesAllowed">Avatar Maximum images number allowance</param>
        public AvatarManager(List<ImageSource> imageSrcCollection, uint maxImagesAllowed = 10)
        {
            ImagesSrcCollection = imageSrcCollection ?? throw new ArgumentNullException("imageSrcCollection");

            AvatarMaxLimit = maxImagesAllowed;
        }
        #endregion

        /// <summary>
        /// Amount of images available for avatar selection
        /// </summary>
        public List<ImageSource> ImagesSrcCollection {get; set;}

        /// <summary>
        /// Avatar Maximum Image Sources
        /// </summary>
        public readonly uint AvatarMaxLimit;

        /// <summary>
        /// Random seed
        /// </summary>
        public Random Random { get; set; } = new Random();

        public void Add(ImageSource imgSrc)
        {
            if (ImagesSrcCollection.Count > AvatarMaxLimit)
                throw new Exception("AvatarMaxLimit reached");

            ImagesSrcCollection.Add(imgSrc);
        }

        public ImageSource GetFreeImageSrc()
        {
            if (ImagesSrcCollection.Count == 0) throw new Exception("No free image sources are available");

            // Take a random item from collection
            ImageSource src = ImagesSrcCollection[GetRandomListIndex()];

            // Track the used item by placing it in the collection of items 'in use'
            HoldItem(src);

            return src;
        }

        public string GetFreeImageUrl()
        {
            return GetFreeImageSrc().ToString().Replace("File: ", "");
        }

        public void PutBack(string imgUrl)
        {
            ImageSourceConverter isc = new ImageSourceConverter();

            ReleaseItem((ImageSource)isc.ConvertFromInvariantString(imgUrl));
        }

        private int GetRandomListIndex()
        {
            return Random.Next(ImagesSrcCollection.Count);
        }

        private void HoldItem(ImageSource imgSrc)
        {
            ImagesSrcCollection.Remove(imgSrc);
        }

        private void ReleaseItem(ImageSource imgSrc)
        {
            ImagesSrcCollection.Add(imgSrc);
        }
    }
}
