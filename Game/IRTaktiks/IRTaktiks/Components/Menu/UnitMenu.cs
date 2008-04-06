using System;
using System.Collections.Generic;
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
	/// Representation of the menu of the player.
	/// </summary>
	public class UnitMenu : DrawableGameComponent
	{
		#region Properties
			
		/// <summary>
        /// The unit whose information will be displayed.
        /// </summary>
        private Unit UnitField;

        /// <summary>
        /// The unit whose information will be displayed.
        /// </summary>
        public Unit Unit
        {
            get { return UnitField; }
        }

		/// <summary>
		/// The position of the menu.
		/// </summary>
		private Vector2 PositionField;

		/// <summary>
		/// The position of the menu.
		/// </summary>
		public Vector2 Position
		{
			get { return PositionField; }
		}

		/// <summary>
		/// The texture that will be used to draw the unit information.
		/// </summary>
		private Texture2D UnitTexture;

		/// <summary>
		/// The sprite font that will be used to write the unit's name.
		/// </summary>
		private SpriteFont NameSpriteFont;

		/// <summary>
		/// The sprite font that will be used to write the unit's life points.
		/// </summary>
		private SpriteFont LifeSpriteFont;

		/// <summary>
		/// The sprite font that will be used to write the unit's mana points.
		/// </summary>
		private SpriteFont ManaSpriteFont;

        #endregion

        #region Constructor

        /// <summary>
		/// Constructor of class.
		/// </summary>
		/// <param name="game">The instance of game that is running.</param>
        /// <param name="unit">The unit who is the owner of the menu.</param>
		public UnitMenu(Game game, Unit unit)
			: base(game)
		{
            // Set the unit whose information will be displayed.
			this.UnitField = unit;
            
            // Set the spritefonts that will be used.
            this.NameSpriteFont = SpriteFontManager.Instance.Chilopod16;
            this.LifeSpriteFont = SpriteFontManager.Instance.Chilopod12;
            this.ManaSpriteFont = SpriteFontManager.Instance.Chilopod12;

			// Player one informations.
			if (this.Unit.Player.PlayerIndex == PlayerIndex.One)
			{
				this.PositionField = new Vector2(0, 125);
			}

			// Player two informations.
			if (this.Unit.Player.PlayerIndex == PlayerIndex.Two)
			{
				this.PositionField = new Vector2(IRTGame.Width - TextureManager.Instance.BackgroundMenu.Width, 125);
			}
		}

		#endregion

		#region Component Methods

		/// <summary>
		/// Allows the game component to perform any initialization it needs to before starting
		/// to run. This is where it can query for any required services and load content.
		/// </summary>
		public override void Initialize()
		{
            base.Initialize();
		}

		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}

		/// <summary>
		/// Called when the DrawableGameComponent needs to be drawn. Override this method
		//  with component-specific drawing code.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Draw(GameTime gameTime)
		{
            IRTGame game = this.Game as IRTGame;

            game.SpriteBatch.Begin();

			// Set texture according the unit status.
            if (this.Unit.IsStatusAlive)
            {
				this.UnitTexture = TextureManager.Instance.UnitStatusAlive;
            }
            if (this.Unit.IsStatusDamaged)
            {
				this.UnitTexture = TextureManager.Instance.UnitStatusDamaged;
            }
            if (this.Unit.IsStatusDeading)
            {
				this.UnitTexture = TextureManager.Instance.UnitStatusDeading;
            }

			// Draws the unit's status background.
			game.SpriteBatch.Draw(this.UnitTexture, this.Position, Color.White);

			// Get the unit's life points text.
            string lifeInformation = String.Format("{0}/{1}", this.Unit.Life, this.Unit.FullLife);

            // Get the unit's mana points text.
            string manaInformation = String.Format("{0}/{1}", this.Unit.Mana, this.Unit.FullMana);

			// Measure the texts size.
			Vector2 nameSize = this.NameSpriteFont.MeasureString(this.Unit.Name);
			Vector2 lifeSize = this.LifeSpriteFont.MeasureString(lifeInformation);
            Vector2 manaSize = this.ManaSpriteFont.MeasureString(manaInformation);

			// Calculate the position of the texts.
            Vector2 namePosition = new Vector2(this.Position.X + this.UnitTexture.Width / 2 - nameSize.X / 2, this.Position.Y + 5);
            Vector2 lifePosition = new Vector2(this.Position.X + this.UnitTexture.Width - lifeSize.X - 5, this.Position.Y + 30);
            Vector2 manaPosition = new Vector2(this.Position.X + this.UnitTexture.Width - manaSize.X - 5, this.Position.Y + 53);

			// Draws the name of the player.
			game.SpriteBatch.DrawString(this.NameSpriteFont, this.Unit.Name, namePosition, Color.White);

			// Draws the life points information.
            game.SpriteBatch.DrawString(this.LifeSpriteFont, lifeInformation, lifePosition, Color.White);

            // Draws the mana points information.
            game.SpriteBatch.DrawString(this.ManaSpriteFont, manaInformation, manaPosition, Color.White);

            game.SpriteBatch.End();
            
            base.Draw(gameTime);
		}

		#endregion
    }
}