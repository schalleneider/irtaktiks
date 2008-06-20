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
    /// The logic representation of one attack.
    /// </summary>
    public class Attack : Command
    {
        #region AttackType

        /// <summary>
        /// The types of attacks
        /// </summary>
        public enum AttackType
        {
            /// <summary>
            /// Close range attacks.
            /// </summary>
            Short,

            /// <summary>
            /// Long range attacks.
            /// </summary>
            Long
        }

        #endregion

        #region Properties

        /// <summary>
        /// The type of the attack.
        /// </summary>
        private AttackType TypeField;

        /// <summary>
        /// The type of the attack.
        /// </summary>
        public AttackType Type
        {
            get { return TypeField; }
        }
        
        /// <summary>
        /// Check if the attack can be used.
        /// </summary>
        public override bool Enabled
        {
            get { return true; }
        }

        #endregion

        #region Constructor
        
        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="unit">The owner of the attack.</param>
        /// <param name="name">The name of the attack.</param>
        /// <param name="type">The type of the attack.</param>
        /// <param name="commandExecute">Method that will be invoked when the attack executes.</param>
        public Attack(Unit unit, string name, AttackType type, CommandExecuteDelegate commandExecute)
            : base(unit, name, 0, commandExecute)
        {
            this.TypeField = type;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Execute the attack.
        /// </summary>
        /// <param name="target">The target of the attack.</param>
        /// <param name="position">The position of the target.</param>
        public override void Execute(Unit target, Vector2 position)
        {
            base.Execute(target, position);
        }

        #endregion
    }
}
