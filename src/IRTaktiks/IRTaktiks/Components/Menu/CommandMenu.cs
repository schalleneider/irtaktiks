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
using IRTaktiks.Components.Action;

namespace IRTaktiks.Components.Menu
{
    /// <summary>
    /// Representation of one command from the unit.
    /// </summary>
    public class CommandMenu : ActionMenu
    {
        #region Properties

        /// <summary>
        /// The command of the unit.
        /// </summary>
        private Command CommandField;
        
        /// <summary>
        /// The command of the unit.
        /// </summary>
        public Command Command
        {
            get { return CommandField; }
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
        /// <param name="command">The command of the unit.</param>
        /// <param name="type">The type of the action owner of the command.</param>
        public CommandMenu(Command command, ActionMenuType type)
            : base(command.Unit, command.Name, type)
        {
            this.CommandField = command;

            if (this.Unit.Player.PlayerIndex == PlayerIndex.One)
            {
                this.ItemTexture = TextureManager.Instance.Sprites.Menu.SubmenuItemPlayerOne;
            }
            else
            {
                this.ItemTexture = TextureManager.Instance.Sprites.Menu.SubmenuItemPlayerTwo;
            }

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

            if (this.Command.Enabled)
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
            Vector2 attributeSize = this.AttributeSpriteFont.MeasureString(this.Command.Attribute.ToString());

            // Calculate the position of the text and attribute.
            Vector2 textPosition = new Vector2(this.Position.X + 54 - textSize.X / 2, this.Position.Y + this.ItemTexture.Height / 2 - textSize.Y / 2);
            Vector2 attributePosition = new Vector2(this.Position.X + 125 - attributeSize.X / 2, this.Position.Y + this.ItemTexture.Height / 2 - attributeSize.Y / 2);

            // Draws the item of the menu.
            spriteBatchManager.Draw(textureToDraw, this.Position, Color.White, 50);

            // Draws the text and attribute.
            spriteBatchManager.DrawString(this.TextSpriteFont, this.Text, textPosition, Color.White, 51);
            spriteBatchManager.DrawString(this.AttributeSpriteFont, this.Command.Attribute.ToString(), attributePosition, Color.White, 51);
        }

        #endregion
    }
}
