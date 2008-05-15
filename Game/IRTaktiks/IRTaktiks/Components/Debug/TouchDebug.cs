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
using IRTaktiks.Components.Scenario;
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
                
        #endregion

        #region Constructor

        /// <summary>
		/// Constructor of class.
		/// </summary>
		/// <param name="game">The instance of game that is running.</param>
        public TouchDebug(Game game)
			: base(game)
		{
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
            base.Update(gameTime);
		}

		/// <summary>
		/// Called when the DrawableGameComponent needs to be drawn. Override this method
		//  with component-specific drawing code.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Draw(GameTime gameTime)
		{
            base.Draw(gameTime);
		}

		#endregion

        #region Input Handling

        /// <summary>
        /// Handle the CursorDown event.
        /// </summary>
        private void Touch_CursorDown(object sender, CursorDownArgs e)
        {
            IRTGame game = this.Game as IRTGame;

            game.ParticleManager.Queue(
                new ParticleEffect(e.Position, 50, ParticleEffect.EffectType.Firework, 0.1f, 2.0f, Color.Goldenrod)
                );

            //AnimationManager.Instance.QueueAnimation(AnimationManager.AnimationType.Long, e.Position);
        }

        #endregion
    }
}