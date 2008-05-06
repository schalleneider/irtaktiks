using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IRTaktiks.Components.Playable;
using System.Threading;
using IRTaktiks.Components.Scenario;

namespace IRTaktiks.Components.Manager
{
    /// <summary>
    /// Manager of animations.
    /// </summary>
    public class AnimationManager
    {
        #region Singleton

        /// <summary>
        /// The instance of the class.
        /// </summary>
        private static AnimationManager InstanceField;

        /// <summary>
        /// The instance of the class.
        /// </summary>
        public static AnimationManager Instance
        {
            get { if (InstanceField == null) InstanceField = new AnimationManager(); return InstanceField; }
        }

        /// <summary>
        /// Private constructor.
        /// </summary>
        private AnimationManager()
        { }

        #endregion

        #region AnimationType

        /// <summary>
        /// Type of animations.
        /// </summary>
        public enum AnimationType
        {
            /// <summary>
            /// The basic animation for attacks.
            /// </summary>
            Attack,

            /// <summary>
            /// The animation for healings magics
            /// </summary>
            Healing,
            
            /// <summary>
            /// The animation for fire magics.
            /// </summary>
            Fire,

            /// <summary>
            /// The animation for ice magics.
            /// </summary>
            Ice,

            /// <summary>
            /// The animation for thunder magics.
            /// </summary>
            Thunder,

            /// <summary>
            /// The basic animation for item usage.
            /// </summary>
            Item,
        }

        #endregion

        #region Properties

        /// <summary>
        /// The particle manager of game.
        /// </summary>
        private ParticleManager ParticleManager;

        #endregion

        #region Initialize

        /// <summary>
        /// Initializes the animation manager.
        /// </summary>
        /// <param name="game">The instance of game.</param>
        public void Initialize(Game game)
        {
            this.ParticleManager = (game as IRTGame).ParticleManager;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Do the respective animation in the specified position.
        /// </summary>
        /// <param name="type">Type of animation.</param>
        /// <param name="position">The central position of the animation.</param>
        public void Animate(AnimationType type, Vector2 position)
        {
            object[] parameters = new object[2];

            parameters[0] = type;
            parameters[1] = position;

            ThreadPool.QueueUserWorkItem(DoAnimation, parameters);
        }

        /// <summary>
        /// Do some animation.
        /// </summary>
        /// <param name="data">Animation data.</param>
        private void DoAnimation(object data)
        {
            object[] parameters = data as object[];

            AnimationType type = (AnimationType)parameters[0];
            Vector2 position = (Vector2)parameters[1];

            switch (type)
            {
                case AnimationType.Attack:

                    break;

                case AnimationType.Healing:
                    this.Healing(position);
                    break;

                case AnimationType.Fire:

                    break;

                case AnimationType.Ice:

                    break;

                case AnimationType.Thunder:

                    break;

                case AnimationType.Item:

                    break;
            }
        }

        #endregion

        #region Animations

        /// <summary>
        /// Animation of healing magics.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Healing(Vector2 position)
        {
            Vector2 firstPosition = new Vector2(position.X - 10, position.Y + 10);
            ParticleManager.Queue(new ParticleEffect(firstPosition, 50, ParticleEffect.EffectType.Flash0, 0.07f, 5f, Color.SkyBlue));
            ParticleManager.Queue(new ParticleEffect(firstPosition, 50, ParticleEffect.EffectType.Flash45, 0.07f, 5f, Color.SkyBlue));
            ParticleManager.Queue(new ParticleEffect(firstPosition, 50, ParticleEffect.EffectType.Flash90, 0.07f, 5f, Color.SkyBlue));
            ParticleManager.Queue(new ParticleEffect(firstPosition, 50, ParticleEffect.EffectType.Flash135, 0.07f, 5f, Color.SkyBlue));

            Thread.Sleep(200);
            
            Vector2 secondPosition = new Vector2(position.X + 10, position.Y - 10);
            ParticleManager.Queue(new ParticleEffect(secondPosition, 50, ParticleEffect.EffectType.Flash0, 0.07f, 5f, Color.LightGreen));
            ParticleManager.Queue(new ParticleEffect(secondPosition, 50, ParticleEffect.EffectType.Flash45, 0.07f, 5f, Color.LightGreen));
            ParticleManager.Queue(new ParticleEffect(secondPosition, 50, ParticleEffect.EffectType.Flash90, 0.07f, 5f, Color.LightGreen));
            ParticleManager.Queue(new ParticleEffect(secondPosition, 50, ParticleEffect.EffectType.Flash135, 0.07f, 5f, Color.LightGreen));

            Thread.Sleep(200);
            
            Vector2 thirdPosition = new Vector2(position.X + 10, position.Y + 10);
            ParticleManager.Queue(new ParticleEffect(thirdPosition, 50, ParticleEffect.EffectType.Flash0, 0.07f, 5f, Color.Goldenrod));
            ParticleManager.Queue(new ParticleEffect(thirdPosition, 50, ParticleEffect.EffectType.Flash45, 0.07f, 5f, Color.Goldenrod));
            ParticleManager.Queue(new ParticleEffect(thirdPosition, 50, ParticleEffect.EffectType.Flash90, 0.07f, 5f, Color.Goldenrod));
            ParticleManager.Queue(new ParticleEffect(thirdPosition, 50, ParticleEffect.EffectType.Flash135, 0.07f, 5f, Color.Goldenrod));
        }
        
        #endregion
    }
}
