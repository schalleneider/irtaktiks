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
using IRTaktiks.Components.Managers;

namespace IRTaktiks.Components.Screens
{
    /// <summary>
    /// Title screen of the game.
    /// </summary>
    public class TitleScreen : Screen
    {
        #region Properties

        #endregion

        #region Constructor

        /// <summary>
		/// Constructor of class.
		/// </summary>
		/// <param name="game">The instance of game that is running.</param>
        /// <param name="priority">Drawing priority of screen. Higher indicates that the screen will be at the top of the others.</param>
        public TitleScreen(Game game, int priority)
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
            // Input Event registration.
            InputManager.Instance.CursorUp += new EventHandler<CursorUpArgs>(CursorUp_Handler);

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
            SpriteBatch spriteBatch = (this.Game as IRTGame).SpriteBatch;
            
            spriteBatch.Begin();
            spriteBatch.Draw(TextureManager.Instance.TitleScreen, new Vector2(0, 0), Color.White);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }

        #endregion

        #region Input Events

        /// <summary>
        /// Handler for the CursorUp event in this class.
        /// </summary>
        /// <param name="sender">Always null.</param>
        /// <param name="e">Data of event.</param>
        private void CursorUp_Handler(object sender, CursorUpArgs e)
        {
            // Exits the tile screen, changing the status of game.
            IRTGame game = this.Game as IRTGame;
            game.ChangeGameStatus(IRTGame.GameStatus.ConfigScreen);
            
            // Unregister this event dispatcher.
            InputManager.Instance.CursorUp -= this.CursorUp_Handler;
        }

        #endregion
    }
}
