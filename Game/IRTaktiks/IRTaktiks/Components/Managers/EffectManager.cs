using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace IRTaktiks.Components.Managers
{
    /// <summary>
    /// Manager of effects.
    /// </summary>
    public class EffectManager
    {
        #region Singleton

        /// <summary>
        /// The instance of the class.
        /// </summary>
        private static EffectManager InstanceField;

        /// <summary>
        /// The instance of the class.
        /// </summary>
        public static EffectManager Instance
        {
            get { if (InstanceField == null) InstanceField = new EffectManager(); return InstanceField; }
        }

        /// <summary>
        /// Private constructor.
        /// </summary>
        private EffectManager()
        { }

        #endregion

        #region Initialize

        /// <summary>
        /// Initialize the instance of the class.
        /// </summary>
        public void Initialize(Game game)
        {
            this.TerrainEffectField = game.Content.Load<Effect>("Shaders/Terrain");
        }

        #endregion

        #region Effect

        /// <summary>
        /// The hlsl effect for terrain generation.
        /// </summary>
        private Effect TerrainEffectField;

        /// <summary>
        /// The hlsl effect for terrain generation.
        /// </summary>
        public Effect TerrainEffect
        {
            get { return TerrainEffectField; }
        }

        #endregion
    }
}
