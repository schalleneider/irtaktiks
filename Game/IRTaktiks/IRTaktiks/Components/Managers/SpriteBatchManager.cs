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
        public SpriteBatchManager()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Queue the texture to be drawn at the flush of the SpriteBatchManager, according its priority.
        /// </summary>
        /// <param name="texture">The sprite texture.</param>
        /// <param name="position">The location, in screen coordinates, where the sprite will be drawn.</param>
        /// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting.</param>
        /// <param name="priority">The priority order to draw the texture. Higher priorities will be drawed first.</param>
        public void Queue(Texture2D texture, Vector2 position, Color color, int priority)
        {
            this.Queue(texture, position, null, color, priority);
        }

        /// <summary>
        /// Queue the texture to be drawn at the flush of the SpriteBatchManager, according its priority.
        /// </summary>
        /// <param name="texture">The sprite texture.</param>
        /// <param name="position">The location, in screen coordinates, where the sprite will be drawn.</param>
        /// <param name="sourceRectangle">A rectangle specifying, in texels, which section of the rectangle to draw. Use null to draw the entire texture.</param>
        /// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting.</param>
        /// <param name="priority">The priority order to draw the texture. Higher priorities will be drawed first.</param>
        public void Queue(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, int priority)
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
