using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IRTaktiks.Components.Manager;
using IRTaktiks.Input;
using IRTaktiks.Input.EventArgs;

namespace IRTaktiks.Components.Config
{
    /// <summary>
    /// Representation of the configuration of player.
    /// </summary>
    public class PlayerConfig : Configurable
    {
        #region Properties

        /// <summary>
        /// Indicate that the item is configurated.
        /// </summary>
        public override bool Configurated
        {
            get { return this.DisplayText.Length > 0; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="game">The instance of game that is running.</param>
        /// <param name="playerIndex">The index of the player owner of the keyboard.</param>
        public PlayerConfig(Game game, PlayerIndex playerIndex)
            : base(game, playerIndex)
        {
            switch (playerIndex)
            {
                case PlayerIndex.One:
                    this.PositionField = new Vector2(0, 0);
                    break;

                case PlayerIndex.Two:
                    this.PositionField = new Vector2(640, 0);
                    break;
            }

            InputManager.Instance.CursorDown += new EventHandler<CursorDownArgs>(CursorDown);
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
            switch (this.PlayerIndex)
            {
                case PlayerIndex.One:
                    
                    if (this.Configurated)
                    {
                        this.TextureField = TextureManager.Instance.Sprites.Config.PlayerOneGreen;
                    }
                    else
                    {
                        this.TextureField = TextureManager.Instance.Sprites.Config.PlayerOneRed;
                    }

                    break;

                case PlayerIndex.Two:

                    if (this.Configurated)
                    {
                        this.TextureField = TextureManager.Instance.Sprites.Config.PlayerTwoGreen;
                    }
                    else
                    {
                        this.TextureField = TextureManager.Instance.Sprites.Config.PlayerTwoRed;
                    }
                    
                    break;
            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// Called when the DrawableGameComponent needs to be drawn. Override this method
        /// with component-specific drawing code.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            IRTGame game = this.Game as IRTGame;

            // Draw the background
            game.SpriteManager.Draw(this.Texture, this.Position, Color.White, 25);

            // Draw the display text
            Vector2 textSize = FontManager.Instance.Chilopod20.MeasureString(this.DisplayText.ToString());
            Vector2 textPosition = new Vector2(this.Position.X + ((44 + 596) / 2) - (textSize.X / 2), this.Position.Y + ((71 + 114) / 2) - (textSize.Y / 2));
            game.SpriteManager.DrawString(FontManager.Instance.Chilopod20, this.DisplayText.ToString(), textPosition, Color.White, 30);

            // Draw the selected indicator.
            if (this.Selected)
            {
                game.SpriteManager.Draw(TextureManager.Instance.Sprites.Config.PlayerSelected, new Vector2(this.Position.X, this.Position.Y), Color.White, 30);
            }

            base.Draw(gameTime);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Unregister all events of the item.
        /// </summary
        public override void Unregister()
        {
            InputManager.Instance.CursorDown -= CursorDown;
        }

        #endregion

        #region Input Handling

        /// <summary>
        /// Input Handling for Cursor Down event.
        /// </summary>
        private void CursorDown(object sender, CursorDownArgs e)
        {
            if ((e.Position.X > (this.Position.X + 44) && e.Position.X < (this.Position.X + 596)) &&
                (e.Position.Y > (this.Position.Y + 44) && e.Position.Y < (this.Position.Y + 114)))
            {
                this.Touch();
            }
        }

        #endregion
    }
}