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
        /// The instance of the camera of the game.
        /// </summary>
        private Camera CameraField;

        /// <summary>
        /// The instance of the camera of the game.
        /// </summary>
        public Camera Camera
        {
            get { return CameraField; }
        }

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
            this.CameraField = new Camera(game);
            this.MapField = new Map(game);

			// Add the children components to the list.
            this.Components.Add(this.Camera);
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
            }

            // Add the units of the player two and its menus.
            foreach (Unit unit in game.PlayerTwo.Units)
            {
                this.Components.Add(unit);
                this.Components.Add(unit.StatusMenu);
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
            // Set the effects parameters.
            EffectManager.Instance.TerrainEffect.Parameters["World"].SetValue(Matrix.Identity);
            EffectManager.Instance.TerrainEffect.Parameters["View"].SetValue(this.Camera.View);
            EffectManager.Instance.TerrainEffect.Parameters["Projection"].SetValue(this.Camera.Projection);
            EffectManager.Instance.TerrainEffect.Parameters["MaxHeight"].SetValue(this.Map.MaxHeight);
            EffectManager.Instance.TerrainEffect.Parameters["TerrainHeightmap"].SetValue(TextureManager.Instance.Terrains.Terrain);
            EffectManager.Instance.TerrainEffect.Parameters["SandTexture"].SetValue(TextureManager.Instance.Textures.Sand);
            EffectManager.Instance.TerrainEffect.Parameters["GrassTexture"].SetValue(TextureManager.Instance.Textures.Grass);
            EffectManager.Instance.TerrainEffect.Parameters["RockTexture"].SetValue(TextureManager.Instance.Textures.Rock);
            EffectManager.Instance.TerrainEffect.Parameters["SnowTexture"].SetValue(TextureManager.Instance.Textures.Snow);

            // Start the effect.
            EffectManager.Instance.TerrainEffect.Begin();

            // Draws all passes of the current technique.
            foreach (EffectPass pass in EffectManager.Instance.TerrainEffect.CurrentTechnique.Passes)
            {
                pass.Begin();
                this.Map.Draw(gameTime);
                pass.End();
            }

            // End the effect.
            EffectManager.Instance.TerrainEffect.End(); 
            
            base.Draw(gameTime);
        }

        #endregion
    }
}
