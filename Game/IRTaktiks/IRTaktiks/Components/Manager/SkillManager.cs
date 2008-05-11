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
        /// Construct the list of the skills based on attributes.
        /// </summary>
        /// <param name="unit">The unit that the skills will be constructed.</param>
        /// <returns>The list of skills.</returns>
        public List<Skill> Construct(Unit unit)
        {
            List<Skill> skills = new List<Skill>();

            switch (unit.Attributes.Job)
            {
                case Job.Assasin:

                    skills.Add(new Skill(unit, "Focus", 10, Skill.SkillType.Self, Focus));
                    skills.Add(new Skill(unit, "Deathblow", 10, Skill.SkillType.Target, Deathblow));
                    skills.Add(new Skill(unit, "Curse", 10, Skill.SkillType.Target, Curse));
                    break;

                case Job.Knight:

                    skills.Add(new Skill(unit, "Quick", 10, Skill.SkillType.Self, Quick));
                    skills.Add(new Skill(unit, "Impact", 10, Skill.SkillType.Target, Impact));
                    skills.Add(new Skill(unit, "Moonslash", 10, Skill.SkillType.Target, Moonslash));
                    break;

                case Job.Monk:

                    skills.Add(new Skill(unit, "Warcry", 10, Skill.SkillType.Self, Warcry));
                    skills.Add(new Skill(unit, "Insane", 10, Skill.SkillType.Target, Insane));
                    skills.Add(new Skill(unit, "Reject", 10, Skill.SkillType.Target, Reject));
                    break;

                case Job.Paladin:

                    skills.Add(new Skill(unit, "Fortitude", 10, Skill.SkillType.Self, Fortitude));
                    skills.Add(new Skill(unit, "Heal", 10, Skill.SkillType.Target, Heal));
                    skills.Add(new Skill(unit, "Final", 10, Skill.SkillType.Target, Final));
                    break;

                case Job.Priest:

                    skills.Add(new Skill(unit, "Heal", 10, Skill.SkillType.Target, Heal));
                    skills.Add(new Skill(unit, "Barrier", 10, Skill.SkillType.Target, Barrier));
                    skills.Add(new Skill(unit, "Holy", 10, Skill.SkillType.Target, Holy));
                    break;

                case Job.Wizard:

                    skills.Add(new Skill(unit, "Drain", 10, Skill.SkillType.Target, Drain));
                    skills.Add(new Skill(unit, "Firework", 10, Skill.SkillType.Target, Firework));
                    skills.Add(new Skill(unit, "Snowstorm", 10, Skill.SkillType.Target, Snowstorm));
                    break;
            }
            
            return skills;
        }

        #endregion

        #region Skills

        /// <summary>
        /// Cast the skill.
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Focus(Command command, Unit caster, Unit target, Vector2 position)
        {
        }

        /// <summary>
        /// Cast the skill.
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Deathblow(Command command, Unit caster, Unit target, Vector2 position)
        {
        }

        /// <summary>
        /// Cast the skill.
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Curse(Command command, Unit caster, Unit target, Vector2 position)
        {
        }

        /// <summary>
        /// Cast the skill.
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Quick(Command command, Unit caster, Unit target, Vector2 position)
        {
        }

        /// <summary>
        /// Cast the skill.
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Impact(Command command, Unit caster, Unit target, Vector2 position)
        {
        }

        /// <summary>
        /// Cast the skill.
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Moonslash(Command command, Unit caster, Unit target, Vector2 position)
        {
        }

        /// <summary>
        /// Cast the skill.
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Warcry(Command command, Unit caster, Unit target, Vector2 position)
        {
        }

        /// <summary>
        /// Cast the skill.
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Insane(Command command, Unit caster, Unit target, Vector2 position)
        {
        }

        /// <summary>
        /// Cast the skill.
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Reject(Command command, Unit caster, Unit target, Vector2 position)
        {
        }

        /// <summary>
        /// Cast the skill.
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Fortitude(Command command, Unit caster, Unit target, Vector2 position)
        {
        }
        
        /// <summary>
        /// Cast the skill.
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Heal(Command command, Unit caster, Unit target, Vector2 position)
        {
            // Obtain the game instance.
            IRTGame game = caster.Game as IRTGame;

            // Show the animation.
            Vector2 animationPosition = target == null ? position : new Vector2(target.Position.X + target.Texture.Width / 2, target.Position.Y + target.Texture.Height / 8);
            AnimationManager.Instance.QueueAnimation(AnimationManager.AnimationType.Heal, animationPosition);

            // Effects on caster.
            caster.Mana -= command.Attribute;
            caster.Time = 0;

            // Show the mp cost.
            if (caster != target)
            {
                game.DamageManager.Queue(new Damage(command.Attribute, "MP", caster.Position, Damage.DamageType.Harmful));
            }

            // Effects on target.
            if (target != null)
            {
                // Heal the target
                double heal = ((caster.Attributes.Level + caster.Attributes.Inteligence) / 8) * 44;
                target.Life += (int)heal;

                // Show the hp gain.
                game.DamageManager.Queue(new Damage((int)heal, null, target.Position, Damage.DamageType.Benefit));
            }
        }

        /// <summary>
        /// Cast the skill.
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Final(Command command, Unit caster, Unit target, Vector2 position)
        {
        }

        /// <summary>
        /// Cast the skill.
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Barrier(Command command, Unit caster, Unit target, Vector2 position)
        {
        }

        /// <summary>
        /// Cast the skill.
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Holy(Command command, Unit caster, Unit target, Vector2 position)
        {
        }

        /// <summary>
        /// Cast the skill.
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Drain(Command command, Unit caster, Unit target, Vector2 position)
        {
        }

        /// <summary>
        /// Cast the skill.
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Firework(Command command, Unit caster, Unit target, Vector2 position)
        {
        }

        /// <summary>
        /// Cast the skill.
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Snowstorm(Command command, Unit caster, Unit target, Vector2 position)
        {
        }

        #endregion
    }
}
