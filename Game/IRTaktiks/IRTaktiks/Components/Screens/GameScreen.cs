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
using IRTaktiks.Components.Managers;
using IRTaktiks.Components.Scenario;

namespace IRTaktiks.Components.Screens
{
    /// <summary>
    /// Game screen of the game.
    /// </summary>
    public class GameScreen : Screen
    {
        #region Properties

		/// <summary>
		/// The instance of the map of the game.
		/// </summary>
		private Map MapField;
		
		/// <summary>
		/// The instance of the map of the game.
		/// </summary>
		public Map Map
		{
			get { return MapField; }
		}

        #endregion

        #region Constructor

        /// <summary>
		/// Constructor of class.
		/// </summary>
		/// <param name="game">The instance of game that is running.</param>
        /// <param name="priority">Drawing priority of screen. Higher indicates that the screen will be at the top of the others.</param>
        public GameScreen(Game game, int priority)
            : base(game, priority)
		{
			// Create the children components of the screen.
            this.MapField = new Map(game);
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

            // Initialize the map.
            this.Map.Initialize();

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

            // Set the effects parameters.
            this.Map.Effect.Parameters["World"].SetValue(Matrix.Identity);
            this.Map.Effect.Parameters["View"].SetValue(game.Camera.View);
            this.Map.Effect.Parameters["Projection"].SetValue(game.Camera.Projection);
            this.Map.Effect.Parameters["MaxHeight"].SetValue(this.Map.MaxHeight);
            this.Map.Effect.Parameters["TerrainHeightmap"].SetValue(TextureManager.Instance.Terrains.Terrain);
            this.Map.Effect.Parameters["SandTexture"].SetValue(TextureManager.Instance.Textures.Sand);
            this.Map.Effect.Parameters["GrassTexture"].SetValue(TextureManager.Instance.Textures.Grass);
            this.Map.Effect.Parameters["RockTexture"].SetValue(TextureManager.Instance.Textures.Rock);
            this.Map.Effect.Parameters["SnowTexture"].SetValue(TextureManager.Instance.Textures.Snow);

            // Start the effect.
            this.Map.Effect.Begin();

            // Draws all passes of the current technique.
            foreach (EffectPass pass in this.Map.Effect.CurrentTechnique.Passes)
            {
                pass.Begin();
                // this.Map.Draw(gameTime);
                pass.End();
            }

            // End the effect.
            this.Map.Effect.End(); 
            
            base.Draw(gameTime);
        }

        #endregion
    }
}
