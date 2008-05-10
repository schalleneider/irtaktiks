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
    /// Manager of skills.
    /// </summary>
    public class SkillManager
    {
        #region Singleton

        /// <summary>
        /// The instance of the class.
        /// </summary>
        private static SkillManager InstanceField;

        /// <summary>
        /// The instance of the class.
        /// </summary>
        public static SkillManager Instance
        {
            get { if (InstanceField == null) InstanceField = new SkillManager(); return InstanceField; }
        }

        /// <summary>
        /// Private constructor.
        /// </summary>
        private SkillManager()
        { }

        #endregion 

        #region Construct

        /// <summary>
        /// Construct the list of the skills based on attributes .
        /// </summary>
        /// <param name="unit">The unit that the skills will be constructed.</param>
        /// <returns>The list of skills.</returns>
        public List<Skill> Construct(Unit unit)
        {
            List<Skill> skills = new List<Skill>();

            switch (unit.Attributes.Job)
            {
                case Job.Assasin:
                    {
                        Skill doubleCut = new Skill(unit, "Double Cut",50);




                        return skills;
                    }

                case Job.Knight:
                    {
                        return skills;
                    }

                case Job.Monk:
                    {
                        return skills;
                    }

                case Job.Paladin:
                    {
                        return skills;
                    }

                case Job.Priest:
                    {
                        return skills;
                    }

                case Job.Wizard:
                    {
                        return skills;
                    }
            }
        }

        #endregion

        #region Check

        /// <summary>
        /// Check if the unit can use the skill.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="cost">The cost of the skill.</param>
        /// <returns>True if the unit can use the skill. False otherwise.</returns>
        private bool CanUse(Skill skill, Unit unit)
        {
            return unit.Mana >= cost;
        }

        #endregion

        #region Cast

        private bool Cast(Skill skill, Unit caster, Unit target)
        {
            // Obtain the game instance.
            IRTGame game = caster.Game as IRTGame;

            // Effects on caster.
            caster.Mana -= MagicManager.HealCost;
            caster.Time = 0;

            // Show the mp cost.
            if (caster != target)
            {
                game.DamageManager.Queue(new Damage(MagicManager.HealCost, "MP", caster.Position, Damage.DamageType.Harmful, 0.1f, 10f));
            }

            // Effects on target.
            if (target != null)
            {
                // Heal the target
                double heal = MagicManager.HealBase * 2.95 * caster.Attributes.CalculateMagicAttackFactor() * 0.1;
                target.Life += (int)heal;

                // Show the hp gain.
                game.DamageManager.Queue(new Damage((int)heal, null, target.Position, Damage.DamageType.Benefit, 0.1f, 10f));
            }
        }

        #endregion
    }
}
