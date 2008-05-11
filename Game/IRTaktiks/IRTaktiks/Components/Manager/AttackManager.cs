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
        { }

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
        /// </summary>
        /// <param name="command">attack casted.</param>
        /// <param name="caster">The caster of the attack.</param>
        /// <param name="target">The target of the attack.</param>
        /// <param name="position">The position of the target.</param>
        private void Short(Command command, Unit caster, Unit target, Vector2 position)
        {
            Attack attack = command as Attack;
        }

        /// <summary>
        /// Cast the attack.
        /// </summary>
        /// <param name="command">attack casted.</param>
        /// <param name="caster">The caster of the attack.</param>
        /// <param name="target">The target of the attack.</param>
        /// <param name="position">The position of the target.</param>
        private void Long(Command command, Unit caster, Unit target, Vector2 position)
        {
            Attack attack = command as Attack;
        }

        #endregion
    }
}
