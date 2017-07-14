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
using IRTaktiks.Input;
using IRTaktiks.Input.EventArgs;

namespace IRTaktiks.Components.Menu
{
    /// <summary>
    /// Representation of the menu of the player.
    /// </summary>
    public class StatusMenu : DrawableGameComponent
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
        /// The texture that will be used to draw the unit's attributes.
        /// </summary>
        private Texture2D AttributesTexture;

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

        /// <summary>
        /// The sprite font that will be used to write the unit's attributes.
        /// </summary>
        private SpriteFont AttributesSpriteFont;
        
        /// <summary>
        /// The position of the attributes.
        /// </summary>
        private Vector2 AttributesPosition;
        
        /// <summary>
        /// Indicates that the attributes are visible.
        /// </summary>
        private bool AttributesVisible;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="game">The instance of game that is running.</param>
        /// <param name="unit">The unit who is the owner of the menu.</param>
        public StatusMenu(Game game, Unit unit)
            : base(game)
        {
            // Set the properties.
            this.UnitField = unit;
            this.AttributesVisible = false;

            // Set the spritefonts that will be used.
            this.NameSpriteFont = FontManager.Instance.Chilopod16;
            this.LifeSpriteFont = FontManager.Instance.Chilopod14;
            this.ManaSpriteFont = FontManager.Instance.Chilopod14;
            this.AttributesSpriteFont = FontManager.Instance.Chilopod14;
            
            // Textures.
            this.UnitTexture = TextureManager.Instance.Sprites.Unit.FullStatusAlive;
            this.AttributesTexture = TextureManager.Instance.Sprites.Unit.Attributes;

            // Player one informations.
            if (this.Unit.Player.PlayerIndex == PlayerIndex.One)
            {
                this.PositionField = new Vector2(0, 192);
                this.AttributesPosition = new Vector2(this.Position.X + this.UnitTexture.Width, this.Position.Y);
            }

            // Player two informations.
            if (this.Unit.Player.PlayerIndex == PlayerIndex.Two)
            {
                this.PositionField = new Vector2(IRTSettings.Default.Width - TextureManager.Instance.Sprites.Menu.Background.Width, 192);
                this.AttributesPosition = new Vector2(this.Position.X - this.AttributesTexture.Width, this.Position.Y);
            }

            InputManager.Instance.CursorDown += new EventHandler<IRTaktiks.Input.EventArgs.CursorDownArgs>(Instance_CursorDown);
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

            // Set texture according the unit status.
            if (this.Unit.IsStatusAlive)
            {
                this.UnitTexture = TextureManager.Instance.Sprites.Unit.FullStatusAlive;
            }
            if (this.Unit.IsStatusDamaged)
            {
                this.UnitTexture = TextureManager.Instance.Sprites.Unit.FullStatusDamaged;
            }
            if (this.Unit.IsStatusDeading)
            {
                this.UnitTexture = TextureManager.Instance.Sprites.Unit.FullStatusDeading;
            }

            // Draws the background of menu.
            game.SpriteManager.Draw(TextureManager.Instance.Sprites.Menu.Background, new Vector2(this.Position.X, 158), new Rectangle(0, 158, this.UnitTexture.Width, this.UnitTexture.Height + ((int)this.Position.Y) - 158), Color.White, 40);

            // Draws the unit's status background.
            game.SpriteManager.Draw(this.UnitTexture, this.Position, Color.White, 50);

            // Get the unit's life points text.
            string lifeInformation = String.Format("{0}/{1}", this.Unit.Life, this.Unit.Attributes.MaximumLife);

            // Get the unit's mana points text.
            string manaInformation = String.Format("{0}/{1}", this.Unit.Mana, this.Unit.Attributes.MaximumMana);

            // Measure the texts size.
            Vector2 nameSize = this.NameSpriteFont.MeasureString(this.Unit.Name);
            Vector2 lifeSize = this.LifeSpriteFont.MeasureString(lifeInformation);
            Vector2 manaSize = this.ManaSpriteFont.MeasureString(manaInformation);

            // Calculate the position of the texts.
            Vector2 namePosition = new Vector2(this.Position.X + this.UnitTexture.Width / 2 - nameSize.X / 2, this.Position.Y + 5);
            Vector2 lifePosition = new Vector2(this.Position.X + this.UnitTexture.Width - lifeSize.X - 5, this.Position.Y + 28);
            Vector2 manaPosition = new Vector2(this.Position.X + this.UnitTexture.Width - manaSize.X - 5, this.Position.Y + 51);

            // Draws the name of the unit.
            game.SpriteManager.DrawString(this.NameSpriteFont, this.Unit.Name, namePosition, Color.White, 52);

            // Draws the life points information.
            game.SpriteManager.DrawString(this.LifeSpriteFont, lifeInformation, lifePosition, Color.White, 52);

            // Draws the mana points information.
            game.SpriteManager.DrawString(this.ManaSpriteFont, manaInformation, manaPosition, Color.White, 52);

            // Measure the maximum width and height for the bars.
            int barMaxWidth = 136;
            int barMaxHeight = 3;

            // Calculates the position of the life, mana and time bars
            Vector2 lifeBarPosition = new Vector2(this.Position.X + 7, this.Position.Y + 46);
            Vector2 manaBarPosition = new Vector2(this.Position.X + 7, this.Position.Y + 69);
            Vector2 timeBarPosition = new Vector2(this.Position.X + 7, this.Position.Y + 91);

            // Calculates the area of the life, mana and time bars, based in unit's values.
            Rectangle lifeBar = new Rectangle((int)lifeBarPosition.X, (int)lifeBarPosition.Y, (int)((barMaxWidth * this.Unit.Life) / this.Unit.Attributes.MaximumLife), barMaxHeight);
            Rectangle manaBar = new Rectangle((int)manaBarPosition.X, (int)manaBarPosition.Y, (int)((barMaxWidth * this.Unit.Mana) / this.Unit.Attributes.MaximumMana), barMaxHeight);
            Rectangle timeBar = new Rectangle((int)timeBarPosition.X, (int)timeBarPosition.Y, (int)(barMaxWidth * this.Unit.Time), barMaxHeight);

            // Draws the life, mana and time bars.
            game.SpriteManager.Draw(TextureManager.Instance.Sprites.Unit.LifeBar, lifeBar, Color.White, 51);
            game.SpriteManager.Draw(TextureManager.Instance.Sprites.Unit.ManaBar, manaBar, Color.White, 51);
            game.SpriteManager.Draw(TextureManager.Instance.Sprites.Unit.TimeBar, timeBar, Color.White, 51);

            // If the attributes are visible.
            if (this.AttributesVisible)
            {
                // Draws the unit's attributes background.
                game.SpriteManager.Draw(this.AttributesTexture, this.AttributesPosition, Color.White, 50);

                // Calculate the size of the texts
                Vector2 strengthSize = this.AttributesSpriteFont.MeasureString("STR");
                Vector2 agilitySize = this.AttributesSpriteFont.MeasureString("AGI");
                Vector2 vitalitySize = this.AttributesSpriteFont.MeasureString("VIT");
                Vector2 inteligenceSize = this.AttributesSpriteFont.MeasureString("INT");
                Vector2 dexteritySize = this.AttributesSpriteFont.MeasureString("DEX");

                Vector2 attackSize = this.AttributesSpriteFont.MeasureString("ATK");
                Vector2 defenseSize = this.AttributesSpriteFont.MeasureString("DEF");
                Vector2 magicAttackSize = this.AttributesSpriteFont.MeasureString("MATK");
                Vector2 magicDefenseSize = this.AttributesSpriteFont.MeasureString("MDEF");
                Vector2 fleeSize = this.AttributesSpriteFont.MeasureString("FLEE");
                Vector2 hitSize = this.AttributesSpriteFont.MeasureString("HIT");
                Vector2 rangeSize = this.AttributesSpriteFont.MeasureString("RANGE");
                Vector2 delaySize = this.AttributesSpriteFont.MeasureString("DELAY");

                // Calculate the position of the texts
                int offsetLabel = 7;
                int offsetValue = offsetLabel + 7;
                int distance = 17; 
                int start1st = 10; 
                int start2nd = 105;

                Vector2 strengthPosition = new Vector2(this.AttributesPosition.X + this.AttributesTexture.Width / 2 - strengthSize.X + offsetLabel, this.AttributesPosition.Y + start1st + distance * 0);
                Vector2 agilityPosition = new Vector2(this.AttributesPosition.X + this.AttributesTexture.Width / 2 - agilitySize.X + offsetLabel, this.AttributesPosition.Y + start1st + distance * 1);
                Vector2 vitalityPosition = new Vector2(this.AttributesPosition.X + this.AttributesTexture.Width / 2 - vitalitySize.X + offsetLabel, this.AttributesPosition.Y + start1st + distance * 2);
                Vector2 inteligencePosition = new Vector2(this.AttributesPosition.X + this.AttributesTexture.Width / 2 - inteligenceSize.X + offsetLabel, this.AttributesPosition.Y + start1st + distance * 3);
                Vector2 dexterityPosition = new Vector2(this.AttributesPosition.X + this.AttributesTexture.Width / 2 - dexteritySize.X + offsetLabel, this.AttributesPosition.Y + start1st + distance * 4);

                Vector2 attackPosition = new Vector2(this.AttributesPosition.X + this.AttributesTexture.Width / 2 - attackSize.X + offsetLabel, this.AttributesPosition.Y + start2nd + distance * 0);
                Vector2 defensePosition = new Vector2(this.AttributesPosition.X + this.AttributesTexture.Width / 2 - defenseSize.X + offsetLabel, this.AttributesPosition.Y + start2nd + distance * 1);
                Vector2 magicAttackPosition = new Vector2(this.AttributesPosition.X + this.AttributesTexture.Width / 2 - magicAttackSize.X + offsetLabel, this.AttributesPosition.Y + start2nd + distance * 2);
                Vector2 magicDefensePosition = new Vector2(this.AttributesPosition.X + this.AttributesTexture.Width / 2 - magicDefenseSize.X + offsetLabel, this.AttributesPosition.Y + start2nd + distance * 3);
                Vector2 fleePosition = new Vector2(this.AttributesPosition.X + this.AttributesTexture.Width / 2 - fleeSize.X + offsetLabel, this.AttributesPosition.Y + start2nd + distance * 4);
                Vector2 hitPosition = new Vector2(this.AttributesPosition.X + this.AttributesTexture.Width / 2 - hitSize.X + offsetLabel, this.AttributesPosition.Y + start2nd + distance * 5);
                Vector2 rangePosition = new Vector2(this.AttributesPosition.X + this.AttributesTexture.Width / 2 - rangeSize.X + offsetLabel, this.AttributesPosition.Y + start2nd + distance * 6);
                Vector2 delayPosition = new Vector2(this.AttributesPosition.X + this.AttributesTexture.Width / 2 - delaySize.X + offsetLabel, this.AttributesPosition.Y + start2nd + distance * 7);

                // Draws the texts.
                game.SpriteManager.DrawString(this.AttributesSpriteFont, "STR", strengthPosition, Color.White, 52);
                game.SpriteManager.DrawString(this.AttributesSpriteFont, "AGI", agilityPosition, Color.White, 52);
                game.SpriteManager.DrawString(this.AttributesSpriteFont, "VIT", vitalityPosition, Color.White, 52);
                game.SpriteManager.DrawString(this.AttributesSpriteFont, "INT", inteligencePosition, Color.White, 52);
                game.SpriteManager.DrawString(this.AttributesSpriteFont, "DEX", dexterityPosition, Color.White, 52);

                game.SpriteManager.DrawString(this.AttributesSpriteFont, "ATK", attackPosition, Color.White, 52);
                game.SpriteManager.DrawString(this.AttributesSpriteFont, "DEF", defensePosition, Color.White, 52);
                game.SpriteManager.DrawString(this.AttributesSpriteFont, "MATK", magicAttackPosition, Color.White, 52);
                game.SpriteManager.DrawString(this.AttributesSpriteFont, "MDEF", magicDefensePosition, Color.White, 52);
                game.SpriteManager.DrawString(this.AttributesSpriteFont, "FLEE", fleePosition, Color.White, 52);
                game.SpriteManager.DrawString(this.AttributesSpriteFont, "HIT", hitPosition, Color.White, 52);
                game.SpriteManager.DrawString(this.AttributesSpriteFont, "RANGE", rangePosition, Color.White, 52);
                game.SpriteManager.DrawString(this.AttributesSpriteFont, "DELAY", delayPosition, Color.White, 52);

                // Draw the values.
                game.SpriteManager.DrawString(this.AttributesSpriteFont, this.Unit.Attributes.Strength.ToString(), new Vector2(this.AttributesPosition.X + this.AttributesTexture.Width / 2 + offsetValue, strengthPosition.Y), Color.White, 52);
                game.SpriteManager.DrawString(this.AttributesSpriteFont, this.Unit.Attributes.Agility.ToString(), new Vector2(this.AttributesPosition.X + this.AttributesTexture.Width / 2 + offsetValue, agilityPosition.Y), Color.White, 52);
                game.SpriteManager.DrawString(this.AttributesSpriteFont, this.Unit.Attributes.Vitality.ToString(), new Vector2(this.AttributesPosition.X + this.AttributesTexture.Width / 2 + offsetValue, vitalityPosition.Y), Color.White, 52);
                game.SpriteManager.DrawString(this.AttributesSpriteFont, this.Unit.Attributes.Inteligence.ToString(), new Vector2(this.AttributesPosition.X + this.AttributesTexture.Width / 2 + offsetValue, inteligencePosition.Y), Color.White, 52);
                game.SpriteManager.DrawString(this.AttributesSpriteFont, this.Unit.Attributes.Dexterity.ToString(), new Vector2(this.AttributesPosition.X + this.AttributesTexture.Width / 2 + offsetValue, dexterityPosition.Y), Color.White, 52);

                game.SpriteManager.DrawString(this.AttributesSpriteFont, this.Unit.Attributes.Attack.ToString(), new Vector2(this.AttributesPosition.X + this.AttributesTexture.Width / 2 + offsetValue, attackPosition.Y), Color.White, 52);
                game.SpriteManager.DrawString(this.AttributesSpriteFont, this.Unit.Attributes.Defense.ToString(), new Vector2(this.AttributesPosition.X + this.AttributesTexture.Width / 2 + offsetValue, defensePosition.Y), Color.White, 52);
                game.SpriteManager.DrawString(this.AttributesSpriteFont, this.Unit.Attributes.MagicAttack.ToString(), new Vector2(this.AttributesPosition.X + this.AttributesTexture.Width / 2 + offsetValue, magicAttackPosition.Y), Color.White, 52);
                game.SpriteManager.DrawString(this.AttributesSpriteFont, this.Unit.Attributes.MagicDefense.ToString(), new Vector2(this.AttributesPosition.X + this.AttributesTexture.Width / 2 + offsetValue, magicDefensePosition.Y), Color.White, 52);
                game.SpriteManager.DrawString(this.AttributesSpriteFont, this.Unit.Attributes.Flee.ToString(), new Vector2(this.AttributesPosition.X + this.AttributesTexture.Width / 2 + offsetValue, fleePosition.Y), Color.White, 52);
                game.SpriteManager.DrawString(this.AttributesSpriteFont, this.Unit.Attributes.Hit.ToString(), new Vector2(this.AttributesPosition.X + this.AttributesTexture.Width / 2 + offsetValue, hitPosition.Y), Color.White, 52);
                game.SpriteManager.DrawString(this.AttributesSpriteFont, this.Unit.Attributes.Range.ToString(), new Vector2(this.AttributesPosition.X + this.AttributesTexture.Width / 2 + offsetValue, rangePosition.Y), Color.White, 52);
                game.SpriteManager.DrawString(this.AttributesSpriteFont, ((int)(300 - this.Unit.Attributes.Delay * 100000)).ToString(), new Vector2(this.AttributesPosition.X + this.AttributesTexture.Width / 2 + offsetValue, delayPosition.Y), Color.White, 52);

            }

            base.Draw(gameTime);
        }

        #endregion

        #region Input Handling

        /// <summary>
        /// Handles the CursorDown event.
        /// </summary>
        /// <param name="sender">Always null.</param>
        /// <param name="e">Data of event.</param>
        private void Instance_CursorDown(object sender, CursorDownArgs e)
        {
            // Check if the touch was inside the x area of the menu.
            if (e.Position.X < (this.Position.X + this.UnitTexture.Width) && e.Position.X > this.Position.X)
            {
                // Check if the touch was inside the y area of the menu.
                if (e.Position.Y < (this.Position.Y + this.UnitTexture.Height) && e.Position.Y > this.Position.Y)
                {
                    this.AttributesVisible = !this.AttributesVisible;
                }
            }
        }

        #endregion
    }
}