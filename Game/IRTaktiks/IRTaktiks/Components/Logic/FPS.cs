using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IRTaktiks.Components.Managers;

namespace IRTaktiks.Components.Logic
{
	/// <summary>
	/// Shows the Frame per Second value.
	/// </summary>
	public class FPS : DrawableGameComponent
	{
		#region Properties

        /// <summary>
        /// The delta value.
        /// </summary>
        private float Delta;

        /// <summary>
        /// The FPS value.
        /// </summary>
        private float FPSValue;

        #endregion

        #region Constructor

        /// <summary>
		/// Constructor of class.
		/// </summary>
		/// <param name="game">The instance of game that is running.</param>
        public FPS(Game game)
			: base(game)
		{
            this.Delta = 0;
            this.FPSValue = 0;
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
            float timeElapsed = Convert.ToSingle(gameTime.ElapsedRealTime.TotalSeconds);
            if ((this.Delta += timeElapsed) > 1)
            {
                this.FPSValue = 1 / timeElapsed;
                this.Delta -= 1;
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

            game.SpriteBatch.Begin();

            Vector2 fpsSize = SpriteFontManager.Instance.Chilopod12.MeasureString(this.FPSValue.ToString());
            Vector2 fpsPosition = new Vector2(IRTGame.Width / 2 - fpsSize.X / 2, 0);

            game.SpriteBatch.DrawString(SpriteFontManager.Instance.Chilopod12, this.FPSValue.ToString(), fpsPosition, Color.White);

            game.SpriteBatch.End();

            base.Draw(gameTime);
		}

		#endregion
    }
}