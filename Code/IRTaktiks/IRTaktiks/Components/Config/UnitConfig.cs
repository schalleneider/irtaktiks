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
using IRTaktiks.Components.Logic;

namespace IRTaktiks.Components.Config
{
    /// <summary>
    /// Representation of the configuration of unit.
    /// </summary>
    public class UnitConfig : Configurable
    {
        #region Attributes

        /// <summary>
        /// The value of the total attribute points.
        /// </summary>
        private int AttributePointsFields;

        /// <summary>
        /// The value of the total attribute points.
        /// </summary>
        public int AttributePoints
        {
            get { return AttributePointsFields; }
            set { AttributePointsFields = value; }
        }
                
        /// <summary>
        /// The value of the configured strength.
        /// </summary>
        private int StrengthField;

        /// <summary>
        /// The value of the configured strength.
        /// </summary>
        public int Strength
        {
            get { return StrengthField; }
        }

        /// <summary>
        /// The value of the configured agility.
        /// </summary>
        private int AgilityField;

        /// <summary>
        /// The value of the configured agility.
        /// </summary>
        public int Agility
        {
            get { return AgilityField; }
        }
        
        /// <summary>
        /// The value of the configured vitality.
        /// </summary>
        private int VitalityField;
        
        /// <summary>
        /// The value of the configured vitality.
        /// </summary>
        public int Vitality
        {
            get { return VitalityField; }
        }
        
        /// <summary>
        /// The value of the configured inteligence.
        /// </summary>
        private int InteligenceField;
        
        /// <summary>
        /// The value of the configured inteligence.
        /// </summary>
        public int Inteligence
        {
            get { return InteligenceField; }
        }
        
        /// <summary>
        /// The value of the configured dexterity.
        /// </summary>
        private int DexterityField;
        
        /// <summary>
        /// The value of the configured dexterity.
        /// </summary>
        public int Dexterity
        {
            get { return DexterityField; }
        }

        /// <summary>
        /// The job of the unit.
        /// </summary>
        private Job JobField;

        /// <summary>
        /// The job of the unit.
        /// </summary>
        public Job Job
        {
            get { return JobField; }
        }

        /// <summary>
        /// The texture of the unit.
        /// </summary>
        private Texture2D UnitTextureField;

        /// <summary>
        /// The texture of the unit.
        /// </summary>
        public Texture2D UnitTexture
        {
            get { return UnitTextureField; }
            set { UnitTextureField = value; }
        }
                        
        #endregion

        #region Properties
        
        /// <summary>
        /// Indicate that the item is configurated.
        /// </summary>
        public override bool Configurated
        {
            get { return this.DisplayText.Length > 0 && this.AttributePoints == 0; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="game">The instance of game that is running.</param>
        /// <param name="playerIndex">The index of the player owner of the keyboard.</param>
        /// <param name="unitIndex">The index of the unit.</param>
        public UnitConfig(Game game, PlayerIndex playerIndex, int unitIndex)
            : base(game, playerIndex)
        {
            int x = 0;
            int y = 0;

            switch (playerIndex)
            {
                case PlayerIndex.One:
                    x = 54 + (unitIndex / 4) * (TextureManager.Instance.Sprites.Config.UnitGreen.Width - 2);
                    y = 170 + (unitIndex % 4) * (TextureManager.Instance.Sprites.Config.UnitGreen.Height - 5);
                    break;

                case PlayerIndex.Two:
                    x = 640 + 54 + (unitIndex / 4) * (TextureManager.Instance.Sprites.Config.UnitGreen.Width - 2);
                    y = 170 + (unitIndex % 4) * (TextureManager.Instance.Sprites.Config.UnitGreen.Height - 5);
                    break;
            }
            
            this.PositionField = new Vector2(x, y);

            this.StrengthField = 0;
            this.AgilityField = 0;
            this.VitalityField = 0;
            this.InteligenceField = 0;
            this.DexterityField = 0;

            this.AttributePointsFields = 150;
            this.JobField = Job.Knight;

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
            if (this.Configurated)
            {
                this.TextureField = TextureManager.Instance.Sprites.Config.UnitGreen;
            }
            else
            {
                this.TextureField = TextureManager.Instance.Sprites.Config.UnitRed;
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
            Vector2 textPosition = new Vector2(this.Position.X + (TextureManager.Instance.Sprites.Config.UnitGreen.Width / 2) - (textSize.X / 2), this.Position.Y + (TextureManager.Instance.Sprites.Config.UnitGreen.Height / 2) - (textSize.Y / 2));
            game.SpriteManager.DrawString(FontManager.Instance.Chilopod20, this.DisplayText.ToString(), textPosition, Color.White, 30);

            // Draw the indicator.
            if (this.Selected)
            {
                game.SpriteManager.Draw(TextureManager.Instance.Sprites.Config.UnitSelected, new Vector2(this.Position.X, this.Position.Y), Color.White, 30);
            }

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

            if ((e.Position.X > this.Position.X && e.Position.X < (this.Position.X + TextureManager.Instance.Sprites.Config.UnitGreen.Width)) &&
                (e.Position.Y > this.Position.Y && e.Position.Y < (this.Position.Y + TextureManager.Instance.Sprites.Config.UnitGreen.Height)))
            {
                this.Touch();
            }
        }

        #endregion
    }
}