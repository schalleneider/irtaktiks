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
using IRTaktiks.Components.Manager;

namespace IRTaktiks.Components.Action
{
    /// <summary>
    /// Delegate to check if the unit can use the command.
    /// </summary>
    /// <param name="unit">The command.</param>
    /// <param name="cost">The unit.</param>
    /// <returns>True, if the unit can use the command, false otherwise.</returns>
    public delegate bool CommandEnabledDelegate(Command command, Unit unit);

    /// <summary>
    /// Delegate to execute the command.
    /// </summary>
    /// <param name="command">The command.</param>
    /// <param name="caster">The owner.</param>
    /// <param name="target">The target.</param>
    public delegate void CommandExecuteDelegate(Command command, Unit caster, Unit target);

    /// <summary>
    /// The logic representation of one command.
    /// </summary>
    public class Command
    {
        #region Delegates

        /// <summary>
        /// Delegate to check if the unit can use the item.
        /// </summary>
        public CommandEnabledDelegate CommandEnabled;

        /// <summary>
        /// Delegate to execute the item.
        /// </summary>
        public CommandExecuteDelegate CommandExecute;

        #endregion

        #region Properties

        /// <summary>
        /// The name of the command.
        /// </summary>
        protected string NameField;

        /// <summary>
        /// The name of the command.
        /// </summary>
        public string Name
        {
            get { return NameField; }
        }

        /// <summary>
        /// The owner of the command.
        /// </summary>
        private Unit UnitField;

        /// <summary>
        /// The owner of the command.
        /// </summary>
        public Unit Unit
        {
            get { return UnitField; }
        }

        /// <summary>
        /// The type of the animation that the skill will display.
        /// </summary>
        private AnimationManager.AnimationType AnimationTypeField;

        /// <summary>
        /// The type of the animation that the skill will display.
        /// </summary>
        public AnimationManager.AnimationType AnimationType
        {
            get { return AnimationTypeField; }
        }

        /// <summary>
        /// Check if the command can be used.
        /// </summary>
        public bool Enabled
        {
            get { return this.CommandEnabled(this, this.Unit); }
        }
      
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="unit">The owner of the command.</param>
        /// <param name="name">The name of the command.</param>
        /// <param name="animationType">The type of the animation that the skill will display.</param>
        public Command(Unit unit, string name, AnimationManager.AnimationType animationType)
        {
            this.UnitField = unit;
            this.NameField = name;
            this.AnimationTypeField = animationType;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="target">The target of the command.</param>
        public void Execute(Unit target)
        {
            this.SkillCast(this.Unit, target);
        }

        #endregion
    }
}
