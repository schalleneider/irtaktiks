using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace IRTaktiks.Components.Managers
{
    /// <summary>
    /// Manager of spritebatchs.
    /// </summary>
    public class SpriteBatchManager
    {
        #region Interface

        /// <summary>
        /// Hold the basic properties and methods of the items that will be drawed.
        /// </summary>
        public abstract class SpriteDraw : IComparable<SpriteDraw>
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
            public int CompareTo(SpriteDraw other)
            {
                return this.Priority.CompareTo(other.Priority);
            }
        }

        #endregion

        #region Texture

        /// <summary>
        /// The item that holds the properties to draw textures.
        /// </summary>
        private class TextureDraw : SpriteDraw
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
            public TextureDraw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, int priority)
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

        #endregion

        #region Font

        /// <summary>
        /// The item that holds the properties to draw fonts.
        /// </summary>
        private class FontDraw : SpriteDraw
        {
            /// <summary>
            /// The sprite font.
            /// </summary>
            protected SpriteFont SpriteFont;
            
            /// <summary>
            /// The string to draw.
            /// </summary>
            protected string Text;
            
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
            /// <param name="text">The string to draw.</param>
            /// <param name="position">The location, in screen coordinates, where the text will be drawn.</param>
            /// <param name="color">The desired color of the text.</param>
            /// <param name="priority">The priority order to draw the text. Higher priorities will be on top.</param>
            public FontDraw(SpriteFont spriteFont, string text, Vector2 position, Color color, int priority)
            {
                this.SpriteFont = spriteFont;
                this.Text = text;
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
                spriteBatch.DrawString(this.SpriteFont, this.Text, this.Position, this.Color);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Enable sprites to be drawn.
        /// </summary>
        private SpriteBatch SpriteBatchField;

        /// <summary>
        /// Enable sprites to be drawn.
        /// </summary>
        public SpriteBatch SpriteBatch
        {
            get { return SpriteBatchField; }
        }

        /// <summary>
        /// The list of items to draw.
        /// </summary>
        private List<SpriteDraw> ListField;

        /// <summary>
        /// The list of items to draw.
        /// </summary>
        public List<SpriteDraw> List
        {
            get { return ListField; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of the class.
        /// </summary>
        public SpriteBatchManager(Game game)
        {
            this.ListField = new List<SpriteDraw>();
            this.SpriteBatchField = new SpriteBatch(game.GraphicsDevice);
        }

        #endregion

        #region Draw

        /// <summary>
        /// Queue the texture to be drawn at the flush of the SpriteBatchManager, according its priority.
        /// </summary>
        /// <param name="texture">The sprite texture.</param>
        /// <param name="position">The location, in screen coordinates, where the sprite will be drawn.</param>
        /// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting.</param>
        /// <param name="priority">The priority order to draw the texture. Higher priorities will be on top.</param>
        public void Draw(Texture2D texture, Vector2 position, Color color, int priority)
        {
            this.Draw(texture, new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height), null, color, priority);
        }
        
        /// <summary>
        /// Queue the texture to be drawn at the flush of the SpriteBatchManager, according its priority.
        /// </summary>
        /// <param name="texture">The sprite texture.</param>
        /// <param name="destinationRectangle">A rectangle specifying, in screen coordinates, where the sprite will be drawn. If this rectangle is not the same size as sourcerectangle, the sprite is scaled to fit.</param>
        /// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting.</param>
        /// <param name="priority">The priority order to draw the texture. Higher priorities will be on top.</param>
        public void Draw(Texture2D texture, Rectangle destinationRectangle, Color color, int priority)
        {
            this.Draw(texture, destinationRectangle, null, color, priority);
        }

        /// <summary>
        /// Queue the texture to be drawn at the flush of the SpriteBatchManager, according its priority.
        /// </summary>
        /// <param name="texture">The sprite texture.</param>
        /// <param name="position">The location, in screen coordinates, where the sprite will be drawn.</param>
        /// <param name="sourceRectangle">A rectangle specifying, in texels, which section of the rectangle to draw. Use null to draw the entire texture.</param>
        /// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting.</param>
        /// <param name="priority">The priority order to draw the texture. Higher priorities will be on top.</param>
        public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, int priority)
        {
            this.Draw(texture, new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Value.Width, sourceRectangle.Value.Height), sourceRectangle, color, priority);
        }

        /// <summary>
        /// Queue the texture to be drawn at the flush of the SpriteBatchManager, according its priority.
        /// </summary>
        /// <param name="texture">The sprite texture.</param>
        /// <param name="destinationRectangle">A rectangle specifying, in screen coordinates, where the sprite will be drawn. If this rectangle is not the same size as sourcerectangle, the sprite is scaled to fit.</param>
        /// <param name="sourceRectangle">A rectangle specifying, in texels, which section of the rectangle to draw. Use null to draw the entire texture.</param>
        /// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting.</param>
        /// <param name="priority">The priority order to draw the texture. Higher priorities will be on top.</param>
        public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, int priority)
        {
            this.List.Add(new TextureDraw(texture, destinationRectangle, sourceRectangle, color, priority));
        }

        /// <summary>
        /// Queue the text to be write at the flush of the SpriteBatchManager.
        /// </summary>
        /// <param name="spriteFont">The sprite font.</param>
        /// <param name="text">The string to draw.</param>
        /// <param name="position">The location, in screen coordinates, where the text will be drawn.</param>
        /// <param name="color">The desired color of the text.</param>
        /// <param name="priority">The priority order to draw the text. Higher priorities will be on top.</param>
        public void DrawString(SpriteFont spriteFont, string text, Vector2 position, Color color, int priority)
        {
            this.List.Add(new FontDraw(spriteFont, text, position, color, priority));
        }

        #endregion

        #region Flush

        /// <summary>
        /// Flushes all queued items to be drawn.
        /// </summary>
        public void Flush()
        {
            // Sort the items of the list, using the priority.
            this.List.Sort();
            
            this.SpriteBatch.Begin();

            // Draws all items in the list.
            for (int index = 0; index < this.List.Count; index++)
            {
                this.List[index].Draw(this.SpriteBatch);
            }

            this.SpriteBatch.End();

            // Clear the list.
            this.List.Clear();
        }

        #endregion
    }
}
