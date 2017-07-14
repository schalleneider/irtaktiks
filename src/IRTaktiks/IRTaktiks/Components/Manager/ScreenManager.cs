using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IRTaktiks.Components.Screen;

namespace IRTaktiks.Components.Manager
{
	/// <summary>
	/// Manager of screens.
	/// </summary>
	public class ScreenManager : DrawableGameComponent
	{
		#region Properties

        /// <summary>
        /// All screens of game.
        /// </summary>
        private List<IScreen> ScreensField;

        /// <summary>
        /// All screens of game.
        /// </summary>
        public List<IScreen> Screens
        {
            get { return ScreensField; }
        }

        #endregion

		#region Constructor

		/// <summary>
		/// Constructor of class.
		/// </summary>
        /// <param name="game">The instance of game that is running.</param>
		public ScreenManager(Game game)
			: base(game)
		{
            this.ScreensField = new List<IScreen>();
		}

		#endregion

        #region Methods

		/// <summary>
		/// Allows the game component to perform any initialization it needs to before starting
		/// to run. This is where it can query for any required services and load content.
		/// </summary>
		public override void Initialize()
		{
            // Sort the screens based in its priorities.
            this.Screens.Sort();
			
            base.Initialize();
		}

		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
			// Update screens.
            foreach (IScreen screen in this.ScreensField)
            {
                // Check if the screen can be updated.
                if (screen.Enabled && screen.Initialized)
                {
                    screen.Update(gameTime);
                }
            }
            
            base.Draw(gameTime);
		}

        /// <summary>
        /// Called when the DrawableGameComponent needs to be drawn. Override this method
        //  with component-specific drawing code.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            // Draw screens.
            foreach (IScreen screen in this.ScreensField)
            {
                // Check if the screen can be drawed.
                if (screen.Visible && screen.Initialized)
                {
                    screen.Draw(gameTime);
                }
            }
            
            base.Update(gameTime);
        }

		#endregion
    }
}