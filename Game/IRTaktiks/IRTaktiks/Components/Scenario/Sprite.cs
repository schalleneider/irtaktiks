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
    /// The item that holds the properties to draw textures.
    /// </summary>
    public class Sprite : Drawable
    {
        /// <summary>
        /// The sprite texture.
        /// </summary>
        protected Texture2D Texture;

        /// <summary>
        /// A rectangle specifying, in screen coordinates, where the sprite will be drawn.
        /// </summary>
        protected Rectangle DestinationRectangle;

        /// <summary>
        /// A rectangle specifying, in texels, which section of the rectangle to draw.
        /// </summary>
        protected Rectangle? SourceRectangle;

        /// <summary>
        /// The color channel modulation to use.
        /// </summary>
        protected Color Color;

        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="texture">The sprite texture.</param>
        /// <param name="destinationRectangle">A rectangle specifying, in screen coordinates, where the sprite will be drawn. If this rectangle is not the same size as sourcerectangle, the sprite is scaled to fit.</param>
        /// <param name="sourceRectangle">A rectangle specifying, in texels, which section of the rectangle to draw. Use null to draw the entire texture.</param>
        /// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting.</param>
        /// <param name="priority">The priority order to draw the texture. Higher priorities will be on top.</param>
        public Sprite(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, int priority)
        {
            this.Texture = texture;
            this.DestinationRectangle = destinationRectangle;
            this.SourceRectangle = sourceRectangle;
            this.Color = color;
            this.Priority = priority;
        }

        /// <summary>
        /// Draws the item, using the given spritebatch.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch used to draw the item.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.DestinationRectangle, this.SourceRectangle, this.Color);
        }
    }
}
