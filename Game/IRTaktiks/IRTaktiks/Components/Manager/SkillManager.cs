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
                        skills.Add(new Skill(unit, "Heal", 95, Skill.SkillType.Heal, QueueCast));
                        skills.Add(new Skill(unit, "Barrier", 70, Skill.SkillType.Barrier, QueueCast));
                        skills.Add(new Skill(unit, "Holy", 182, Skill.SkillType.Holy, QueueCast)); 
                        
                        return skills;
                    }

                case Job.Wizard:
                    {
                        return skills;
                    }

                default:
                    {
                        return skills;
                    }
            }
        }

        #endregion

        #region Queue

        /// <summary>
        /// Queue the cast of the skill.
        /// </summary>
        /// <param name="command">Skill that will be casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        private void QueueCast(Command command, Unit caster, Unit target)
        {
            Skill skill = command as Skill;
        }

        /// <summary>
        /// Cast the skill.
        /// </summary>
        /// <param name="data">Data tranferred across the threads.</param>
        private void Cast(object data)
        {

        }

        #endregion

        #region Casts



        #endregion
    }
}
