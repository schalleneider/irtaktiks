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
using IRTaktiks.Components.Playables;

namespace IRTaktiks.Components.Screens
{
    /// <summary>
    /// Config screen of the game.
    /// </summary>
    public class ConfigScreen : Screen
    {
        #region Properties

        #endregion

        #region Constructor

        /// <summary>
		/// Constructor of class.
		/// </summary>
		/// <param name="game">The instance of game that is running.</param>
        /// <param name="priority">Drawing priority of screen. Higher indicates that the screen will be at the top of the others.</param>
        public ConfigScreen(Game game, int priority)
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

            // Create the players
            game.PlayerOne = new Player(this.Game, " Willians", PlayerIndex.One);
            game.PlayerTwo = new Player(this.Game, "Camila", PlayerIndex.Two);
            
            // Create the units for player one.
            Unit fighterOne = new Unit(this.Game, game.PlayerOne, 1, "Fighter", 2000, 500, 60, 20, 30, 10, 60);
            Unit sniperOne = new Unit(this.Game, game.PlayerOne, 1, "Sniper", 1000, 600, 10, 50, 20, 10, 80);
            Unit wizardOne = new Unit(this.Game, game.PlayerOne, 1, "Wizard", 500, 2000, 10, 10, 10, 80, 80);

            // Create the units for player two.
            Unit fighterTwo = new Unit(this.Game, game.PlayerTwo, 1, "Fighter", 2000, 500, 60, 20, 30, 10, 60);
            Unit sniperTwo = new Unit(this.Game, game.PlayerTwo, 1, "Sniper", 1000, 600, 10, 50, 20, 10, 80);
            Unit wizardTwo = new Unit(this.Game, game.PlayerTwo, 1, "Wizard", 500, 2000, 10, 10, 10, 80, 80);

            

            // Add the units for player one.
            game.PlayerOne.Units.Add(fighterOne);
            game.PlayerOne.Units.Add(sniperOne);
            game.PlayerOne.Units.Add(wizardOne);

            // Add the units for player two.
            game.PlayerTwo.Units.Add(fighterTwo);
            game.PlayerTwo.Units.Add(sniperTwo);
            game.PlayerTwo.Units.Add(wizardTwo);

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
            game.ChangeGameStatus(IRTGame.GameStatus.GameScreen);

            // Unregister this event dispatcher.
            InputManager.Instance.CursorUp -= this.CursorUp_Handler;
        }

        #endregion
    }
}
