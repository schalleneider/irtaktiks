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

            /// <summary>
            /// Animation for Elixir.
            /// </summary>
            Elixir
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

                case AnimationType.Elixir:
                    this.Elixir(position);
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
            int sliceParticles = 30;
            int bloodParticles = 25;
            float sliceLifeIncrement = 0.2f;
            float bloodLifeIncrement = 0.3f;
            float sliceTotalLife = 2.5f;
            float bloodTotalLife = 3.0f;

            Vector2 slicePosition = new Vector2(position.X, position.Y);
            Vector2 bloodPosition1st = new Vector2(position.X + 1, position.Y + 1);
            Vector2 bloodPosition2st = new Vector2(position.X + 2, position.Y + 2);

            this.ParticleManager.Queue(new ParticleEffect(slicePosition, sliceParticles, ParticleEffect.EffectType.Flash45, sliceLifeIncrement, sliceTotalLife, Color.Silver));

            Thread.Sleep(100);

            this.ParticleManager.Queue(new ParticleEffect(bloodPosition1st, bloodParticles, ParticleEffect.EffectType.Flash45, bloodLifeIncrement, bloodTotalLife, Color.Red));

            Thread.Sleep(100);

            this.ParticleManager.Queue(new ParticleEffect(bloodPosition2st, bloodParticles, ParticleEffect.EffectType.Flash0, bloodLifeIncrement, bloodTotalLife, Color.Red));
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Barrier(Vector2 position)
        {
            int ringParticles = 50;
            int sphereParticles = 100;

            float ringLifeIncrement = 0.1f;
            float sphereLifeIncrement = 0.7f;

            float ringTotalLife = 4.5f;
            float sphereTotalLife = 21.0f;

            Vector2 position1st = new Vector2(position.X, position.Y);


            this.ParticleManager.Queue(new ParticleEffect(position1st, ringParticles, ParticleEffect.EffectType.Ring, ringLifeIncrement, ringTotalLife, Color.Silver));
            
            Thread.Sleep(500);

            this.ParticleManager.Queue(new ParticleEffect(position1st, sphereParticles, ParticleEffect.EffectType.Sphere, sphereLifeIncrement, sphereTotalLife, Color.Silver));

        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Curse(Vector2 position)
        {
            int particles = 50;            
            float lifeIncrement = 0.1f;
            float totalLife = 4.5f;
            
            Vector2 position1st = new Vector2(position.X, position.Y);

            this.ParticleManager.Queue(new ParticleEffect(position1st, particles, ParticleEffect.EffectType.Slice, lifeIncrement, totalLife, Color.Green));
                        
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Drain(Vector2 position)
        {
            int particles = 50;
            float lifeIncrement = 0.1f;
            float totalLife = 4.5f;

            Vector2 position1st = new Vector2(position.X, position.Y);

            this.ParticleManager.Queue(new ParticleEffect(position1st, particles, ParticleEffect.EffectType.Pillar, lifeIncrement, totalLife, Color.Red));
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Elixir(Vector2 position)
        {
            int particles = 25;
            float lifeIncrement = 0.07f;
            float totalLife = 3.5f;
            
            Vector2 position1st = new Vector2(position.X - 15, position.Y + 15);
            Vector2 position2nd = new Vector2(position.X - 05, position.Y + 05);
            Vector2 position3rd = new Vector2(position.X + 05, position.Y - 05);
            Vector2 position4th = new Vector2(position.X + 15, position.Y - 15);

            this.ParticleManager.Queue(new ParticleEffect(position1st, particles, ParticleEffect.EffectType.Flash0, lifeIncrement, totalLife, Color.ForestGreen));
            this.ParticleManager.Queue(new ParticleEffect(position1st, particles, ParticleEffect.EffectType.Flash45, lifeIncrement, totalLife, Color.ForestGreen));
            this.ParticleManager.Queue(new ParticleEffect(position1st, particles, ParticleEffect.EffectType.Flash90, lifeIncrement, totalLife, Color.ForestGreen));
            this.ParticleManager.Queue(new ParticleEffect(position1st, particles, ParticleEffect.EffectType.Flash135, lifeIncrement, totalLife, Color.ForestGreen));

            Thread.Sleep(200);

            this.ParticleManager.Queue(new ParticleEffect(position2nd, particles, ParticleEffect.EffectType.Flash0, lifeIncrement, totalLife, Color.Crimson));
            this.ParticleManager.Queue(new ParticleEffect(position2nd, particles, ParticleEffect.EffectType.Flash45, lifeIncrement, totalLife, Color.Crimson));
            this.ParticleManager.Queue(new ParticleEffect(position2nd, particles, ParticleEffect.EffectType.Flash90, lifeIncrement, totalLife, Color.Crimson));
            this.ParticleManager.Queue(new ParticleEffect(position2nd, particles, ParticleEffect.EffectType.Flash135, lifeIncrement, totalLife, Color.Crimson));

            Thread.Sleep(200);

            this.ParticleManager.Queue(new ParticleEffect(position3rd, particles, ParticleEffect.EffectType.Flash0, lifeIncrement, totalLife, Color.Chocolate));
            this.ParticleManager.Queue(new ParticleEffect(position3rd, particles, ParticleEffect.EffectType.Flash45, lifeIncrement, totalLife, Color.Chocolate));
            this.ParticleManager.Queue(new ParticleEffect(position3rd, particles, ParticleEffect.EffectType.Flash90, lifeIncrement, totalLife, Color.Chocolate));
            this.ParticleManager.Queue(new ParticleEffect(position3rd, particles, ParticleEffect.EffectType.Flash135, lifeIncrement, totalLife, Color.Chocolate));

            Thread.Sleep(200);

            this.ParticleManager.Queue(new ParticleEffect(position4th, particles, ParticleEffect.EffectType.Flash0, lifeIncrement, totalLife, Color.BlueViolet));
            this.ParticleManager.Queue(new ParticleEffect(position4th, particles, ParticleEffect.EffectType.Flash45, lifeIncrement, totalLife, Color.BlueViolet));
            this.ParticleManager.Queue(new ParticleEffect(position4th, particles, ParticleEffect.EffectType.Flash90, lifeIncrement, totalLife, Color.BlueViolet));
            this.ParticleManager.Queue(new ParticleEffect(position4th, particles, ParticleEffect.EffectType.Flash135, lifeIncrement, totalLife, Color.BlueViolet));
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Flame(Vector2 position)
        {
            int particles1st = 25;
            int particles2nd = 10;
            int particles3rd = 5;
            float lifeIncrement1st = 0.02f;
            float lifeIncrement2nd = 0.04f;
            float lifeIncrement3rd = 0.06f;
            float totalLife = 3.0f;

            Vector2 position1st = new Vector2(position.X - 15, position.Y - 15);
            Vector2 position2nd = new Vector2(position.X - 10, position.Y - 10);
            Vector2 position3rd = new Vector2(position.X - 05, position.Y - 05);
            Vector2 position4th = new Vector2(position.X - 0, position.Y - 0);
            Vector2 position5th = new Vector2(position.X + 05, position.Y - 05);
            Vector2 position6th = new Vector2(position.X + 10, position.Y - 10);
            Vector2 position7th = new Vector2(position.X + 15, position.Y - 15);

            this.ParticleManager.Queue(new ParticleEffect(position1st, particles3rd, ParticleEffect.EffectType.Losangle, lifeIncrement3rd, totalLife, Color.Gold));
            this.ParticleManager.Queue(new ParticleEffect(position2nd, particles2nd, ParticleEffect.EffectType.Losangle, lifeIncrement3rd, totalLife, Color.Red));
            this.ParticleManager.Queue(new ParticleEffect(position3rd, particles1st, ParticleEffect.EffectType.Losangle, lifeIncrement2nd, totalLife, Color.Goldenrod));
            this.ParticleManager.Queue(new ParticleEffect(position4th, particles1st, ParticleEffect.EffectType.Firework, lifeIncrement1st, totalLife, Color.Red));
            this.ParticleManager.Queue(new ParticleEffect(position5th, particles1st, ParticleEffect.EffectType.Losangle, lifeIncrement2nd, totalLife, Color.Tomato));
            this.ParticleManager.Queue(new ParticleEffect(position6th, particles2nd, ParticleEffect.EffectType.Losangle, lifeIncrement3rd, totalLife, Color.Goldenrod));
            this.ParticleManager.Queue(new ParticleEffect(position7th, particles3rd, ParticleEffect.EffectType.Losangle, lifeIncrement3rd, totalLife, Color.Gold));

            Thread.Sleep(150);

            this.ParticleManager.Queue(new ParticleEffect(position1st, particles3rd, ParticleEffect.EffectType.Losangle, lifeIncrement3rd, totalLife, Color.Gold));
            this.ParticleManager.Queue(new ParticleEffect(position2nd, particles2nd, ParticleEffect.EffectType.Firework, lifeIncrement1st, totalLife, Color.Goldenrod));
            this.ParticleManager.Queue(new ParticleEffect(position3rd, particles1st, ParticleEffect.EffectType.Firework, lifeIncrement2nd, totalLife, Color.Red));
            this.ParticleManager.Queue(new ParticleEffect(position4th, particles1st, ParticleEffect.EffectType.Firework, lifeIncrement1st, totalLife, Color.WhiteSmoke));
            this.ParticleManager.Queue(new ParticleEffect(position5th, particles1st, ParticleEffect.EffectType.Firework, lifeIncrement2nd, totalLife, Color.Red));
            this.ParticleManager.Queue(new ParticleEffect(position6th, particles2nd, ParticleEffect.EffectType.Firework, lifeIncrement1st, totalLife, Color.Goldenrod));
            this.ParticleManager.Queue(new ParticleEffect(position7th, particles3rd, ParticleEffect.EffectType.Losangle, lifeIncrement3rd, totalLife, Color.Gold));

            Thread.Sleep(150);

            this.ParticleManager.Queue(new ParticleEffect(position1st, particles3rd, ParticleEffect.EffectType.Losangle, lifeIncrement3rd, totalLife, Color.Gold));
            this.ParticleManager.Queue(new ParticleEffect(position2nd, particles2nd, ParticleEffect.EffectType.Firework, lifeIncrement3rd, totalLife, Color.Goldenrod));
            this.ParticleManager.Queue(new ParticleEffect(position3rd, particles1st, ParticleEffect.EffectType.Losangle, lifeIncrement2nd, totalLife, Color.Thistle));
            this.ParticleManager.Queue(new ParticleEffect(position4th, particles1st, ParticleEffect.EffectType.Firework, lifeIncrement1st, totalLife, Color.Red));
            this.ParticleManager.Queue(new ParticleEffect(position5th, particles1st, ParticleEffect.EffectType.Losangle, lifeIncrement2nd, totalLife, Color.Thistle));
            this.ParticleManager.Queue(new ParticleEffect(position6th, particles2nd, ParticleEffect.EffectType.Firework, lifeIncrement3rd, totalLife, Color.Goldenrod));
            this.ParticleManager.Queue(new ParticleEffect(position7th, particles3rd, ParticleEffect.EffectType.Losangle, lifeIncrement3rd, totalLife, Color.Gold));

            Thread.Sleep(150);

            this.ParticleManager.Queue(new ParticleEffect(position1st, particles3rd, ParticleEffect.EffectType.Losangle, lifeIncrement3rd, totalLife, Color.Gold));
            this.ParticleManager.Queue(new ParticleEffect(position2nd, particles2nd, ParticleEffect.EffectType.Firework, lifeIncrement1st, totalLife, Color.Goldenrod));
            this.ParticleManager.Queue(new ParticleEffect(position3rd, particles1st, ParticleEffect.EffectType.Firework, lifeIncrement2nd, totalLife, Color.Red));
            this.ParticleManager.Queue(new ParticleEffect(position4th, particles1st, ParticleEffect.EffectType.Firework, lifeIncrement1st, totalLife, Color.WhiteSmoke));
            this.ParticleManager.Queue(new ParticleEffect(position5th, particles1st, ParticleEffect.EffectType.Firework, lifeIncrement2nd, totalLife, Color.Red));
            this.ParticleManager.Queue(new ParticleEffect(position6th, particles2nd, ParticleEffect.EffectType.Firework, lifeIncrement1st, totalLife, Color.Goldenrod));
            this.ParticleManager.Queue(new ParticleEffect(position7th, particles3rd, ParticleEffect.EffectType.Losangle, lifeIncrement3rd, totalLife, Color.Gold));
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Frost(Vector2 position)
        {
            int particles1st = 25;
            int particles2nd = 10;
            int particles3rd = 5;
            float lifeIncrement1st = 0.02f;
            float lifeIncrement2nd = 0.04f;
            float lifeIncrement3rd = 0.06f;
            float totalLife = 3.0f;

            Vector2 position1st = new Vector2(position.X - 15, position.Y - 15);
            Vector2 position2nd = new Vector2(position.X - 10, position.Y - 10);
            Vector2 position3rd = new Vector2(position.X - 05, position.Y - 05);
            Vector2 position4th = new Vector2(position.X - 0, position.Y - 0);
            Vector2 position5th = new Vector2(position.X + 05, position.Y - 05);
            Vector2 position6th = new Vector2(position.X + 10, position.Y - 10);
            Vector2 position7th = new Vector2(position.X + 15, position.Y - 15);

            this.ParticleManager.Queue(new ParticleEffect(position1st, particles3rd, ParticleEffect.EffectType.Losangle, lifeIncrement3rd, totalLife, Color.Violet));
            this.ParticleManager.Queue(new ParticleEffect(position2nd, particles2nd, ParticleEffect.EffectType.Losangle, lifeIncrement3rd, totalLife, Color.Aquamarine));
            this.ParticleManager.Queue(new ParticleEffect(position3rd, particles1st, ParticleEffect.EffectType.Losangle, lifeIncrement2nd, totalLife, Color.ForestGreen));
            this.ParticleManager.Queue(new ParticleEffect(position4th, particles1st, ParticleEffect.EffectType.Firework, lifeIncrement1st, totalLife, Color.Wheat));
            this.ParticleManager.Queue(new ParticleEffect(position5th, particles1st, ParticleEffect.EffectType.Losangle, lifeIncrement2nd, totalLife, Color.Aquamarine));
            this.ParticleManager.Queue(new ParticleEffect(position6th, particles2nd, ParticleEffect.EffectType.Losangle, lifeIncrement3rd, totalLife, Color.ForestGreen));
            this.ParticleManager.Queue(new ParticleEffect(position7th, particles3rd, ParticleEffect.EffectType.Losangle, lifeIncrement3rd, totalLife, Color.Violet));

            Thread.Sleep(150);

            this.ParticleManager.Queue(new ParticleEffect(position1st, particles3rd, ParticleEffect.EffectType.Losangle, lifeIncrement3rd, totalLife, Color.CornflowerBlue));
            this.ParticleManager.Queue(new ParticleEffect(position2nd, particles2nd, ParticleEffect.EffectType.Firework, lifeIncrement1st, totalLife, Color.CadetBlue));
            this.ParticleManager.Queue(new ParticleEffect(position3rd, particles1st, ParticleEffect.EffectType.Firework, lifeIncrement2nd, totalLife, Color.Blue));
            this.ParticleManager.Queue(new ParticleEffect(position4th, particles1st, ParticleEffect.EffectType.Firework, lifeIncrement1st, totalLife, Color.Turquoise));
            this.ParticleManager.Queue(new ParticleEffect(position5th, particles1st, ParticleEffect.EffectType.Firework, lifeIncrement2nd, totalLife, Color.Blue));
            this.ParticleManager.Queue(new ParticleEffect(position6th, particles2nd, ParticleEffect.EffectType.Firework, lifeIncrement1st, totalLife, Color.CadetBlue));
            this.ParticleManager.Queue(new ParticleEffect(position7th, particles3rd, ParticleEffect.EffectType.Losangle, lifeIncrement3rd, totalLife, Color.CornflowerBlue));

            Thread.Sleep(150);

            this.ParticleManager.Queue(new ParticleEffect(position1st, particles3rd, ParticleEffect.EffectType.Losangle, lifeIncrement3rd, totalLife, Color.DarkBlue));
            this.ParticleManager.Queue(new ParticleEffect(position2nd, particles2nd, ParticleEffect.EffectType.Firework, lifeIncrement3rd, totalLife, Color.CornflowerBlue));
            this.ParticleManager.Queue(new ParticleEffect(position3rd, particles1st, ParticleEffect.EffectType.Losangle, lifeIncrement2nd, totalLife, Color.DarkViolet));
            this.ParticleManager.Queue(new ParticleEffect(position4th, particles1st, ParticleEffect.EffectType.Firework, lifeIncrement1st, totalLife, Color.Blue));
            this.ParticleManager.Queue(new ParticleEffect(position5th, particles1st, ParticleEffect.EffectType.Losangle, lifeIncrement2nd, totalLife, Color.DarkViolet));
            this.ParticleManager.Queue(new ParticleEffect(position6th, particles2nd, ParticleEffect.EffectType.Firework, lifeIncrement3rd, totalLife, Color.CornflowerBlue));
            this.ParticleManager.Queue(new ParticleEffect(position7th, particles3rd, ParticleEffect.EffectType.Losangle, lifeIncrement3rd, totalLife, Color.DarkBlue));

            Thread.Sleep(150);

            this.ParticleManager.Queue(new ParticleEffect(position1st, particles3rd, ParticleEffect.EffectType.Losangle, lifeIncrement3rd, totalLife, Color.CornflowerBlue));
            this.ParticleManager.Queue(new ParticleEffect(position2nd, particles2nd, ParticleEffect.EffectType.Firework, lifeIncrement1st, totalLife, Color.DarkBlue));
            this.ParticleManager.Queue(new ParticleEffect(position3rd, particles1st, ParticleEffect.EffectType.Firework, lifeIncrement2nd, totalLife, Color.CornflowerBlue));
            this.ParticleManager.Queue(new ParticleEffect(position4th, particles1st, ParticleEffect.EffectType.Firework, lifeIncrement1st, totalLife, Color.Blue));
            this.ParticleManager.Queue(new ParticleEffect(position5th, particles1st, ParticleEffect.EffectType.Firework, lifeIncrement2nd, totalLife, Color.DarkViolet));
            this.ParticleManager.Queue(new ParticleEffect(position6th, particles2nd, ParticleEffect.EffectType.Firework, lifeIncrement1st, totalLife, Color.CornflowerBlue));
            this.ParticleManager.Queue(new ParticleEffect(position7th, particles3rd, ParticleEffect.EffectType.Losangle, lifeIncrement3rd, totalLife, Color.DarkBlue));
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Heal(Vector2 position)
        {
            int particles = 50;
            float lifeIncrement = 0.07f;
            float totalLife = 5.0f;

            Vector2 position1st = new Vector2(position.X - 10, position.Y + 10);
            Vector2 position2nd = new Vector2(position.X + 10, position.Y - 10);
            Vector2 position3rd = new Vector2(position.X + 10, position.Y + 10);

            this.ParticleManager.Queue(new ParticleEffect(position1st, particles, ParticleEffect.EffectType.Flash0, lifeIncrement, totalLife, Color.ForestGreen));
            this.ParticleManager.Queue(new ParticleEffect(position1st, particles, ParticleEffect.EffectType.Flash45, lifeIncrement, totalLife, Color.ForestGreen));
            this.ParticleManager.Queue(new ParticleEffect(position1st, particles, ParticleEffect.EffectType.Flash90, lifeIncrement, totalLife, Color.ForestGreen));
            this.ParticleManager.Queue(new ParticleEffect(position1st, particles, ParticleEffect.EffectType.Flash135, lifeIncrement, totalLife, Color.ForestGreen));

            Thread.Sleep(200);

            this.ParticleManager.Queue(new ParticleEffect(position2nd, particles, ParticleEffect.EffectType.Flash0, lifeIncrement, totalLife, Color.Crimson));
            this.ParticleManager.Queue(new ParticleEffect(position2nd, particles, ParticleEffect.EffectType.Flash45, lifeIncrement, totalLife, Color.Crimson));
            this.ParticleManager.Queue(new ParticleEffect(position2nd, particles, ParticleEffect.EffectType.Flash90, lifeIncrement, totalLife, Color.Crimson));
            this.ParticleManager.Queue(new ParticleEffect(position2nd, particles, ParticleEffect.EffectType.Flash135, lifeIncrement, totalLife, Color.Crimson));

            Thread.Sleep(200);

            this.ParticleManager.Queue(new ParticleEffect(position3rd, particles, ParticleEffect.EffectType.Flash0, lifeIncrement, totalLife, Color.Chocolate));
            this.ParticleManager.Queue(new ParticleEffect(position3rd, particles, ParticleEffect.EffectType.Flash45, lifeIncrement, totalLife, Color.Chocolate));
            this.ParticleManager.Queue(new ParticleEffect(position3rd, particles, ParticleEffect.EffectType.Flash90, lifeIncrement, totalLife, Color.Chocolate));
            this.ParticleManager.Queue(new ParticleEffect(position3rd, particles, ParticleEffect.EffectType.Flash135, lifeIncrement, totalLife, Color.Chocolate));
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Holy(Vector2 position)
        {
            int particles = 50;
            float lifeIncrement = 0.06f;
            float totalLife = 3.0f;

            Vector2 horizontalPosition1st = new Vector2(position.X - 0, position.Y + 03);
            Vector2 horizontalPosition2nd = new Vector2(position.X - 0, position.Y + 0);
            Vector2 horizontalPosition3rd = new Vector2(position.X + 0, position.Y - 03);           
            Vector2 verticalPosition1st = new Vector2(position.X - 03, position.Y + 0);
            Vector2 verticalPosition2nd = new Vector2(position.X - 0, position.Y + 0);
            Vector2 verticalPosition3rd = new Vector2(position.X + 03, position.Y - 0);


            this.ParticleManager.Queue(new ParticleEffect(horizontalPosition1st, particles, ParticleEffect.EffectType.Flash0, lifeIncrement, totalLife, Color.Goldenrod));
            this.ParticleManager.Queue(new ParticleEffect(horizontalPosition2nd, particles, ParticleEffect.EffectType.Flash0, lifeIncrement, totalLife, Color.Goldenrod));
            this.ParticleManager.Queue(new ParticleEffect(horizontalPosition3rd, particles, ParticleEffect.EffectType.Flash0, lifeIncrement, totalLife, Color.Goldenrod));
            this.ParticleManager.Queue(new ParticleEffect(verticalPosition1st, particles, ParticleEffect.EffectType.Flash90, lifeIncrement, totalLife, Color.Goldenrod));
            this.ParticleManager.Queue(new ParticleEffect(verticalPosition2nd, particles, ParticleEffect.EffectType.Flash90, lifeIncrement, totalLife, Color.Goldenrod));
            this.ParticleManager.Queue(new ParticleEffect(verticalPosition3rd, particles, ParticleEffect.EffectType.Flash90, lifeIncrement, totalLife, Color.Goldenrod));

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
            int particles = 75;
            float lifeIncrement = 0.1f;
            float totalLife = 7.0f;

            Vector2 position1st = new Vector2(position.X, position.Y + 24);
            Vector2 position2nd = new Vector2(position.X, position.Y);

            this.ParticleManager.Queue(new ParticleEffect(position1st, particles, ParticleEffect.EffectType.Pillar, lifeIncrement, totalLife, Color.IndianRed));
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Long(Vector2 position)
        {
            int particles = 50;
            float lifeIncrement = 0.1f;
            float totalLife = 2.5f;

            Vector2 position1st = new Vector2(position.X - 5, position.Y - 5);
            Vector2 position2nd = new Vector2(position.X - 5, position.Y + 5);
            Vector2 position3rd = new Vector2(position.X + 5, position.Y - 5);
            Vector2 position4th = new Vector2(position.X + 5, position.Y + 5);

            this.ParticleManager.Queue(new ParticleEffect(position1st, particles, ParticleEffect.EffectType.Firework, lifeIncrement, totalLife, Color.Orchid));

            Thread.Sleep(50);
            
            this.ParticleManager.Queue(new ParticleEffect(position2nd, particles, ParticleEffect.EffectType.Firework, lifeIncrement, totalLife, Color.CadetBlue));

            Thread.Sleep(50);
            
            this.ParticleManager.Queue(new ParticleEffect(position3rd, particles, ParticleEffect.EffectType.Firework, lifeIncrement, totalLife, Color.IndianRed));

            Thread.Sleep(50);

            this.ParticleManager.Queue(new ParticleEffect(position4th, particles, ParticleEffect.EffectType.Firework, lifeIncrement, totalLife, Color.Cornsilk));
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Might(Vector2 position)
        {
            int particles = 75;
            float lifeIncrement = 0.1f;
            float totalLife = 7.0f;

            Vector2 position1st = new Vector2(position.X, position.Y + 24);
            Vector2 position2nd = new Vector2(position.X, position.Y);

            this.ParticleManager.Queue(new ParticleEffect(position1st, particles, ParticleEffect.EffectType.Pillar, lifeIncrement, totalLife, Color.GhostWhite));
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Quick(Vector2 position)
        {
            int particles = 75;
            float lifeIncrement = 0.1f;
            float totalLife = 7.0f;

            Vector2 position1st = new Vector2(position.X, position.Y + 24);
            Vector2 position2nd = new Vector2(position.X, position.Y);

            this.ParticleManager.Queue(new ParticleEffect(position1st, particles, ParticleEffect.EffectType.Pillar, lifeIncrement, totalLife, Color.Green));
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
            int particles = 50;
            float lifeIncrement = 0.1f;
            float totalLife = 4.0f;

            Vector2 position1st = new Vector2(position.X, position.Y - 10);
            Vector2 position2nd = new Vector2(position.X, position.Y + 10);
            Vector2 position3rd = new Vector2(position.X, position.Y);

            this.ParticleManager.Queue(new ParticleEffect(position1st, particles, ParticleEffect.EffectType.Flash45, lifeIncrement, totalLife, Color.Crimson));
            this.ParticleManager.Queue(new ParticleEffect(position1st, particles, ParticleEffect.EffectType.Flash135, lifeIncrement, totalLife, Color.Chocolate));

            this.ParticleManager.Queue(new ParticleEffect(position2nd, particles, ParticleEffect.EffectType.Flash45, lifeIncrement, totalLife, Color.DarkSeaGreen));
            this.ParticleManager.Queue(new ParticleEffect(position2nd, particles, ParticleEffect.EffectType.Flash135, lifeIncrement, totalLife, Color.BlueViolet));

            this.ParticleManager.Queue(new ParticleEffect(position3rd, particles, ParticleEffect.EffectType.Firework, lifeIncrement, totalLife / 2, Color.Red));
        }

        /// <summary>
        /// Execute the Animation.
        /// </summary>
        /// <param name="position">The target position.</param>
        private void Stealth(Vector2 position)
        {
            int particles = 75;
            float lifeIncrement = 0.1f;
            float totalLife = 7.0f;

            Vector2 position1st = new Vector2(position.X, position.Y + 24);
            Vector2 position2nd = new Vector2(position.X, position.Y);

            this.ParticleManager.Queue(new ParticleEffect(position1st, particles, ParticleEffect.EffectType.Pillar, lifeIncrement, totalLife, Color.Black));
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
            int particles = 75;
            float lifeIncrement = 0.1f;
            float totalLife = 7.0f;

            Vector2 position1st = new Vector2(position.X, position.Y + 24);
            Vector2 position2nd = new Vector2(position.X, position.Y);

            this.ParticleManager.Queue(new ParticleEffect(position1st, particles, ParticleEffect.EffectType.Pillar, lifeIncrement, totalLife, Color.SaddleBrown));
        }
        
        #endregion
    }
}
