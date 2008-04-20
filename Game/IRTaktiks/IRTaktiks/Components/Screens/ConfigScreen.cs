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
using IRTaktiks.Components.Logic;
using IRTaktiks.Components.Managers;

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
            Unit fighterOne = new Unit(this.Game, game.PlayerOne, new Vector2(200, 200), new UnitAttributes(80, 99, 60, 10, 40), UnitOrientation.Left, TextureManager.Instance.Characters.Knight, "Azn Flames", 11358, 758);
            Unit wizardOne = new Unit(this.Game, game.PlayerOne, new Vector2(200, 300), new UnitAttributes(1, 1, 1, 99, 99), UnitOrientation.Right, TextureManager.Instance.Characters.Knight, "Ben Kildan", 1521, 2536);

            // Create the units for player two.
            Unit fighterTwo = new Unit(this.Game, game.PlayerTwo, new Vector2(800, 200), new UnitAttributes(80, 99, 60, 10, 40), UnitOrientation.Up, TextureManager.Instance.Characters.Wizard, "Spanker", 5012, 612);
            Unit wizardTwo = new Unit(this.Game, game.PlayerTwo, new Vector2(800, 300), new UnitAttributes(1, 1, 1, 99, 99), UnitOrientation.Down, TextureManager.Instance.Characters.Wizard, "Mr. Light", 1105, 3452);

            // Add the units for player one.
            game.PlayerOne.Units.Add(fighterOne);
            game.PlayerOne.Units.Add(wizardOne);

            // Add the units for player two.
            game.PlayerTwo.Units.Add(fighterTwo);
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
            IRTGame game = this.Game as IRTGame;

            game.SpriteBatch.Begin();

            game.SpriteBatch.Draw(TextureManager.Instance.Sprites.Config.Background, new Vector2(0, 0), Color.White);
            game.SpriteBatch.Draw(TextureManager.Instance.Sprites.Config.Background, new Vector2(640, 0), Color.White);

            game.SpriteBatch.End(); 
            
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
