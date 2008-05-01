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
using IRTaktiks.Input;
using IRTaktiks.Input.EventArgs;

namespace IRTaktiks.Components.Debug
{
    /// <summary>
    /// Debug component for touchs.
    /// </summary>
    public class TouchDebug : DrawableGameComponent
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

        /// <summary>
        /// The Size of the arrow.
        /// </summary>
        private Rectangle Size;
                
        #endregion

        #region Constructor

        /// <summary>
		/// Constructor of class.
		/// </summary>
		/// <param name="game">The instance of game that is running.</param>
        public TouchDebug(Game game)
			: base(game)
		{
            this.Enabled = false;
            this.Visible = false;

            InputManager.Instance.CursorDown += new EventHandler<CursorDownArgs>(Touch_CursorDown);
        }

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
            if (gameTime.TotalGameTime.Milliseconds % 1 == 0)
            {
                this.Size.Width += 2;
                this.Size.Height += 2;
            }

            if (this.Size.Width >= TextureManager.Instance.Sprites.Debug.Arrow.Width / 2)
            {
                this.Enabled = false;
                this.Visible = false;
            }
            
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

            Vector2 arrowPosition = new Vector2(this.Size.X - this.Size.Width / 2, this.Size.Y - this.Size.Height / 2);

            game.SpriteManager.Draw(TextureManager.Instance.Sprites.Debug.Arrow, new Rectangle((int)arrowPosition.X, (int)arrowPosition.Y, this.Size.Width, this.Size.Height), Color.White, 100);

            base.Draw(gameTime);
		}

		#endregion

        #region Input Handling

        /// <summary>
        /// Handle the CursorDown event.
        /// </summary>
        private void Touch_CursorDown(object sender, CursorDownArgs e)
        {
            this.Enabled = true;
            this.Visible = true;

            this.Size = new Rectangle((int)e.Position.X, (int)e.Position.Y, 0, 0); 
        }

        #endregion
    }
}