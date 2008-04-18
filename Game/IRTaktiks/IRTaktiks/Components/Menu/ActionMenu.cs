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
	/// Representation of one action from the unit.
	/// </summary>
	public class ActionMenu
	{
        #region Properties

        /// <summary>
        /// The unit owner of the action.
        /// </summary>
        private Unit UnitField;
        
        /// <summary>
        /// The unit owner of the action.
        /// </summary>
        public Unit Unit
        {
            get { return UnitField; }
        }

        /// <summary>
        /// The commands of this action.
        /// </summary>
        private List<CommandMenu> CommandsField;

        /// <summary>
        /// The commands of this action.
        /// </summary>
        public List<CommandMenu> Commands
        {
            get { return CommandsField; }
        }
        
        /// <summary>
        /// The text of the action.
        /// </summary>
        private string TextField;

        /// <summary>
        /// The text of the action.
        /// </summary>
        public string Text
        {
            get { return TextField; }
        }

        /// <summary>
        /// The actual position of the item.
        /// </summary>
        private Vector2 PositionField;

        /// <summary>
        /// The actual position of the item.
        /// </summary>
        public Vector2 Position
        {
            get { return PositionField; }
            set { PositionField = value; }
        }

        /// <summary>
        /// For actions who have commands, indicates its selection.
        /// </summary>
        private bool IsSelectedField;

        /// <summary>
        /// For actions who have commands, indicates its selection.
        /// </summary>
        public bool IsSelected
        {
            get { return IsSelectedField; }
            set { IsSelectedField = true; }
        }

        #endregion

        #region Textures and SpriteFonts

        /// <summary>
        /// The texture of the item, when its not selected.
        /// </summary>
        private Texture2D ItemTexture;
        
        /// <summary>
        /// The texture of the item, when its selected.
        /// </summary>
        private Texture2D SelectedItemTexture;

        /// <summary>
        /// The sprite font that will be used to write the command.
        /// </summary>
        private SpriteFont TextSpriteFont;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="unit">The unit that will be the actor of the command.</param>
        /// <param name="text">The text of the command that will be displayed.</param>
        public ActionMenu(Unit unit, string text)
        {
            // Set the properties.
            this.UnitField = unit;
            this.TextField = text;

            // Set the correct textures.
            this.ItemTexture = TextureManager.Instance.Sprites.Menu.Item;
            this.SelectedItemTexture = TextureManager.Instance.Sprites.Menu.SelectedItem;

            // Set the font to draw the text.
            this.TextSpriteFont = SpriteFontManager.Instance.Chilopod16;
            
            // Create the commands.
            this.CommandsField = new List<CommandMenu>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Draws the menuitem at the specified position.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatche that will be used to draw the textures.</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            // Draws the item of the menu.
            spriteBatch.Draw(this.IsSelected ? this.SelectedItemTexture : this.ItemTexture, this.Position, Color.White);

            // Measure the text size.
            Vector2 textSize = this.TextSpriteFont.MeasureString(this.Text);

            // Calculate the position of the texts.
            Vector2 textPosition = new Vector2(this.Position.X + this.ItemTexture.Width / 2 - textSize.X / 2, this.Position.Y + this.ItemTexture.Height / 2 - textSize.Y / 2);
            
            // Draws the name of the player.
            spriteBatch.DrawString(this.TextSpriteFont, this.Text, textPosition, Color.White);
        }

        #endregion
    }
}