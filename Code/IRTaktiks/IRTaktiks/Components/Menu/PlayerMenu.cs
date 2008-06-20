using System;
using System.Collections.Generic;
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
	/// Representation of the menu of the player.
	/// </summary>
	public class PlayerMenu : DrawableGameComponent
    {
        #region Properties

        /// <summary>
        /// The player whose information will be displayed.
        /// </summary>
        private Player PlayerField;

        /// <summary>
        /// The player whose information will be displayed.
        /// </summary>
        public Player Player
        {
            get { return PlayerField; }
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
		/// The texture that will be used to draw the player information.
		/// </summary>
		private Texture2D PlayerTexture;

		/// <summary>
		/// The spritefont that will be used to write the player's name.
		/// </summary>
		private SpriteFont NameSpriteFont;

		/// <summary>
		/// The spritefont that will be used to write the player's unit informations.
		/// </summary>
		private SpriteFont UnitSpriteFont;
		        
        #endregion

        #region Constructor

        /// <summary>
		/// Constructor of class.
		/// </summary>
		/// <param name="game">The instance of game that is running.</param>
        /// <param name="player">The player who is the owner of the menu.</param>
		public PlayerMenu(Game game, Player player)
			: base(game)
		{
			// Set the player whose information will be displayed.
            this.PlayerField = player;

			// Set the spritefonts that will be used.
			this.NameSpriteFont = FontManager.Instance.Chilopod20;
			this.UnitSpriteFont = FontManager.Instance.Chilopod20;

			// Player one informations.
			if (this.Player.PlayerIndex == PlayerIndex.One)
			{
				this.PositionField = new Vector2(0, 33);

				this.PlayerTexture = TextureManager.Instance.Sprites.Player.PlayerOneBackground;
			}

			// Player two informations.
			if (this.Player.PlayerIndex == PlayerIndex.Two)
			{
				this.PositionField = new Vector2(IRTGame.Width - TextureManager.Instance.Sprites.Menu.Background.Width, 33);

                this.PlayerTexture = TextureManager.Instance.Sprites.Player.PlayerTwoBackground;
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
			
			// Draws the background of menu.
            game.SpriteManager.Draw(TextureManager.Instance.Sprites.Menu.Background, new Vector2(this.Position.X, 0), new Rectangle(0, 0, this.PlayerTexture.Width, this.PlayerTexture.Height + ((int)this.Position.Y) - 0), Color.White, 40);
			
			// Draws the player status background.
            game.SpriteManager.Draw(this.PlayerTexture, this.Position, Color.White, 50);

			// Get the unit information text.
			string unitInformation = String.Format("0{0}/0{1}", this.Player.Units.FindAll(delegate(Unit unit) { return unit.Life > 0; }).Count, this.Player.Units.Count);

			// Measure the texts size.
			Vector2 nameSize = this.NameSpriteFont.MeasureString(this.Player.Name);
			Vector2 unitSize = this.UnitSpriteFont.MeasureString(unitInformation);
			
			// Calculate the position of the texts.
			Vector2 namePosition = new Vector2(this.Position.X + this.PlayerTexture.Width - nameSize.X - 10, this.Position.Y + 25);
			Vector2 unitPosition = new Vector2(this.Position.X + this.PlayerTexture.Width - unitSize.X - 10, this.Position.Y + 85);

			// Draws the name of the player.
            game.SpriteManager.DrawString(this.NameSpriteFont, this.Player.Name, namePosition, Color.White, 51);

			// Draws the unit information.
            game.SpriteManager.DrawString(this.UnitSpriteFont, unitInformation, unitPosition, Color.White, 51);
            
            base.Draw(gameTime);
		}

		#endregion
    }
}