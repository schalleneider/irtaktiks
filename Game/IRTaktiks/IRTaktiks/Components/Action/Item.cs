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
            get { return this.ItemEnabled(this.Unit, this.Quantity); }
        }
                
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="unit">The owner of the item.</param>
        /// <param name="name">The name of the item.</param>
        /// <param name="quantity">The quantity of items.</param>
        public Item(Unit unit, string name, int quantity)
            : base(unit, name)
        {
            this.QuantityField = quantity;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Use the item.
        /// </summary>
        /// <param name="target">The target of the item.</param>
        public void Use(Unit target)
        {
            this.ItemUse(this.Unit, target);
        }

        #endregion
    }
}
