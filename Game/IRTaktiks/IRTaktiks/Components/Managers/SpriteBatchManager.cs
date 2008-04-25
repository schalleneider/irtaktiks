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

        #endregion

        #region Lists

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of the class.
        /// </summary>
        public SpriteBatchManager(Game game)
        {
            this.SpriteBatchField = new SpriteBatch(game.GraphicsDevice);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Queue the texture to be drawn at the flush of the SpriteBatchManager, according its priority.
        /// </summary>
        /// <param name="texture">The sprite texture.</param>
        /// <param name="destinationRectangle">A rectangle specifying, in screen coordinates, where the sprite will be drawn. If this rectangle is not the same size as sourcerectangle, the sprite is scaled to fit.</param>
        /// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting.</param>
        /// <param name="priority">The priority order to draw the texture. Higher priorities will be on top.</param>
        public void Draw(Texture2D texture, Rectangle destinationRectangle, Color color, int priority)
        {

        }

        /// <summary>
        /// Queue the texture to be drawn at the flush of the SpriteBatchManager, according its priority.
        /// </summary>
        /// <param name="texture">The sprite texture.</param>
        /// <param name="position">The location, in screen coordinates, where the sprite will be drawn.</param>
        /// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting.</param>
        /// <param name="priority">The priority order to draw the texture. Higher priorities will be on top.</param>
        public void Draw(Texture2D texture, Vector2 position, Color color, int priority)
        {

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

        }

        /// <summary>
        /// Flushes all queued sprites to be drawn.
        /// </summary>
        public void Flush()
        {
            this.SpriteBatch.Begin();

            this.SpriteBatch.End();
        }

        #endregion
    }
}
