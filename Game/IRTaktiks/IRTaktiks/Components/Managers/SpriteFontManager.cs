using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace IRTaktiks.Components.Managers
{
    /// <summary>
    /// Manager of sprite fonts.
    /// </summary>
    public class SpriteFontManager
    {
        #region Singleton

        /// <summary>
        /// The instance of the class.
        /// </summary>
        private static SpriteFontManager InstanceField;

        /// <summary>
        /// The instance of the class.
        /// </summary>
        public static SpriteFontManager Instance
        {
            get { if (InstanceField == null) InstanceField = new SpriteFontManager(); return InstanceField; }
        }

        /// <summary>
        /// Private constructor.
        /// </summary>
		private SpriteFontManager()
        { }

        #endregion

        #region Initialize

        /// <summary>
        /// Initialize the instance of the class.
        /// </summary>
        public void Initialize(Game game)
        {
			// Load the spritefonts
            this.Chilopod12Field = game.Content.Load<SpriteFont>("Fonts/Chilopod12");
            this.Chilopod14Field = game.Content.Load<SpriteFont>("Fonts/Chilopod14");
            this.Chilopod16Field = game.Content.Load<SpriteFont>("Fonts/Chilopod16");
            this.Chilopod18Field = game.Content.Load<SpriteFont>("Fonts/Chilopod18");
            this.Chilopod20Field = game.Content.Load<SpriteFont>("Fonts/Chilopod20");

            // Set the correct spacing.
            this.Chilopod12.Spacing = -3.5f;
            this.Chilopod14.Spacing = -3.5f;
            this.Chilopod16.Spacing = -3.5f;
            this.Chilopod18.Spacing = -3.5f;
			this.Chilopod20.Spacing = -3.5f;
        }

        #endregion

		#region Sprite Font

        /// <summary>
        /// The spritefont for the chilopod font type, 12pt of size.
        /// </summary>
        private SpriteFont Chilopod12Field;

        /// <summary>
        /// The spritefont for the chilopod font type, 12pt of size.
        /// </summary>
        public SpriteFont Chilopod12
        {
            get { return Chilopod12Field; }
        }

        /// <summary>
        /// The spritefont for the chilopod font type, 14pt of size.
        /// </summary>
        private SpriteFont Chilopod14Field;

        /// <summary>
        /// The spritefont for the chilopod font type, 14pt of size.
        /// </summary>
        public SpriteFont Chilopod14
        {
            get { return Chilopod14Field; }
        }

        /// <summary>
        /// The spritefont for the chilopod font type, 16pt of size.
        /// </summary>
        private SpriteFont Chilopod16Field;

        /// <summary>
        /// The spritefont for the chilopod font type, 16pt of size.
        /// </summary>
        public SpriteFont Chilopod16
        {
            get { return Chilopod16Field; }
        }
        
        /// <summary>
        /// The spritefont for the chilopod font type, 18pt of size.
        /// </summary>
        private SpriteFont Chilopod18Field;
        
        /// <summary>
        /// The spritefont for the chilopod font type, 18pt of size.
        /// </summary>
        public SpriteFont Chilopod18
        {
            get { return Chilopod18Field; }
        }

		/// <summary>
		/// The spritefont for the chilopod font type, 20pt of size.
		/// </summary>
		private SpriteFont Chilopod20Field;

		/// <summary>
		/// The spritefont for the chilopod font type, 20pt of size.
		/// </summary>
		public SpriteFont Chilopod20
		{
			get { return Chilopod20Field; }
		}

        #endregion
    }
}
