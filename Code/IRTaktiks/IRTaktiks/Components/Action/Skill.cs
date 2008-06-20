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

        /// <summary>
        /// The types of skills.
        /// </summary>
        public enum SkillType
        {
            /// <summary>
            /// Skill self-usable.
            /// </summary>
            Self,

            /// <summary>
            /// Skill target-usable.
            /// </summary>
            Target
        }

        #endregion

        #region Properties
        
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
        public override bool Enabled
        {
            get { return this.Unit.Mana >= this.Attribute; }
        }

        #endregion

        #region Constructor
        
        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="unit">The owner of the skill.</param>
        /// <param name="name">The name of the skill.</param>
        /// <param name="type">The type of the skill.</param>
        /// <param name="type">The type of the skill.</param>
        /// <param name="commandExecute">Method that will be invoked when the skill executes.</param>
        public Skill(Unit unit, string name, int cost, SkillType type, CommandExecuteDelegate commandExecute)
            : base(unit, name, cost, commandExecute)
        {
            this.TypeField = type;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Execute the skill.
        /// </summary>
        /// <param name="target">The target of the skill.</param>
        /// <param name="position">The position of the target.</param>
        public override void Execute(Unit target, Vector2 position)
        {
            base.Execute(target, position);
        }

        #endregion
    }
}
