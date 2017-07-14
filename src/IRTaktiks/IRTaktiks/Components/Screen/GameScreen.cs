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
using IRTaktiks.Components.Manager;
using IRTaktiks.Components.Scenario;

namespace IRTaktiks.Components.Screen
{
    /// <summary>
    /// Game screen of the game.
    /// </summary>
    public class GameScreen : IScreen
    {
        #region Constructor

        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="game">The instance of game that is running.</param>
        /// <param name="priority">Drawing priority of screen. Higher indicates that the screen will be at the top of the others.</param>
        public GameScreen(Game game, int priority)
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

            // Add the players to the game and its menus.
            this.Components.Add(game.PlayerOne);
            this.Components.Add(game.PlayerTwo);

            this.Components.Add(game.PlayerOne.Menu);
            this.Components.Add(game.PlayerTwo.Menu);

            // Add the units of the player one and its menus.
            foreach (Unit unit in game.PlayerOne.Units)
            {
                this.Components.Add(unit);
                this.Components.Add(unit.StatusMenu);
                this.Components.Add(unit.ActionManager);
            }

            // Add the units of the player two and its menus.
            foreach (Unit unit in game.PlayerTwo.Units)
            {
                this.Components.Add(unit);
                this.Components.Add(unit.StatusMenu);
                this.Components.Add(unit.ActionManager);
            }

            game.MapManager.Visible = true;

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            IRTGame game = this.Game as IRTGame;

            // End the game.
            if (game.PlayerOne.Won || game.PlayerTwo.Won)
            {
                game.ChangeScreen(IRTGame.GameScreens.EndScreen);
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
            base.Draw(gameTime);
        }

        #endregion
    }
}
