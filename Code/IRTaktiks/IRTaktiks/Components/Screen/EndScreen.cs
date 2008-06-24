using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IRTaktiks.Input;
using IRTaktiks.Input.EventArgs;
using IRTaktiks.Components.Manager;

namespace IRTaktiks.Components.Screen
{
    /// <summary>
    /// End screen of the game.
    /// </summary>
    public class EndScreen : IScreen
    {
        #region Properties

        /// <summary>
        /// The endgame texture for the player one.
        /// </summary>
        private Texture2D PlayerOneTextureField;

        /// <summary>
        /// The endgame texture for the player one.
        /// </summary>
        public Texture2D PlayerOneTexture
        {
            get { return PlayerOneTextureField; }
        }

        /// <summary>
        /// The endgame texture for the player two.
        /// </summary>
        private Texture2D PlayerTwoTextureField;

        /// <summary>
        /// The endgame texture for the player two.
        /// </summary>
        public Texture2D PlayerTwoTexture
        {
            get { return PlayerTwoTextureField; }
        }
        
        #endregion

        #region Constructor

        /// <summary>
		/// Constructor of class.
		/// </summary>
		/// <param name="game">The instance of game that is running.</param>
        /// <param name="priority">Drawing priority of screen. Higher indicates that the screen will be at the top of the others.</param>
        public EndScreen(Game game, int priority)
            : base(game, priority)
		{
		}

		#endregion

		#region Component Methods

		/// <summary>
		/// Allows the game component to perform any initialization it needs to before starting
		/// to run. This is where it can query for any required services and load content.
		/// </summary>
		public override void Initialize()
		{
            IRTGame game = this.Game as IRTGame; 
            game.MapManager.Visible = true;

            // Player one win !!!
            if (game.PlayerOne.Won)
            {
                this.PlayerOneTextureField = TextureManager.Instance.Sprites.Menu.Win;
                this.PlayerTwoTextureField = TextureManager.Instance.Sprites.Menu.Lose;
            }

            // Player two win !!!
            if (game.PlayerTwo.Won)
            {
                this.PlayerOneTextureField = TextureManager.Instance.Sprites.Menu.Lose;
                this.PlayerTwoTextureField = TextureManager.Instance.Sprites.Menu.Win;
            }

            base.Initialize();
		}

		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
            base.Update(gameTime);
		}

        /// <summary>
        /// Called when the DrawableGameComponent needs to be drawn. Override this method
        //  with component-specific drawing code.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            IRTGame game = this.Game as IRTGame;

            game.SpriteManager.Draw(this.PlayerOneTexture, new Vector2(0, 0), Color.White, 90);
            game.SpriteManager.Draw(this.PlayerTwoTexture, new Vector2(640, 0), Color.White, 90);

            base.Draw(gameTime);
        }

        #endregion
    }
}
