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
            /// Animation for Short attack.
            /// </summary>
            Short,

            /// <summary>
            /// Animation for Long attack.
            /// </summary>
            Long,

            /// <summary>
            /// Animation for Stealth skill.
            /// </summary>
            Stealth,

            /// <summary>
            /// Animation for Ambush skill.
            /// </summary>
            Ambush,

            /// <summary>
            /// Animation for Curse skill.
            /// </summary>
            Curse,

            /// <summary>
            /// Animation for Quick skill.
            /// </summary>
            Quick,

            /// <summary>
            /// Animation for Impact skill.
            /// </summary>
            Impact,

            /// <summary>
            /// Animation for Revenge skill.
            /// </summary>
            Revenge,

            /// <summary>
            /// Animation for Warcry skill.
            /// </summary>
            Warcry,

            /// <summary>
            /// Animation for Insane skill.
            /// </summary>
            Insane,

            /// <summary>
            /// Animation for Reject skill.
            /// </summary>
            Reject,

            /// <summary>
            /// Animation for Might skill.
            /// </summary>
            Might,

            /// <summary>
            /// Animation for Heal skill.
            /// </summary>
            Heal,

            /// <summary>
            /// Animation for Unseal skill.
            /// </summary>
            Unseal,

            /// <summary>
            /// Animation for Barrier skill.
            /// </summary>
            Barrier,

            /// <summary>
            /// Animation for Holy skill.
            /// </summary>
            Holy,

            /// <summary>
            /// Animation for Drain skill.
            /// </summary>
            Drain,

            /// <summary>
            /// Animation for Flame skill.
            /// </summary>
            Flame,

            /// <summary>
            /// Animation for Frost skill.
            /// </summary>
            Frost,

            /// <summary>
            /// Animation for Item usage.
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
        public void QueueAnimation(AnimationType type, Vector2 position)
        {
            object[] parameters = new object[2];

            parameters[0] = type;
            parameters[1] = position;

            ThreadPool.QueueUserWorkItem(Animate, parameters);
        }

        /// <summary>
        /// Do some animation.
        /// </summary>
        /// <param name="data">Data tranferred across the threads.</param>
        private void Animate(object data)
        {
            object[] parameters = data as object[];

            AnimationType type = (AnimationType)parameters[0];
            Vector2 position = (Vector2)parameters[1];

            switch (type)
            {
                case AnimationType.Ambush:
                    this.Ambush(position);
                    break;

                case AnimationType.Barrier:
                    this.Barrier(position);
                    break;

                case AnimationType.Curse:
                    this.Curse(position);
                    break;

                case AnimationType.Drain:
                    this.Drain(position);
                    break;

                case AnimationType.Flame:
                    this.Flame(position);
                    break;

                case AnimationType.Frost:
                    this.Frost(position);
                    break;

                case AnimationType.Heal:
                    this.Heal(position);
                    break;

                case AnimationType.Holy:
                    this.Holy(position);
                    break;

                case AnimationType.Impact:
                    this.Impact(position);
                    break;

                case AnimationType.Insane:
                    this.Insane(position);
                    break;

                case AnimationType.Item:
                    this.Item(position);
                    break;

                case AnimationType.Long:
                    this.Long(position);
                    break;

                case AnimationType.Might:
                    this.Might(position);
                    break;

                case AnimationType.Quick:
                    this.Quick(position);
                    break;

                case AnimationType.Reject:
                    this.Reject(position);
                    break;

                case AnimationType.Revenge:
                    this.Revenge(position);
                    break;

                case AnimationType.Short:
                    this.Short(position);
                    break;

                case AnimationType.Stealth:
                    this.Stealth(position);
                    break;

                case AnimationType.Unseal:
                    this.Unseal(position);
                    break;

                case AnimationType.Warcry:
                    this.Warcry(position);
                    break;
            }
        }

        #endregion

        #region Animations

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void None(Vector2 position)
        {
            this.ParticleManager.Queue(new ParticleEffect(position, 100, ParticleEffect.EffectType.Ring, 0.1f, 5.0f, Color.PowderBlue));
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Ambush(Vector2 position)
        {
            this.None(position);
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Barrier(Vector2 position)
        {
            this.None(position);
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Curse(Vector2 position)
        {
            this.None(position);
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Drain(Vector2 position)
        {
            this.None(position);
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Flame(Vector2 position)
        {
            this.None(position);
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Frost(Vector2 position)
        {
            this.None(position);
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Heal(Vector2 position)
        {
            Vector2 firstPosition = new Vector2(position.X - 10, position.Y + 10);
            this.ParticleManager.Queue(new ParticleEffect(firstPosition, 50, ParticleEffect.EffectType.Flash0, 0.07f, 5f, Color.ForestGreen));
            this.ParticleManager.Queue(new ParticleEffect(firstPosition, 50, ParticleEffect.EffectType.Flash45, 0.07f, 5f, Color.ForestGreen));
            this.ParticleManager.Queue(new ParticleEffect(firstPosition, 50, ParticleEffect.EffectType.Flash90, 0.07f, 5f, Color.ForestGreen));
            this.ParticleManager.Queue(new ParticleEffect(firstPosition, 50, ParticleEffect.EffectType.Flash135, 0.07f, 5f, Color.ForestGreen));

            Thread.Sleep(200);

            Vector2 secondPosition = new Vector2(position.X + 10, position.Y - 10);
            this.ParticleManager.Queue(new ParticleEffect(secondPosition, 50, ParticleEffect.EffectType.Flash0, 0.07f, 5f, Color.Crimson));
            this.ParticleManager.Queue(new ParticleEffect(secondPosition, 50, ParticleEffect.EffectType.Flash45, 0.07f, 5f, Color.Crimson));
            this.ParticleManager.Queue(new ParticleEffect(secondPosition, 50, ParticleEffect.EffectType.Flash90, 0.07f, 5f, Color.Crimson));
            this.ParticleManager.Queue(new ParticleEffect(secondPosition, 50, ParticleEffect.EffectType.Flash135, 0.07f, 5f, Color.Crimson));

            Thread.Sleep(200);

            Vector2 thirdPosition = new Vector2(position.X + 10, position.Y + 10);
            this.ParticleManager.Queue(new ParticleEffect(thirdPosition, 50, ParticleEffect.EffectType.Flash0, 0.07f, 5f, Color.Chocolate));
            this.ParticleManager.Queue(new ParticleEffect(thirdPosition, 50, ParticleEffect.EffectType.Flash45, 0.07f, 5f, Color.Chocolate));
            this.ParticleManager.Queue(new ParticleEffect(thirdPosition, 50, ParticleEffect.EffectType.Flash90, 0.07f, 5f, Color.Chocolate));
            this.ParticleManager.Queue(new ParticleEffect(thirdPosition, 50, ParticleEffect.EffectType.Flash135, 0.07f, 5f, Color.Chocolate));
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Holy(Vector2 position)
        {
            this.None(position);
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Impact(Vector2 position)
        {
            this.None(position);
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Insane(Vector2 position)
        {
            this.None(position);
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Item(Vector2 position)
        {
            this.None(position);
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Long(Vector2 position)
        {
            this.None(position);
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Might(Vector2 position)
        {
            this.None(position);
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Quick(Vector2 position)
        {
            this.None(position);
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Reject(Vector2 position)
        {
            this.None(position);
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Revenge(Vector2 position)
        {
            this.None(position);
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Short(Vector2 position)
        {
            Vector2 firstPosition = new Vector2(position.X - 5, position.Y + 10);
            this.ParticleManager.Queue(new ParticleEffect(firstPosition, 100, ParticleEffect.EffectType.Flash45, 0.1f, 3f, Color.Orchid));

            Thread.Sleep(200);

            Vector2 secondPosition = new Vector2(position.X + 5, position.Y - 10);
            this.ParticleManager.Queue(new ParticleEffect(secondPosition, 100, ParticleEffect.EffectType.Flash135, 0.1f, 3f, Color.Aquamarine));

            Thread.Sleep(200);

            Vector2 thirdPosition = new Vector2(position.X - 5, position.Y - 10);
            this.ParticleManager.Queue(new ParticleEffect(thirdPosition, 100, ParticleEffect.EffectType.Flash45, 0.1f, 3f, Color.Orchid));

            Thread.Sleep(200);

            Vector2 fourthPosition = new Vector2(position.X + 5, position.Y + 10);
            this.ParticleManager.Queue(new ParticleEffect(fourthPosition, 100, ParticleEffect.EffectType.Flash135, 0.1f, 3f, Color.Aquamarine));
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Stealth(Vector2 position)
        {
            this.None(position);
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Unseal(Vector2 position)
        {
            this.None(position);
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Warcry(Vector2 position)
        {
            this.None(position);
        }
        
        #endregion
    }
}
