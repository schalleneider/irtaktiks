using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IRTaktiks.Components.Playable;
using IRTaktiks.Components.Scenario;
using IRTaktiks.Components.Action;
using System.Collections.Generic;
using IRTaktiks.Components.Logic;

namespace IRTaktiks.Components.Manager
{
    /// <summary>
    /// Manager of attacks.
    /// </summary>
    public class AttackManager
    {
        #region Singleton

        /// <summary>
        /// The instance of the class.
        /// </summary>
        private static AttackManager InstanceField;

        /// <summary>
        /// The instance of the class.
        /// </summary>
        public static AttackManager Instance
        {
            get { if (InstanceField == null) InstanceField = new AttackManager(); return InstanceField; }
        }

        /// <summary>
        /// Private constructor.
        /// </summary>
        private AttackManager()
        {
            this.Random = new Random();
        }

        #endregion 

        #region Properties

        /// <summary>
        /// Random number generator.
        /// </summary>
        private Random Random;

        #endregion

        #region Construct

        /// <summary>
        /// Construct the list of the attacks based on attributes.
        /// </summary>
        /// <param name="unit">The unit that the attacks will be constructed.</param>
        /// <returns>The list of attacks.</returns>
        public List<Attack> Construct(Unit unit)
        {
            List<Attack> attacks = new List<Attack>();

            #region Same attacks in all Jobs
            /*
            switch (unit.Attributes.Job)
            {
                case Job.Assasin:
                    {
                        return attacks;
                    }

                case Job.Knight:
                    {
                        return attacks;
                    }

                case Job.Monk:
                    {
                        return attacks;
                    }

                case Job.Paladin:
                    {
                        return attacks;
                    }

                case Job.Priest:
                    {
                        return attacks;
                    }

                case Job.Wizard:
                    {
                        return attacks;
                    }

                default:
                    {
                        return attacks;
                    }
            }
            */
            #endregion

            attacks.Add(new Attack(unit, "Short", Attack.AttackType.Short, Short));
            attacks.Add(new Attack(unit, "Long", Attack.AttackType.Long, Long));

            return attacks;
        }

        #endregion

        #region Attacks

        /// <summary>
        /// Cast the attack.
        /// (ATK - 0.3 * DEF) + (0.1 * (ATK - 0.3 * DEF))
        /// </summary>
        /// <param name="command">attack casted.</param>
        /// <param name="caster">The caster of the attack.</param>
        /// <param name="target">The target of the attack.</param>
        /// <param name="position">The position of the target.</param>
        private void Short(Command command, Unit caster, Unit target, Vector2 position)
        {
            // Obtain the game instance.
            IRTGame game = caster.Game as IRTGame;

            // Effects on caster.
            caster.Time = 0;

            // Effects on target.
            if (target != null)
            {
                // Calcule the attack.
                double attack;

                attack = (caster.Attributes.Attack - 0.3f * target.Attributes.Defense);
                attack += this.Random.NextDouble() * 0.1f * attack;
                attack = attack < 1 ? 1 : attack;

                // Apply the hit and flee %.
                int percent = caster.Attributes.Hit - target.Attributes.Flee;

                // Limits of flee and hit.
                if (percent > 100) percent = 100;
                if (percent < 0) percent = 0;

                // Hit
                if (this.Random.Next(0, 100) <= percent)
                {
                    target.Life -= (int)attack;
                    
                    Vector2 animationPosition = target == null ? position : new Vector2(target.Position.X + target.Texture.Width / 2, target.Position.Y + target.Texture.Height / 8);
                    AnimationManager.Instance.QueueAnimation(AnimationManager.AnimationType.Short, animationPosition);
                    
                    game.DamageManager.Queue(new Damage((int)attack, null, target.Position, Damage.DamageType.Harmful));
                }

                // Miss
                else
                {
                    game.DamageManager.Queue(new Damage("MISS", caster.Position, Damage.DamageType.Harmful));
                }
            }
        }

        /// <summary>
        /// Cast the attack.
        /// (1.5 * HIT - 0.3 * DEF) + (0.1 * (1.5 * HIT - 0.3 * DEF))
        /// </summary>
        /// <param name="command">attack casted.</param>
        /// <param name="caster">The caster of the attack.</param>
        /// <param name="target">The target of the attack.</param>
        /// <param name="position">The position of the target.</param>
        private void Long(Command command, Unit caster, Unit target, Vector2 position)
        {
            // Obtain the game instance.
            IRTGame game = caster.Game as IRTGame;

            // Effects on caster.
            caster.Time = 0;

            // Effects on target.
            if (target != null)
            {
                // Calcule the attack.
                double attack;

                attack = (1.5f * caster.Attributes.Hit - 0.3f * target.Attributes.Defense);
                attack += this.Random.NextDouble() * 0.1f * attack;
                attack = attack < 1 ? 1 : attack;

                // Apply the hit and flee %.
                int percent = caster.Attributes.Hit - target.Attributes.Flee;

                // Limits of flee and hit.
                if (percent > 100) percent = 100;
                if (percent < 0) percent = 0;

                // Hit
                if (this.Random.Next(0, 100) <= percent)
                {
                    target.Life -= (int)attack; 
                    
                    Vector2 animationPosition = target == null ? position : new Vector2(target.Position.X + target.Texture.Width / 2, target.Position.Y + target.Texture.Height / 8);
                    AnimationManager.Instance.QueueAnimation(AnimationManager.AnimationType.Long, animationPosition);
                    
                    game.DamageManager.Queue(new Damage((int)attack, null, target.Position, Damage.DamageType.Harmful));
                }

                // Miss
                else
                {
                    game.DamageManager.Queue(new Damage("MISS", caster.Position, Damage.DamageType.Harmful));
                }
            }
        }

        #endregion
    }
}
