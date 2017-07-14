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

namespace IRTaktiks.Components.Scenario
{
    /// <summary>
    /// Hold the basic properties and methods of the items that will be drawed.
    /// </summary>
    public abstract class Drawable : IComparable<Drawable>
    {
        /// <summary>
        /// The priority order to draw the texture. Higher priorities will be on top.
        /// </summary>
        protected int Priority;

        /// <summary>
        /// Draws the item, using the given spritebatch.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch used to draw the item.</param>
        public abstract void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Compare this item with another.
        /// Used to sort a list of items.
        /// </summary>
        /// <param name="other">The other item to compare.</param>
        /// <returns>A signed number indicating the relative values of this instance and value.</returns>
        public int CompareTo(Drawable other)
        {
            return this.Priority.CompareTo(other.Priority);
        }
    }
}
