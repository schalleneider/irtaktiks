using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace IRTaktiks.Components.Managers.Actions
{
    public class MagicManager
    {
        #region Singleton

        /// <summary>
        /// The instance of the class.
        /// </summary>
        private static MagicManager InstanceField;

        /// <summary>
        /// The instance of the class.
        /// </summary>
        public static MagicManager Instance
        {
            get { if (InstanceField == null) InstanceField = new MagicManager(); return InstanceField; }
        }

        /// <summary>
        /// Private constructor.
        /// </summary>
        private MagicManager()
        { }

        #endregion 

        #region Methods

        #endregion
    }
}
