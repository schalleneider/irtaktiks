using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IRTaktiks.Components.Scenario;

namespace IRTaktiks.Components.Manager
{
    /// <summary>
    /// Manager of spritebatchs.
    /// </summary>
    public class SpriteManager
    {
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
        private List<Drawable> ListField;

        /// <summary>
        /// The list of items to draw.
        /// </summary>
        public List<Drawable> List
        {
            get { return ListField; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="game">The instance of the game.</param>
        public SpriteManager(Game game)
        {
            this.ListField = new List<Drawable>(50);
            this.SpriteBatchField = new SpriteBatch(game.GraphicsDevice);
        }

        #endregion

        #region Draw

        /// <summary>
        /// Queue the texture to be drawn at the flush, according its priority.
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
        /// Queue the texture to be drawn at the flush, according its priority.
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
        /// Queue the texture to be drawn at the flush, according its priority.
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
        /// Queue the texture to be drawn at the flush, according its priority.
        /// </summary>
        /// <param name="texture">The sprite texture.</param>
        /// <param name="destinationRectangle">A rectangle specifying, in screen coordinates, where the sprite will be drawn. If this rectangle is not the same size as sourcerectangle, the sprite is scaled to fit.</param>
        /// <param name="sourceRectangle">A rectangle specifying, in texels, which section of the rectangle to draw. Use null to draw the entire texture.</param>
        /// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting.</param>
        /// <param name="priority">The priority order to draw the texture. Higher priorities will be on top.</param>
        public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, int priority)
        {
            this.List.Add(new Sprite(texture, destinationRectangle, sourceRectangle, color, priority));
        }

        /// <summary>
        /// Queue the text to be write at the flush, according its priority.
        /// </summary>
        /// <param name="spriteFont">The sprite font.</param>
        /// <param name="text">The string to draw.</param>
        /// <param name="position">The location, in screen coordinates, where the text will be drawn.</param>
        /// <param name="color">The desired color of the text.</param>
        /// <param name="priority">The priority order to draw the text. Higher priorities will be on top.</param>
        public void DrawString(SpriteFont spriteFont, string text, Vector2 position, Color color, int priority)
        {
            this.List.Add(new Text(spriteFont, text, position, color, priority));
        }

        #endregion

        #region Flush

        /// <summary>
        /// Flushes all queued sprites to be drawn.
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