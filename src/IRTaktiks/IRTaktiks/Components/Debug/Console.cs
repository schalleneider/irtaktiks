using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IRTaktiks.Components.Manager;

namespace IRTaktiks.Components.Debug
{
    /// <summary>
    /// Debug component for debug variables.
    /// </summary>
    public class Console : DrawableGameComponent
    {
        #region Properties

        /// <summary>
        /// The text to be displayed.
        /// </summary>
        private string TextField;

        /// <summary>
        /// The text to be displayed.
        /// </summary>
        public string Text
        {
            get { return TextField; }
            set { TextField = value; }
        }
        
        #endregion

        #region Constructor

        /// <summary>
		/// Constructor of class.
		/// </summary>
		/// <param name="game">The instance of game that is running.</param>
        public Console(Game game)
			: base(game)
		{ }

		#endregion

		#region Component Methods

		/// <summary>
		/// Allows the game component to perform any initialization it needs to before starting
		/// to run. This is where it can query for any required services and load content.
		/// </summary>
		public override void Initialize()
		{
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
		/// Called when the DrawableGameComponent needs to be drawn. Override this method with component-specific drawing code.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Draw(GameTime gameTime)
		{
            if (this.Text != null)
            {
                Vector2 textSize = FontManager.Instance.Debug.MeasureString(this.Text);
                Vector2 textPosition = new Vector2(IRTSettings.Default.Width / 2 - textSize.X / 2, IRTSettings.Default.Height - textSize.Y);

                (this.Game as IRTGame).SpriteManager.DrawString(FontManager.Instance.Debug, this.Text, textPosition, Color.Yellow, 100);
            }

            base.Draw(gameTime);
		}

		#endregion
    }
}