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
    /// The item that holds the properties to draw texts.
    /// </summary>
    public class Text : Drawable
    {
        /// <summary>
        /// The sprite font.
        /// </summary>
        protected SpriteFont SpriteFont;

        /// <summary>
        /// The string to draw.
        /// </summary>
        protected string Value;

        /// <summary>
        /// The location, in screen coordinates, where the text will be drawn.
        /// </summary>
        protected Vector2 Position;

        /// <summary>
        /// The desired color of the text.
        /// </summary>
        protected Color Color;

        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="spriteFont">The sprite font.</param>
        /// <param name="value">The string to draw.</param>
        /// <param name="position">The location, in screen coordinates, where the text will be drawn.</param>
        /// <param name="color">The desired color of the text.</param>
        /// <param name="priority">The priority order to draw the text. Higher priorities will be on top.</param>
        public Text(SpriteFont spriteFont, string value, Vector2 position, Color color, int priority)
        {
            this.SpriteFont = spriteFont;
            this.Value = value;
            this.Position = position;
            this.Color = color;
            this.Priority = priority;
        }

        /// <summary>
        /// Draws the item, using the given spritebatch.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch used to draw the item.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(this.SpriteFont, this.Value, this.Position, this.Color);
        }
    }
}
