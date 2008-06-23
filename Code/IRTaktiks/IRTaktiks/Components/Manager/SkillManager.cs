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
using System.Threading;

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

                    skills.Add(new Skill(unit, "Stealth", 50, Skill.SkillType.Self, Stealth));
                    skills.Add(new Skill(unit, "Ambush", 65, Skill.SkillType.Target, Ambush));
                    skills.Add(new Skill(unit, "Curse", 110, Skill.SkillType.Target, Curse));
                    break;

                case Job.Knight:

                    skills.Add(new Skill(unit, "Quick", 40, Skill.SkillType.Self, Quick));
                    skills.Add(new Skill(unit, "Impact", 70, Skill.SkillType.Target, Impact));
                    skills.Add(new Skill(unit, "Revenge", 75, Skill.SkillType.Target, Revenge));
                    break;

                case Job.Monk:

                    skills.Add(new Skill(unit, "Warcry", 45, Skill.SkillType.Self, Warcry));
                    skills.Add(new Skill(unit, "Insane", 90, Skill.SkillType.Target, Insane));
                    skills.Add(new Skill(unit, "Reject", 0, Skill.SkillType.Target, Reject));
                    break;

                case Job.Paladin:

                    skills.Add(new Skill(unit, "Might", 60, Skill.SkillType.Self, Might));
                    skills.Add(new Skill(unit, "Heal", 85, Skill.SkillType.Target, Heal));
                    skills.Add(new Skill(unit, "Unseal", 500, Skill.SkillType.Target, Unseal));
                    break;

                case Job.Priest:

                    skills.Add(new Skill(unit, "Heal", 85, Skill.SkillType.Target, Heal));
                    skills.Add(new Skill(unit, "Barrier", 80, Skill.SkillType.Target, Barrier));
                    skills.Add(new Skill(unit, "Holy", 180, Skill.SkillType.Target, Holy));
                    break;

                case Job.Wizard:

                    skills.Add(new Skill(unit, "Drain", 50, Skill.SkillType.Target, Drain));
                    skills.Add(new Skill(unit, "Flame", 210, Skill.SkillType.Target, Flame));
                    skills.Add(new Skill(unit, "Frost", 250, Skill.SkillType.Target, Frost));
                    break;
            }

            return skills;
        }

        #endregion

        #region Skills

        /// <summary>
        /// Increases the Dexterity of the caster for 60 seconds.
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Stealth(Command command, Unit caster, Unit target, Vector2 position)
        {
            // Obtain the game instance.
            IRTGame game = caster.Game as IRTGame;

            // Show the animation.
            Vector2 animationPosition = new Vector2(caster.Position.X + caster.Texture.Width / 2, caster.Position.Y + caster.Texture.Height / 8);
            AnimationManager.Instance.QueueAnimation(AnimationManager.AnimationType.Stealth, animationPosition);

            // Effects on caster.
            caster.Mana -= command.Attribute;
            caster.Time = 0;

            // Show the bonus.
            game.DamageManager.Queue(new Damage("DEX++", caster.Position, Damage.DamageType.Benefit));

            ThreadPool.QueueUserWorkItem(delegate(object data)
            {
                Unit unit = data as Unit;

                float oldDexterity = unit.Attributes.Dexterity;

                unit.Attributes.Dexterity *= 2.0f;

                Thread.Sleep(60000);

                unit.Attributes.Dexterity = oldDexterity;

            }, target);
        }

        /// <summary>
        /// 500% of one simple attack, with -50% of hit.
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Ambush(Command command, Unit caster, Unit target, Vector2 position)
        {
            // Obtain the game instance.
            IRTGame game = caster.Game as IRTGame;

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
                // Calcule the attack.
                double damage;

                damage = 5 * (caster.Attributes.Attack - 0.3f * target.Attributes.Defense);
                damage += this.Random.NextDouble() * 0.1f * damage;
                damage = damage < 1 ? 1 : damage;

                // Apply the hit and flee %.
                int percent = caster.Attributes.Hit / 2 - target.Attributes.Flee;

                // Limits of flee and hit.
                if (percent > 100) percent = 100;
                if (percent < 0) percent = 0;

                // Hit
                if (this.Random.Next(0, 100) <= percent)
                {
                    target.Life -= (int)damage;

                    Vector2 animationPosition = target == null ? position : new Vector2(target.Position.X + target.Texture.Width / 2, target.Position.Y + target.Texture.Height / 8);
                    AnimationManager.Instance.QueueAnimation(AnimationManager.AnimationType.Ambush, animationPosition);

                    game.DamageManager.Queue(new Damage((int)damage, null, target.Position, Damage.DamageType.Harmful));
                }

                // Miss
                else
                {
                    game.DamageManager.Queue(new Damage("MISS", target.Position, Damage.DamageType.Harmful));
                }
            }
        }

        /// <summary>
        /// 5% of the percentage to hit the target, to kill the target instantly.
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Curse(Command command, Unit caster, Unit target, Vector2 position)
        {
            // Obtain the game instance.
            IRTGame game = caster.Game as IRTGame;

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
                // Apply the percentage.
                int percent = Convert.ToInt32(Math.Ceiling(0.05f * (caster.Attributes.Hit / 2 - target.Attributes.Flee)));

                // Limits of flee and hit.
                if (percent > 100) percent = 100;
                if (percent < 0) percent = 0;

                // Hit
                if (this.Random.Next(0, 100) <= percent)
                {
                    target.Life = 0;

                    Vector2 animationPosition = target == null ? position : new Vector2(target.Position.X + target.Texture.Width / 2, target.Position.Y + target.Texture.Height / 8);
                    AnimationManager.Instance.QueueAnimation(AnimationManager.AnimationType.Curse, animationPosition);

                    game.DamageManager.Queue(new Damage("OWNED", target.Position, Damage.DamageType.Harmful));
                }

                // Miss
                else
                {
                    game.DamageManager.Queue(new Damage("MISS", target.Position, Damage.DamageType.Harmful));
                }
            }
        }

        /// <summary>
        /// Increases the Agility of the caster for 30 seconds.
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Quick(Command command, Unit caster, Unit target, Vector2 position)
        {
            // Obtain the game instance.
            IRTGame game = caster.Game as IRTGame;

            // Show the animation.
            Vector2 animationPosition = new Vector2(caster.Position.X + caster.Texture.Width / 2, caster.Position.Y + caster.Texture.Height / 8);
            AnimationManager.Instance.QueueAnimation(AnimationManager.AnimationType.Quick, animationPosition);

            // Effects on caster.
            caster.Mana -= command.Attribute;
            caster.Time = 0;

            // Show the bonus.
            game.DamageManager.Queue(new Damage("AGI++", caster.Position, Damage.DamageType.Benefit));

            ThreadPool.QueueUserWorkItem(delegate(object data)
            {
                Unit unit = data as Unit;

                float oldAgility = unit.Attributes.Agility;

                unit.Attributes.Agility *= 2.0f;

                Thread.Sleep(30000);

                unit.Attributes.Agility = oldAgility;

            }, target);
        }

        /// <summary>
        /// 300% of one simple attack, with aditional 15% to double the damage.
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Impact(Command command, Unit caster, Unit target, Vector2 position)
        {
            // Obtain the game instance.
            IRTGame game = caster.Game as IRTGame;

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
                // Calcule the attack.
                double damage;

                damage = 3 * (caster.Attributes.Attack - 0.3f * target.Attributes.Defense);
                damage += this.Random.NextDouble() * 0.1f * damage;
                damage = damage < 1 ? 1 : damage;

                // Apply the hit and flee %.
                int percent = caster.Attributes.Hit - target.Attributes.Flee;

                // Limits of flee and hit.
                if (percent > 100) percent = 100;
                if (percent < 0) percent = 0;

                // Hit
                if (this.Random.Next(0, 100) <= percent)
                {
                    // Apply the 15% to double the attack.
                    if (this.Random.Next(0, 100) <= 15)
                    {
                        damage *= 2.0f;
                    }

                    target.Life -= (int)damage;

                    Vector2 animationPosition = target == null ? position : new Vector2(target.Position.X + target.Texture.Width / 2, target.Position.Y + target.Texture.Height / 8);
                    AnimationManager.Instance.QueueAnimation(AnimationManager.AnimationType.Impact, animationPosition);

                    game.DamageManager.Queue(new Damage((int)damage, null, target.Position, Damage.DamageType.Harmful));
                }

                // Miss
                else
                {
                    game.DamageManager.Queue(new Damage("MISS", target.Position, Damage.DamageType.Harmful));
                }
            }
        }

        /// <summary>
        /// 50% of one simple attack, on the mp of the target.
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Revenge(Command command, Unit caster, Unit target, Vector2 position)
        {
            // Obtain the game instance.
            IRTGame game = caster.Game as IRTGame;

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
                // Calcule the attack.
                double damage;

                damage = 0.5f * (caster.Attributes.Attack - 0.3f * target.Attributes.MagicDefense);
                damage += this.Random.NextDouble() * 0.1f * damage;
                damage = damage < 1 ? 1 : damage;

                // Apply the hit and flee %.
                int percent = caster.Attributes.Hit - target.Attributes.Flee;

                // Limits of flee and hit.
                if (percent > 100) percent = 100;
                if (percent < 0) percent = 0;

                // Hit
                if (this.Random.Next(0, 100) <= percent)
                {
                    target.Mana -= (int)damage;

                    Vector2 animationPosition = target == null ? position : new Vector2(target.Position.X + target.Texture.Width / 2, target.Position.Y + target.Texture.Height / 8);
                    AnimationManager.Instance.QueueAnimation(AnimationManager.AnimationType.Revenge, animationPosition);

                    game.DamageManager.Queue(new Damage((int)damage, "MP", target.Position, Damage.DamageType.Harmful));
                }

                // Miss
                else
                {
                    game.DamageManager.Queue(new Damage("MISS", target.Position, Damage.DamageType.Harmful));
                }
            }
        }

        /// <summary>
        /// Increases the Strength of the caster for 30 seconds.
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Warcry(Command command, Unit caster, Unit target, Vector2 position)
        {
            // Obtain the game instance.
            IRTGame game = caster.Game as IRTGame;

            // Show the animation.
            Vector2 animationPosition = new Vector2(caster.Position.X + caster.Texture.Width / 2, caster.Position.Y + caster.Texture.Height / 8);
            AnimationManager.Instance.QueueAnimation(AnimationManager.AnimationType.Warcry, animationPosition);

            // Effects on caster.
            caster.Mana -= command.Attribute;
            caster.Time = 0;

            // Show the bonus.
            game.DamageManager.Queue(new Damage("STR++", caster.Position, Damage.DamageType.Benefit));

            ThreadPool.QueueUserWorkItem(delegate(object data)
            {
                Unit unit = data as Unit;

                float oldStrength = unit.Attributes.Strength;

                unit.Attributes.Strength *= 2.0f;

                Thread.Sleep(30000);

                unit.Attributes.Strength = oldStrength;

            }, target);
        }

        /// <summary>
        /// 5 consecutive attacks based in magic attack on the target.
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Insane(Command command, Unit caster, Unit target, Vector2 position)
        {
            // Obtain the game instance.
            IRTGame game = caster.Game as IRTGame;

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
                // Calcule the attack.
                double damage;

                // Five consecutive blows.
                for (int index = 0; index < 5; index++)
                {
                    damage = (caster.Attributes.MagicAttack - 0.3f * target.Attributes.MagicDefense);
                    damage += this.Random.NextDouble() * 0.1f * damage;
                    damage = damage < 1 ? 1 : damage;

                    // Apply the hit and flee %.
                    int percent = caster.Attributes.Hit - target.Attributes.Flee;

                    // Limits of flee and hit.
                    if (percent > 100) percent = 100;
                    if (percent < 0) percent = 0;

                    // Hit
                    if (this.Random.Next(0, 100) <= percent)
                    {
                        target.Life -= (int)damage;

                        Vector2 animationPosition = target == null ? position : new Vector2(target.Position.X + target.Texture.Width / 2, target.Position.Y + target.Texture.Height / 8);
                        AnimationManager.Instance.QueueAnimation(AnimationManager.AnimationType.Insane, animationPosition);

                        game.DamageManager.Queue(new Damage((int)damage, null, target.Position, Damage.DamageType.Harmful));
                    }

                    // Miss
                    else
                    {
                        game.DamageManager.Queue(new Damage("MISS", target.Position, Damage.DamageType.Harmful));
                    }

                    Thread.Sleep(500);
                }
            }
        }

        /// <summary>
        /// Large amount of psysical and magic damage, based on level, strength, inteligence and mana left. Cost all mana points of the unit.
        /// ((15 * (LVL / 100) * (ATK + MATK) - (0.5 * (DEF + MDEF)) * MANA / MAXMANA)
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Reject(Command command, Unit caster, Unit target, Vector2 position)
        {
            // Obtain the game instance.
            IRTGame game = caster.Game as IRTGame;

            // Effects on target.
            if (target != null)
            {
                // Calcule the attack.
                double damage;

                damage = ((15 * (caster.Attributes.Level / 100) * (caster.Attributes.Attack + caster.Attributes.MagicAttack)) - (0.5 * (caster.Attributes.Defense + caster.Attributes.MagicDefense)) * (float)caster.Mana / (float)caster.Attributes.MaximumMana);
                damage += this.Random.NextDouble() * 0.1f * damage;
                damage = damage < 1 ? 1 : damage;

                // Apply the hit and flee %.
                int percent = caster.Attributes.Hit - target.Attributes.Flee;

                // Limits of flee and hit.
                if (percent > 100) percent = 100;
                if (percent < 0) percent = 0;

                // Hit
                if (this.Random.Next(0, 100) <= percent)
                {
                    target.Life -= (int)damage;

                    Vector2 animationPosition = target == null ? position : new Vector2(target.Position.X + target.Texture.Width / 2, target.Position.Y + target.Texture.Height / 8);
                    AnimationManager.Instance.QueueAnimation(AnimationManager.AnimationType.Reject, animationPosition);

                    game.DamageManager.Queue(new Damage((int)damage, null, target.Position, Damage.DamageType.Harmful));
                }

                // Miss
                else
                {
                    game.DamageManager.Queue(new Damage("MISS", target.Position, Damage.DamageType.Harmful));
                }
            }

            // Show the mp cost.
            if (caster != target)
            {
                game.DamageManager.Queue(new Damage(caster.Mana, "MP", caster.Position, Damage.DamageType.Harmful));
            }

            // Effects on caster.
            caster.Mana = 0;
            caster.Time = 0;
        }

        /// <summary>
        /// Increases the Vitality of the caster for 60 seconds.
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Might(Command command, Unit caster, Unit target, Vector2 position)
        {
            // Obtain the game instance.
            IRTGame game = caster.Game as IRTGame;

            // Show the animation.
            Vector2 animationPosition = new Vector2(caster.Position.X + caster.Texture.Width / 2, caster.Position.Y + caster.Texture.Height / 8);
            AnimationManager.Instance.QueueAnimation(AnimationManager.AnimationType.Might, animationPosition);

            // Effects on caster.
            caster.Mana -= command.Attribute;
            caster.Time = 0;

            // Show the bonus.
            game.DamageManager.Queue(new Damage("VIT++", caster.Position, Damage.DamageType.Benefit));

            ThreadPool.QueueUserWorkItem(delegate(object data)
            {
                Unit unit = data as Unit;

                float oldVitality = unit.Attributes.Vitality;

                unit.Attributes.Vitality *= 2.0f;

                Thread.Sleep(60000);

                unit.Attributes.Vitality = oldVitality;

            }, target);
        }

        /// <summary>
        /// Heal the life of the target.
        /// ((LVL + INT) / 8) * 44)
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
                double damage = ((caster.Attributes.Level + caster.Attributes.Inteligence) / 8) * 44;
                damage += this.Random.NextDouble() * 0.1f * damage;
                damage = damage < 1 ? 1 : damage; 
                
                target.Life += (int)damage;

                // Show the damage.
                game.DamageManager.Queue(new Damage((int)damage, null, target.Position, Damage.DamageType.Benefit));
            }
        }

        /// <summary>
        /// Reduces 50% of the target life at the cost of the 50% of the caster life.
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Unseal(Command command, Unit caster, Unit target, Vector2 position)
        {
            // Obtain the game instance.
            IRTGame game = caster.Game as IRTGame;

            // Show the animation.
            Vector2 animationPosition = target == null ? position : new Vector2(target.Position.X + target.Texture.Width / 2, target.Position.Y + target.Texture.Height / 8);
            AnimationManager.Instance.QueueAnimation(AnimationManager.AnimationType.Unseal, animationPosition);

            // Effects on caster.
            caster.Life /= 2;
            caster.Time = 0;

            // Show the hp cost.
            if (caster != target)
            {
                game.DamageManager.Queue(new Damage(caster.Life, null, caster.Position, Damage.DamageType.Harmful));
            }

            // Effects on target.
            if (target != null)
            {
                target.Life /= 2;

                // Show the damage.
                game.DamageManager.Queue(new Damage(target.Life, null, target.Position, Damage.DamageType.Benefit));
            }
        }

        /// <summary>
        /// Increases the DEF and MDEF of the target for 60 seconds.
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Barrier(Command command, Unit caster, Unit target, Vector2 position)
        {
            // Obtain the game instance.
            IRTGame game = caster.Game as IRTGame;

            // Show the animation.
            Vector2 animationPosition = target == null ? position : new Vector2(target.Position.X + target.Texture.Width / 2, target.Position.Y + target.Texture.Height / 8);
            AnimationManager.Instance.QueueAnimation(AnimationManager.AnimationType.Barrier, animationPosition);

            // Effects on caster.
            caster.Mana -= command.Attribute;
            caster.Time = 0;

            // Show the bonus.
            game.DamageManager.Queue(new Damage("DEF++\nMDEF++", caster.Position, Damage.DamageType.Benefit));

            // Effects on target.
            if (target != null)
            {
                ThreadPool.QueueUserWorkItem(delegate(object data)
                {
                    Unit unit = data as Unit;

                    float oldDefenseFactor = unit.Attributes.DefenseFactor;
                    float oldMagicDefenseFactor = unit.Attributes.MagicDefenseFactor;

                    unit.Attributes.DefenseFactor *= 1.5f;
                    unit.Attributes.MagicDefenseFactor *= 1.5f;

                    Thread.Sleep(60000);

                    unit.Attributes.DefenseFactor = oldDefenseFactor;
                    unit.Attributes.MagicDefenseFactor = oldMagicDefenseFactor;

                }, target);
            }
        }

        /// <summary>
        /// Holy property skill.
        /// ((LVL + INT) / 6) * 60) - (2 * MDEF)
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Holy(Command command, Unit caster, Unit target, Vector2 position)
        {
            // Obtain the game instance.
            IRTGame game = caster.Game as IRTGame;

            // Show the animation.
            Vector2 animationPosition = target == null ? position : new Vector2(target.Position.X + target.Texture.Width / 2, target.Position.Y + target.Texture.Height / 8);
            AnimationManager.Instance.QueueAnimation(AnimationManager.AnimationType.Holy, animationPosition);

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
                // Calculate the damage.
                double damage = ((caster.Attributes.Level + caster.Attributes.Inteligence) / 6) * 50 - (2 * target.Attributes.MagicDefense);
                damage += this.Random.NextDouble() * 0.1f * damage;
                damage = damage < 1 ? 1 : damage;

                target.Life -= (int)damage;

                // Show the damage.
                game.DamageManager.Queue(new Damage((int)damage, null, target.Position, Damage.DamageType.Harmful));
            }
        }

        /// <summary>
        /// Steal the target's MP.
        /// (((T.INT / C.INT) * 5% T.MP) + 5% T.MP)
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Drain(Command command, Unit caster, Unit target, Vector2 position)
        {
            // Obtain the game instance.
            IRTGame game = caster.Game as IRTGame;

            // Show the animation.
            Vector2 animationPosition = target == null ? position : new Vector2(target.Position.X + target.Texture.Width / 2, target.Position.Y + target.Texture.Height / 8);
            AnimationManager.Instance.QueueAnimation(AnimationManager.AnimationType.Drain, animationPosition);
                        
            // Effects on target.
            if (target != null)
            {
                // Calculate the damage.
                double damage = (((target.Attributes.Inteligence / caster.Attributes.Inteligence) * (0.05f * target.Attributes.MaximumMana)) + (0.05f * target.Attributes.MaximumMana));
                damage += this.Random.NextDouble() * 0.1f * damage;
                damage = damage < 1 ? 1 : damage;

                target.Mana -= (int)damage;
                
                // Show the damage.
                game.DamageManager.Queue(new Damage((int)damage, "MP", target.Position, Damage.DamageType.Harmful));

                caster.Mana += (int)damage;

                // Show the gain.                
                game.DamageManager.Queue(new Damage((int)damage, "MP", caster.Position, Damage.DamageType.Benefit));
            }
            
            // Effects on caster.
            caster.Mana -= command.Attribute;
            caster.Time = 0;
        }

        /// <summary>
        /// Fire property skill.
        /// ((LVL + INT) / 6) * 60) - (2 * MDEF)
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Flame(Command command, Unit caster, Unit target, Vector2 position)
        {
            // Obtain the game instance.
            IRTGame game = caster.Game as IRTGame;

            // Show the animation.
            Vector2 animationPosition = target == null ? position : new Vector2(target.Position.X + target.Texture.Width / 2, target.Position.Y + target.Texture.Height / 8);
            AnimationManager.Instance.QueueAnimation(AnimationManager.AnimationType.Flame, animationPosition);

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
                // Calculate the damage.
                double damage = ((caster.Attributes.Level + caster.Attributes.Inteligence) / 6) * 50 - (2 * target.Attributes.MagicDefense);
                damage += this.Random.NextDouble() * 0.1f * damage;
                damage = damage < 1 ? 1 : damage;

                target.Life -= (int)damage;

                // Show the damage.
                game.DamageManager.Queue(new Damage((int)damage, null, target.Position, Damage.DamageType.Harmful));
            }
        }

        /// <summary>
        /// Ice property skill.
        /// ((LVL + INT) / 6) * 5) - (2 * MDEF)
        /// </summary>
        /// <param name="command">Skill casted.</param>
        /// <param name="caster">The caster of the skill.</param>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        private void Frost(Command command, Unit caster, Unit target, Vector2 position)
        {
            // Obtain the game instance.
            IRTGame game = caster.Game as IRTGame;

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
                // Five consecutive blows.
                for (int index = 0; index < 10; index++)
                {
                    // Calculate the damage.
                    double damage = ((caster.Attributes.Level + caster.Attributes.Inteligence) / 6) * 7 - (2 * target.Attributes.MagicDefense);
                    damage += this.Random.NextDouble() * 0.5f * damage;
                    damage = damage < 1 ? 1 : damage;

                    target.Life -= (int)damage;

                    // Show the damage.
                    game.DamageManager.Queue(new Damage((int)damage, null, target.Position, Damage.DamageType.Harmful));

                    Thread.Sleep(500);
                }
            }
        }

        #endregion
    }
}
