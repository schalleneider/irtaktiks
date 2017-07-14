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
    /// Delegate to execute the command.
    /// </summary>
    /// <param name="command">The command.</param>
    /// <param name="caster">The owner.</param>
    /// <param name="target">The target.</param>
    /// <param name="position">The position of the target.</param>
    public delegate void CommandExecuteDelegate(Command command, Unit caster, Unit target, Vector2 position);

    /// <summary>
    /// The logic representation of one command.
    /// </summary>
    public abstract class Command
    {
        #region Delegates

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
        /// The attribute of the command.
        /// </summary>
        private int AttributeField;
        
        /// <summary>
        /// The attribute of the command.
        /// </summary>
        public int Attribute
        {
            get { return AttributeField; }
            set { AttributeField = value; }
        }

        /// <summary>
        /// Check if the item can be used.
        /// </summary>
        public abstract bool Enabled
        {
            get;
        }
      
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="unit">The owner of the command.</param>
        /// <param name="name">The name of the command.</param>
        /// <param name="commandExecute">Method that will be invoked when the command executes.</param>
        public Command(Unit unit, string name, int attribute, CommandExecuteDelegate commandExecute)
        {
            this.UnitField = unit;
            this.NameField = name;
            this.AttributeField = attribute;

            this.CommandExecute = commandExecute;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="target">The target of the command.</param>
        /// <param name="position">The position of the target.</param>
        public virtual void Execute(Unit target, Vector2 position)
        {
            this.CommandExecute(this, this.Unit, target, position);
        }

        #endregion
    }
}
