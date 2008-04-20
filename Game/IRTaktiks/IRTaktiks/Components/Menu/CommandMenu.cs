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
    /// <summary>
    /// Representation of one command from the unit.
    /// </summary>
    public class CommandMenu : ActionMenu
    {
        #region Properties

        /// <summary>
        /// The attribute of the command.
        /// </summary>
        private int AttributeField;
        
        /// <summary>
        /// The attribute of the command.
        /// </summary>
        public int Attribute
        {
            get { return AttributeField; }
        }
        
        /// <summary>
        /// Indicates that the command can be executed.
        /// </summary>
        private bool IsEnabledField;

        /// <summary>
        /// Indicates that the command can be executed.
        /// </summary>
        public bool IsEnabled
        {
            get { return IsEnabledField; }
            set { IsEnabledField = true; }
        }

        #endregion
        
        #region Textures and SpriteFonts

        /// <summary>
        /// The sprite font that will be used to write the attribute value.
        /// </summary>
        private SpriteFont AttributeSpriteFont;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="unit">The unit that will be the actor of the command.</param>
        /// <param name="text">The text of the command that will be displayed.</param>
        /// <param name="attribute">The attribute of the command.</param>
        public CommandMenu(Unit unit, string text, int attribute)
            : base(unit, text)
        {
            this.AttributeField = attribute;

            this.ItemTexture = TextureManager.Instance.Sprites.Menu.SubmenuItem;
            this.SelectedItemTexture = TextureManager.Instance.Sprites.Menu.SubmenuSelectedItem;

            this.TextSpriteFont = SpriteFontManager.Instance.Chilopod14;
            this.AttributeSpriteFont = SpriteFontManager.Instance.Chilopod12;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Draws the subitem.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch that will be used to draw the textures.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draws the item of the menu.
            spriteBatch.Draw(this.IsSelected ? this.SelectedItemTexture : this.ItemTexture, this.Position, Color.White);

            // Measure the text and attribute size.
            Vector2 textSize = this.TextSpriteFont.MeasureString(this.Text);
            Vector2 attributeSize = this.AttributeSpriteFont.MeasureString(this.Attribute.ToString());

            // Calculate the position of the text and attribute.
            Vector2 textPosition = new Vector2(this.Position.X + 54 - textSize.X / 2, this.Position.Y + this.ItemTexture.Height / 2 - textSize.Y / 2);
            Vector2 attributePosition = new Vector2(this.Position.X + 125 - attributeSize.X / 2, this.Position.Y + this.ItemTexture.Height / 2 - attributeSize.Y / 2);

            // Draws the text and attribute.
            spriteBatch.DrawString(this.TextSpriteFont, this.Text, textPosition, Color.White);
            spriteBatch.DrawString(this.AttributeSpriteFont, this.Attribute.ToString(), attributePosition, Color.White);
        }

        #endregion
    }
}
