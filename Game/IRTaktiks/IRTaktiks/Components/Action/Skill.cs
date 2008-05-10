using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IRTaktiks.Components.Playable;

namespace IRTaktiks.Components.Action
{
    /// <summary>
    /// The logic representation of one skill.
    /// </summary>
    public class Skill : Command
    {
        #region SkillType

        public enum SkillType
        {
            /// <summary>
            /// Increses Agility.
            /// Knight and Monk.
            /// </summary>
            Quick,

            /// <summary>
            /// Damage x3 and 15% to double the damage.
            /// Knight.
            /// </summary>
            Impact,

            /// <summary>
            /// Damages the MP. 
            /// Knight.
            /// </summary>
            MoonSlash,

            /// <summary>
            /// Restore HP.
            /// Paladin and Priest.
            /// </summary>
            Heal,

            /// <summary>
            /// Increases Vitality.
            /// Paladin.
            /// </summary>
            Fortitude,

            /// <summary>
            /// Damages 50% HP of the enemy in exchange of 50% Paladin's HP.
            /// Paladin.
            /// </summary>
            Final,

            /// <summary>
            /// Convert HP from enemy to MP.
            /// Wizard.
            /// </summary>
            Drain,

            /// <summary>
            /// Fire magic.
            /// Wizard.
            /// </summary>
            Firework,

            /// <summary>
            /// Ice magic.
            /// Wizard.
            /// </summary>
            Snowstorm,

            /// <summary>
            /// Increases the DEF and MDEF.
            /// Priest.
            /// </summary>
            Barrier,
            
            /// <summary>
            /// Holy magic.
            /// Priest.
            /// </summary>
            Holy,

            /// <summary>
            /// Increases the Dexterity.
            /// Assasin.
            /// </summary>
            Focus,

            /// <summary>
            /// Damage x5 and HIT -30%.
            /// Assasin.
            /// </summary>
            Deathblow,

            /// <summary>
            /// ?% of kill the target.
            /// Assasin.
            /// </summary>
            Curse,

            /// <summary>
            /// Increases the Strenght.
            /// Monk.
            /// </summary>
            Warcry,

            /// <summary>
            /// 5 attacks in a row.
            /// Monk.
            /// </summary>
            Release,

            /// <summary>
            /// Damage x10 and MP = 0.
            /// Monk.
            /// </summary>
            Reject,
        }

        #endregion

        #region Properties

        /// <summary>
        /// The cost mana of the skill.
        /// </summary>
        protected int CostField;

        /// <summary>
        /// The cost mana of the skill.
        /// </summary>
        public int Cost
        {
            get { return CostField; }
        }

        /// <summary>
        /// The type of the skill.
        /// </summary>
        private SkillType TypeField;

        /// <summary>
        /// The type of the skill.
        /// </summary>
        public SkillType Type
        {
            get { return TypeField; }
        }
        
        /// <summary>
        /// Check if the skill can be used.
        /// </summary>
        public bool Enabled
        {
            get { return this.Unit.Mana >= this.Cost; }
        }

        #endregion

        #region Constructor
        
        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="unit">The owner of the skill.</param>
        /// <param name="name">The name of the skill.</param>
        /// <param name="cost">The cost mana of the skill.</param>
        /// <param name="type">The type of the skill.</param>
        /// <param name="commandExecute">Method that will be invoked when the skill executes.</param>
        public Skill(Unit unit, string name, int cost, SkillType type, CommandExecuteDelegate commandExecute)
            : base(unit, name, commandExecute)
        {
            this.CostField = cost;
            this.TypeField = type;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Execute the skill.
        /// </summary>
        /// <param name="target">The target of the skill.</param>
        public override void Execute(Unit target)
        {
            base.Execute(target);
        }

        #endregion
    }
}
