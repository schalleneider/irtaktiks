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
using IRTaktiks.Components.Playable;
using IRTaktiks.Components.Logic;
using IRTaktiks.Components.Manager;

namespace IRTaktiks.Components.Screen
{
    /// <summary>
    /// Config screen of the game.
    /// </summary>
    public class ConfigScreen : IScreen
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
            game.PlayerOne = new Player(this.Game, "Metatron", PlayerIndex.One);
            game.PlayerTwo = new Player(this.Game, "Lilith", PlayerIndex.Two);
            
            // Create the units for player one.
            Attributes attributesKnight = new Attributes(99, Job.Knight, Element.Fire, 99, 1, 50, 1, 50);
            Attributes attributesThief = new Attributes(99, Job.Assasin, Element.Wind, 20, 99, 20, 1, 40);
            Unit knightOne = new Unit(this.Game, game.PlayerOne, new Vector2(500, 400), attributesKnight, Orientation.Left, TextureManager.Instance.Characters.Knight, "Knight");
            Unit thiefOne = new Unit(this.Game, game.PlayerOne, new Vector2(500, 300), attributesThief, Orientation.Right, TextureManager.Instance.Characters.Wizard, "Thief");

            // Create the units for player two.
            Attributes attributesMonk = new Attributes(99, Job.Monk, Element.Earth, 50, 30, 10, 30, 10);
            Attributes attributesPriest = new Attributes(99, Job.Priest, Element.Holy, 1, 1, 1, 99, 99);            
            Unit monkTwo = new Unit(this.Game, game.PlayerTwo, new Vector2(800, 200), attributesMonk, Orientation.Up, TextureManager.Instance.Characters.Knight, "Monk");
            Unit priestTwo = new Unit(this.Game, game.PlayerTwo, new Vector2(800, 300), attributesPriest, Orientation.Down, TextureManager.Instance.Characters.Wizard, "Priest");

            // Add the units for player one.
            game.PlayerOne.Units.Add(knightOne);
            game.PlayerOne.Units.Add(thiefOne);

            // Add the units for player two.
            game.PlayerTwo.Units.Add(monkTwo);
            game.PlayerTwo.Units.Add(priestTwo);

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

            game.SpriteManager.Draw(TextureManager.Instance.Sprites.Config.Background, new Vector2(0, 0), Color.White, 100);
            game.SpriteManager.Draw(TextureManager.Instance.Sprites.Config.Background, new Vector2(640, 0), Color.White, 100);
            
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
