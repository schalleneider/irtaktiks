using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IRTaktiks.Components.Scenario;

namespace IRTaktiks.Components.Manager
{
    /// <summary>
    /// Manager of map
    /// </summary>
    public class MapManager : DrawableGameComponent
    {
        #region Properties
        
        /// <summary>
        /// The map to be drawn
        /// </summary>
        private Map MapField;
        
        /// <summary>
        /// The map to be drawn
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
        /// <param name="camera">The camera of the game.</param>
        public MapManager(Game game)
            : base(game)
        {
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
        /// Called when the DrawableGameComponent needs to be drawn. Override this method with component-specific drawing code.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            IRTGame game = this.Game as IRTGame;

            this.Map.Effect.Parameters["World"].SetValue(Matrix.Identity);
            this.Map.Effect.Parameters["View"].SetValue(game.Camera.MapView);
            this.Map.Effect.Parameters["Projection"].SetValue(game.Camera.MapProjection);
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
                this.Map.Draw(gameTime);
                pass.End();
            }

            // End the effect.
            this.Map.Effect.End();
            
            base.Draw(gameTime);
        }

        #endregion
    }
}