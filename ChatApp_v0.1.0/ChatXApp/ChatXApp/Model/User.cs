using System;

namespace ChatXApp.Model
{
    /// <summary>
    /// Model "User" class
    /// </summary>
    public class User
    {
        #region Constructors
        public User()
        {
            Name = "TestUser";
        }

        public User(string name)
        {
            Name = name ?? throw new ArgumentNullException("name");
        }

        public User(string name, string imgUrl)
        {
            Name = name ?? throw new ArgumentNullException("name");
            ImageUrl = imgUrl ?? throw new ArgumentNullException("imgUrl");
        }
        #endregion

        /// <summary>
        /// User's avatar image
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// User's name
        /// </summary>
        public string Name { get; set; }
    }
}