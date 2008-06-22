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
    /// Representation of the config keyboard.
    /// </summary>
    public class Keyboard : Configurable
    {
        #region Event

        /// <summary>
        /// The delegate of methods that will handle the typed event.
        /// </summary>
        /// <param name="letter">The letter pressed on the keyboard.</param>
        public delegate void TypedEventHandler(string letter);

        /// <summary>
        /// Event fired when a letter on the keyboard is typed.
        /// </summary>
        public event TypedEventHandler Typed;

        #endregion

        #region Properties

        /// <summary>
        /// Indicate that the item is configurated.
        /// </summary>
        public override bool Configurated
        {
            get { return true; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="game">The instance of game that is running.</param>
        /// <param name="playerIndex">The index of the player owner of the keyboard.</param>
        public Keyboard(Game game, PlayerIndex playerIndex)
            : base(game, playerIndex)
        {
            switch (playerIndex)
            {
                case PlayerIndex.One:
                    this.PositionField = new Vector2(0, 0);
                    this.TextureField = TextureManager.Instance.Sprites.Config.KeyboardPlayerOne;
                    break;

                case PlayerIndex.Two:
                    this.PositionField = new Vector2(640, 0);
                    this.TextureField = TextureManager.Instance.Sprites.Config.KeyboardPlayerTwo;
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

            game.SpriteManager.Draw(this.Texture, this.Position, Color.White, 20);
            
            base.Draw(gameTime);
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// Deactivate the item.
        /// </summary>
        protected override void Unregister()
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
            // 1st Line
            if (e.Position.Y > (this.Position.Y + 783) && e.Position.Y < (this.Position.Y + 822))
            {
                if (e.Position.X > (this.Position.X + 27) && e.Position.X < (this.Position.X + 66))
                    this.Typed("1");

                if (e.Position.X > (this.Position.X + 71) && e.Position.X < (this.Position.X + 110))
                    this.Typed("2");

                if (e.Position.X > (this.Position.X + 115) && e.Position.X < (this.Position.X + 154))
                    this.Typed("3");

                if (e.Position.X > (this.Position.X + 159) && e.Position.X < (this.Position.X + 198))
                    this.Typed("4");

                if (e.Position.X > (this.Position.X + 203) && e.Position.X < (this.Position.X + 242))
                    this.Typed("5");

                if (e.Position.X > (this.Position.X + 247) && e.Position.X < (this.Position.X + 283))
                    this.Typed("6");

                if (e.Position.X > (this.Position.X + 291) && e.Position.X < (this.Position.X + 330))
                    this.Typed("7");

                if (e.Position.X > (this.Position.X + 335) && e.Position.X < (this.Position.X + 374))
                    this.Typed("8");

                if (e.Position.X > (this.Position.X + 379) && e.Position.X < (this.Position.X + 418))
                    this.Typed("9");

                if (e.Position.X > (this.Position.X + 423) && e.Position.X < (this.Position.X + 462))
                    this.Typed("0");

                if (e.Position.X > (this.Position.X + 467) && e.Position.X < (this.Position.X + 594))
                    this.Typed(null);
            }

            // 2nd Line
            if (e.Position.Y > (this.Position.Y + 827) && e.Position.Y < (this.Position.Y + 866))
            {
                if (e.Position.X > (this.Position.X + 35) && e.Position.X < (this.Position.X + 74))
                    this.Typed("Q");

                if (e.Position.X > (this.Position.X + 79) && e.Position.X < (this.Position.X + 118))
                    this.Typed("W");

                if (e.Position.X > (this.Position.X + 123) && e.Position.X < (this.Position.X + 162))
                    this.Typed("E");

                if (e.Position.X > (this.Position.X + 167) && e.Position.X < (this.Position.X + 206))
                    this.Typed("R");

                if (e.Position.X > (this.Position.X + 211) && e.Position.X < (this.Position.X + 250))
                    this.Typed("T");

                if (e.Position.X > (this.Position.X + 255) && e.Position.X < (this.Position.X + 294))
                    this.Typed("Y");

                if (e.Position.X > (this.Position.X + 299) && e.Position.X < (this.Position.X + 338))
                    this.Typed("U");

                if (e.Position.X > (this.Position.X + 343) && e.Position.X < (this.Position.X + 382))
                    this.Typed("I");

                if (e.Position.X > (this.Position.X + 387) && e.Position.X < (this.Position.X + 426))
                    this.Typed("O");

                if (e.Position.X > (this.Position.X + 431) && e.Position.X < (this.Position.X + 470))
                    this.Typed("P");

                if (e.Position.X > (this.Position.X + 475) && e.Position.X < (this.Position.X + 514))
                    this.Typed("(");

                if (e.Position.X > (this.Position.X + 519) && e.Position.X < (this.Position.X + 558))
                    this.Typed("[");

                if (e.Position.X > (this.Position.X + 563) && e.Position.X < (this.Position.X + 602))
                    this.Typed("{");
            }

            // 3rd Line
            if (e.Position.Y > (this.Position.Y + 871) && e.Position.Y < (this.Position.Y + 910))
            {
                if (e.Position.X > (this.Position.X + 46) && e.Position.X < (this.Position.X + 85))
                    this.Typed("A");

                if (e.Position.X > (this.Position.X + 90) && e.Position.X < (this.Position.X + 129))
                    this.Typed("S");

                if (e.Position.X > (this.Position.X + 134) && e.Position.X < (this.Position.X + 173))
                    this.Typed("D");

                if (e.Position.X > (this.Position.X + 178) && e.Position.X < (this.Position.X + 217))
                    this.Typed("F");

                if (e.Position.X > (this.Position.X + 222) && e.Position.X < (this.Position.X + 261))
                    this.Typed("G");

                if (e.Position.X > (this.Position.X + 266) && e.Position.X < (this.Position.X + 305))
                    this.Typed("H");

                if (e.Position.X > (this.Position.X + 310) && e.Position.X < (this.Position.X + 349))
                    this.Typed("J");

                if (e.Position.X > (this.Position.X + 354) && e.Position.X < (this.Position.X + 393))
                    this.Typed("K");

                if (e.Position.X > (this.Position.X + 398) && e.Position.X < (this.Position.X + 437))
                    this.Typed("L");

                if (e.Position.X > (this.Position.X + 442) && e.Position.X < (this.Position.X + 481))
                    this.Typed("C");

                if (e.Position.X > (this.Position.X + 486) && e.Position.X < (this.Position.X + 525))
                    this.Typed(")");

                if (e.Position.X > (this.Position.X + 530) && e.Position.X < (this.Position.X + 569))
                    this.Typed("]");

                if (e.Position.X > (this.Position.X + 574) && e.Position.X < (this.Position.X + 613))
                    this.Typed("}");
            }

            // 4th Line
            if (e.Position.Y > (this.Position.Y + 915) && e.Position.Y < (this.Position.Y + 954))
            {
                if (e.Position.X > (this.Position.X + 27) && e.Position.X < (this.Position.X + 66))
                    this.Typed("\\");

                if (e.Position.X > (this.Position.X + 71) && e.Position.X < (this.Position.X + 110))
                    this.Typed("Z");

                if (e.Position.X > (this.Position.X + 115) && e.Position.X < (this.Position.X + 154))
                    this.Typed("X");

                if (e.Position.X > (this.Position.X + 159) && e.Position.X < (this.Position.X + 198))
                    this.Typed("C");

                if (e.Position.X > (this.Position.X + 203) && e.Position.X < (this.Position.X + 242))
                    this.Typed("V");

                if (e.Position.X > (this.Position.X + 247) && e.Position.X < (this.Position.X + 283))
                    this.Typed("B");

                if (e.Position.X > (this.Position.X + 291) && e.Position.X < (this.Position.X + 330))
                    this.Typed("N");

                if (e.Position.X > (this.Position.X + 335) && e.Position.X < (this.Position.X + 374))
                    this.Typed("M");

                if (e.Position.X > (this.Position.X + 379) && e.Position.X < (this.Position.X + 418))
                    this.Typed("<");

                if (e.Position.X > (this.Position.X + 423) && e.Position.X < (this.Position.X + 462))
                    this.Typed(">");

                if (e.Position.X > (this.Position.X + 467) && e.Position.X < (this.Position.X + 506))
                    this.Typed(",");

                if (e.Position.X > (this.Position.X + 511) && e.Position.X < (this.Position.X + 550))
                    this.Typed(".");

                if (e.Position.X > (this.Position.X + 555) && e.Position.X < (this.Position.X + 594))
                    this.Typed("/");
            }

            // 5th Line
            if (e.Position.Y > (this.Position.Y + 959) && e.Position.Y < (this.Position.Y + 997))
            {
                if (e.Position.X > (this.Position.X + 115) && e.Position.X < (this.Position.X + 506))
                    this.Typed(" ");
            }
        }

        #endregion
    }
}