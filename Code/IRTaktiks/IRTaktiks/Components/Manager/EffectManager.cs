using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace IRTaktiks.Components.Manager
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
            this.AliasingEffectField = game.Content.Load<Effect>("Shaders/Aliasing");
            this.ParticleEffectField = game.Content.Load<Effect>("Shaders/ScaledParticle");
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

        /// <summary>
        /// The hlsl effect for anti-aliasing in 2D sprites.
        /// </summary>
        private Effect AliasingEffectField;

        /// <summary>
        /// The hlsl effect for anti-aliasing in 2D sprites.
        /// </summary>
        public Effect AliasingEffect
        {
            get { return AliasingEffectField; }
        }

        /// <summary>
        /// The hlsl effect for particle drawing in 3D particles.
        /// </summary>
        private Effect ParticleEffectField;

        /// <summary>
        /// The hlsl effect for particle drawing in 3D particles.
        /// </summary>
        public Effect ParticleEffect
        {
            get { return ParticleEffectField; }
        }
        
        #endregion
    }
}
