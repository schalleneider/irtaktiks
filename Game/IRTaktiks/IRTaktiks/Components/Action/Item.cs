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
    /// The logic representation of one item.
    /// </summary>
    public class Item : Command
    {
        #region Properties

        /// <summary>
        /// The quantity of items that the unit have.
        /// </summary>
        protected int QuantityField;

        /// <summary>
        /// The quantity of items that the unit have.
        /// </summary>
        public int Quantity
        {
            get { return QuantityField; }
        }

        /// <summary>
        /// Check if the item can be used.
        /// </summary>
        public bool Enabled
        {
            get { return this.Quantity > 0; }
        }

        #endregion

        #region Constructor
        
        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="unit">The owner of the item.</param>
        /// <param name="name">The name of the item.</param>
        /// <param name="quantity">The quantity of items that the unit have.</param>
        /// <param name="commandExecute">Method that will be invoked when the item is used.</param>
        public Item(Unit unit, string name, int quantity, CommandExecuteDelegate commandExecute)
            : base(unit, name, commandExecute)
        {
            this.QuantityField = quantity;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Execute the skill.
        /// </summary>
        /// <param name="target">The target of the skill.</param>
        public override void Execute(Unit target)
        {
            this.QuantityField--;
            base.Execute(target);
        }

        #endregion
    }
}
