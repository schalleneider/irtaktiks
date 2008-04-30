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
using IRTaktiks.Components.Manager;

namespace IRTaktiks.Components.Menu
{
    /// <summary>
    /// The method template who will used to handle the enabled property.
    /// </summary> 
    /// <param name="unit">The unit who want to use the command associated with this menu.</param>
    /// <returns>True, if the unit can use the command. False otherwise.</returns>
    public delegate bool EnabledDelegate(Unit unit);
    
    /// <summary>
    /// Representation of one command from the unit.
    /// </summary>
    public class CommandMenu : ActionMenu
    {
        #region Delegate

        /// <summary>
        /// The pointer to the method who will check if the unit can execute the command.
        /// </summary>
        private EnabledDelegate CheckEnabled;

        #endregion

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
        public bool Enabled
        {
            get { return this.CheckEnabled(this.Unit); }
        }

        #endregion
        
        #region Textures and SpriteFonts

        /// <summary>
        /// The texture of the item, when its disabled.
        /// </summary>
        private Texture2D DisabledItemTexture;

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
        /// <param name="checkEnabled">The method who will check if the unit can execute the command.</param>
        public CommandMenu(Unit unit, string text, int attribute, EnabledDelegate checkEnabled)
            : base(unit, text)
        {
            this.AttributeField = attribute;
            this.CheckEnabled = checkEnabled; 

            this.ItemTexture = TextureManager.Instance.Sprites.Menu.SubmenuItem;
            this.SelectedItemTexture = TextureManager.Instance.Sprites.Menu.SubmenuSelectedItem;
            this.DisabledItemTexture = TextureManager.Instance.Sprites.Menu.SubmenuDisabledItem;

            this.TextSpriteFont = FontManager.Instance.Chilopod14;
            this.AttributeSpriteFont = FontManager.Instance.Chilopod12;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Draws the subitem.
        /// </summary>
        /// <param name="spriteBatchManager">SpriteBatchManager used to draw.</param>
        public override void Draw(SpriteManager spriteBatchManager)
        {
            Texture2D textureToDraw = this.DisabledItemTexture;

            if (this.Enabled)
            {
                if (this.Selected)
                {
                    textureToDraw = this.SelectedItemTexture;
                }
                else
                {
                    textureToDraw = this.ItemTexture;
                }
            }

            // Measure the text and attribute size.
            Vector2 textSize = this.TextSpriteFont.MeasureString(this.Text);
            Vector2 attributeSize = this.AttributeSpriteFont.MeasureString(this.Attribute.ToString());

            // Calculate the position of the text and attribute.
            Vector2 textPosition = new Vector2(this.Position.X + 54 - textSize.X / 2, this.Position.Y + this.ItemTexture.Height / 2 - textSize.Y / 2);
            Vector2 attributePosition = new Vector2(this.Position.X + 125 - attributeSize.X / 2, this.Position.Y + this.ItemTexture.Height / 2 - attributeSize.Y / 2);

            // Draws the item of the menu.
            spriteBatchManager.Draw(textureToDraw, this.Position, Color.White, 50);

            // Draws the text and attribute.
            spriteBatchManager.DrawString(this.TextSpriteFont, this.Text, textPosition, Color.White, 51);
            spriteBatchManager.DrawString(this.AttributeSpriteFont, this.Attribute.ToString(), attributePosition, Color.White, 51);
        }

        #endregion
    }
}
