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
        #region ItemType

        /// <summary>
        /// The types of items
        /// </summary>
        public enum ItemType
        {
            /// <summary>
            /// Item target-usable.
            /// </summary>
            Target,

            /// <summary>
            /// Item self-usable.
            /// </summary>
            Self
        }

        #endregion

        #region Properties

        /// <summary>
        /// The type of the item.
        /// </summary>
        private ItemType TypeField;

        /// <summary>
        /// The type of the item.
        /// </summary>
        public ItemType Type
        {
            get { return TypeField; }
        }

        /// <summary>
        /// Check if the item can be used.
        /// </summary>
        public override bool Enabled
        {
            get { return this.Attribute > 0; }
        }

        #endregion

        #region Constructor
        
        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="unit">The owner of the item.</param>
        /// <param name="name">The name of the item.</param>
        /// <param name="type">The type of the item.</param>
        /// <param name="quantity">The quantity of items that the unit have.</param>
        /// <param name="commandExecute">Method that will be invoked when the item is used.</param>
        public Item(Unit unit, string name, int quantity, ItemType type, CommandExecuteDelegate commandExecute)
            : base(unit, name, quantity, commandExecute)
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
            this.Attribute -= 1;
            base.Execute(target, position);
        }

        #endregion
    }
}
