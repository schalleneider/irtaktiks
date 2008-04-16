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
using IRTaktiks.Components.Playables;
using IRTaktiks.Components.Managers;

namespace IRTaktiks.Components.Menu
{
    public class ActionMenuItem
    {
        #region Items

        /// <summary>
        /// The subitems of the menu item.
        /// </summary>
        private List<ActionMenuItem> ItemsField;

        /// <summary>
        /// The subitems of the menu item.
        /// </summary>
        public List<ActionMenuItem> Items
        {
            get { if (ItemsField == null) ItemsField = new List<ActionMenuItem>(); return ItemsField; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// The parent item of this menuitem.
        /// </summary>
        private ActionMenuItem ParentField;

        /// <summary>
        /// The parent item of this menuitem.
        /// </summary>
        public ActionMenuItem Parent
        {
            get { return ParentField; }
        }

        /// <summary>
        /// The text that will be displayed.
        /// </summary>
        private string TextField;

        /// <summary>
        /// The text that will be displayed.
        /// </summary>
        public string Text
        {
            get { return TextField; }
        }

        /// <summary>
        /// Indicates that the menuitem is selected.
        /// </summary>
        private bool IsSelectedField;

        /// <summary>
        /// Indicates that the menuitem is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return IsSelectedField; }
            set { IsSelectedField = value; }
        }

        /// <summary>
        /// Indicates that the menuitem is enabled.
        /// </summary>
        private bool IsEnabledField;

        /// <summary>
        /// Indicates that the menuitem is enabled.
        /// </summary>
        public bool IsEnabled
        {
            get { return IsEnabledField; }
            set { IsEnabledField = true; }
        }
        
        /// <summary>
        /// Indicates that the menuitem is a root menu.
        /// </summary>
        public bool IsRoot
        {
            get { return this.Parent == null; }
        }

        /// <summary>
        /// The sprite font that will be used to write the menu item.
        /// </summary>
        private SpriteFont TextSpriteFont;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="parent">The parent item of this menuitem.</param>
        /// <param name="text">The text that will be displayed.</param>
        public ActionMenuItem(ActionMenuItem parent, string text)
        {
            this.ParentField = parent;
            this.TextField = text;

            this.TextSpriteFont = SpriteFontManager.Instance.Chilopod14;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Draws the menuitem at the specified position.
        /// </summary>
        /// <param name="position">The position that the menuitem will be drawed.</param>
        /// <param name="spriteBatch">SpriteBatche that will be used to draw the textures.</param>
        public void Draw(Vector2 position, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.Instance.Sprites.Menu.Item, position, Color.White);
        }

        #endregion
    }
}
