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
        /// The index of the actual character.
        /// </summary>
        private int CharacterIndexField;
        
        /// <summary>
        /// The index of the actual character.
        /// </summary>
        public int CharacterIndex
        {
            get { return CharacterIndexField; }
        }

        /// <summary>
        /// The texture of the attributes.
        /// </summary>
        private Texture2D AttributeTexture;
                        
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

                    this.AttributeTexture = TextureManager.Instance.Sprites.Config.AttributesPlayerOne;
                    
                    break;

                case PlayerIndex.Two:
                    x = 640 + 54 + (unitIndex / 4) * (TextureManager.Instance.Sprites.Config.UnitGreen.Width - 2);
                    y = 170 + (unitIndex % 4) * (TextureManager.Instance.Sprites.Config.UnitGreen.Height - 5);
                    
                    this.AttributeTexture = TextureManager.Instance.Sprites.Config.AttributesPlayerTwo;

                    break;
            }
            
            this.PositionField = new Vector2(x, y);

            this.StrengthField = 0;
            this.AgilityField = 0;
            this.VitalityField = 0;
            this.InteligenceField = 0;
            this.DexterityField = 0;

            this.CharacterIndexField = 0;

            this.AttributePointsFields = IRTSettings.Default.AttributePointsPerUnit;

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
                Vector2 attributePosition = new Vector2((640 * ((int)this.Position.X / 640)) + 38, 379);
                
                game.SpriteManager.Draw(TextureManager.Instance.Sprites.Config.UnitSelected, new Vector2(this.Position.X, this.Position.Y), Color.White, 30);
                game.SpriteManager.Draw(this.AttributeTexture, attributePosition, Color.White, 30);

                // Draw the character
                Texture2D characterTexture = Character.Instance[this.CharacterIndex].Texture;
                Vector2 characterPosition = new Vector2(attributePosition.X + ((23 + 184) / 2) - (characterTexture.Width / 2), attributePosition.Y + ((49 + 214) / 2) - (characterTexture.Height / 8));
                int index = gameTime.TotalGameTime.Seconds % 4; index = index == 0 ? 0 : index == 1 ? 2 : index == 2 ? 3 : 1;
                game.SpriteManager.Draw(characterTexture, characterPosition, new Rectangle(0, 48 * index, 32, 48), Color.White, 35);

                // Draw the job name
                string jobName = Enum.GetName(typeof(Job), Character.Instance[this.CharacterIndex].Job);
                Vector2 jobSize = FontManager.Instance.Chilopod20.MeasureString(jobName);
                Vector2 jobPosition = new Vector2(attributePosition.X + ((23 + 184) / 2) - (jobSize.X / 2), attributePosition.Y + ((214 + 247) / 2) - (jobSize.Y / 2));
                game.SpriteManager.DrawString(FontManager.Instance.Chilopod20, jobName, jobPosition, Color.White, 35);

                // Draw the points left
                string pointsText = String.Format("{0} points left", this.AttributePoints);
                Vector2 pointsSize = FontManager.Instance.Chilopod20.MeasureString(pointsText);
                Vector2 pointsPosition = new Vector2(attributePosition.X + ((184 + 541) / 2) - (pointsSize.X / 2), attributePosition.Y + ((49 + 82) / 2) - (pointsSize.Y / 2));
                game.SpriteManager.DrawString(FontManager.Instance.Chilopod20, pointsText, pointsPosition, Color.White, 35);

                // Draw the strength
                Vector2 strengthSize = FontManager.Instance.Chilopod20.MeasureString(this.Strength.ToString());
                Vector2 strengthPosition = new Vector2(attributePosition.X + ((411 + 475) / 2) - (strengthSize.X / 2), attributePosition.Y + ((82 + 115) / 2) - (strengthSize.Y / 2));
                game.SpriteManager.DrawString(FontManager.Instance.Chilopod20, this.Strength.ToString(), strengthPosition, Color.White, 35);

                // Draw the agility
                Vector2 agilitySize = FontManager.Instance.Chilopod20.MeasureString(this.Agility.ToString());
                Vector2 agilityPosition = new Vector2(attributePosition.X + ((411 + 475) / 2) - (agilitySize.X / 2), attributePosition.Y + ((115 + 148) / 2) - (agilitySize.Y / 2));
                game.SpriteManager.DrawString(FontManager.Instance.Chilopod20, this.Agility.ToString(), agilityPosition, Color.White, 35);

                // Draw the vitality
                Vector2 vitalitySize = FontManager.Instance.Chilopod20.MeasureString(this.Vitality.ToString());
                Vector2 vitalityPosition = new Vector2(attributePosition.X + ((411 + 475) / 2) - (vitalitySize.X / 2), attributePosition.Y + ((148 + 181) / 2) - (vitalitySize.Y / 2));
                game.SpriteManager.DrawString(FontManager.Instance.Chilopod20, this.Vitality.ToString(), vitalityPosition, Color.White, 35);

                // Draw the inteligence
                Vector2 inteligenceSize = FontManager.Instance.Chilopod20.MeasureString(this.Inteligence.ToString());
                Vector2 inteligencePosition = new Vector2(attributePosition.X + ((411 + 475) / 2) - (inteligenceSize.X / 2), attributePosition.Y + ((181 + 214) / 2) - (inteligenceSize.Y / 2));
                game.SpriteManager.DrawString(FontManager.Instance.Chilopod20, this.Inteligence.ToString(), inteligencePosition, Color.White, 35);

                // Draw the dexterity
                Vector2 dexteritySize = FontManager.Instance.Chilopod20.MeasureString(this.Dexterity.ToString());
                Vector2 dexterityPosition = new Vector2(attributePosition.X + ((411 + 475) / 2) - (dexteritySize.X / 2), attributePosition.Y + ((214 + 247) / 2) - (dexteritySize.Y / 2));
                game.SpriteManager.DrawString(FontManager.Instance.Chilopod20, this.Dexterity.ToString(), dexterityPosition, Color.White, 35);
            }

            base.Draw(gameTime);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Unregister all touch events in the keyboard.
        /// </summary>
        public override void Unregister()
        {
            InputManager.Instance.CursorDown -= CursorDown;
        }

        /// <summary>
        /// Change the character to the next.
        /// </summary>
        private void NextCharacter()
        {
            this.CharacterIndexField++;

            if (this.CharacterIndex >= Character.Instance.Count)
            {
                this.CharacterIndexField = 0;
            }
        }

        /// <summary>
        /// Change the character to the previous.
        /// </summary>
        private void PreviousCharacter()
        {
            this.CharacterIndexField--;

            if (this.CharacterIndex < 0)
            {
                this.CharacterIndexField = Character.Instance.Count - 1;
            }
        }

        /// <summary>
        /// Check if the attributes can be changed
        /// </summary>
        /// <param name="value">The value that will be added to the attribute</param>
        private bool CanChangeAttribute(int attribute, int value)
        {
            int futureAttributePoints = this.AttributePoints - value;
            int futureAttributeValue = attribute + value;
            return (futureAttributePoints >= 0 && futureAttributePoints <= IRTSettings.Default.AttributePointsPerUnit) &&
                    (futureAttributeValue >= 0 && futureAttributeValue <= IRTSettings.Default.PointsPerAttribute);
        }

        /// <summary>
        /// Change the value of the stength
        /// </summary>
        /// <param name="value">The value that will be added to the strength</param>
        private void ChangeStrength(int value)
        {
            if (this.CanChangeAttribute(this.Strength, value))
            {
                this.StrengthField += value;
                this.AttributePoints -= value;
            }
        }

        /// <summary>
        /// Change the value of the agility
        /// </summary>
        /// <param name="value">The value that will be added to the agility</param>
        private void ChangeAgility(int value)
        {
            if (this.CanChangeAttribute(this.Agility, value))
            {
                this.AgilityField += value;
                this.AttributePoints -= value;
            }
        }

        /// <summary>
        /// Change the value of the vitality
        /// </summary>
        /// <param name="value">The value that will be added to the vitality</param>
        private void ChangeVitality(int value)
        {
            if (this.CanChangeAttribute(this.Vitality, value))
            {
                this.VitalityField += value;
                this.AttributePoints -= value;
            }
        }

        /// <summary>
        /// Change the value of the inteligence
        /// </summary>
        /// <param name="value">The value that will be added to the inteligence</param>
        private void ChangeInteligence(int value)
        {
            if (this.CanChangeAttribute(this.Inteligence, value))
            {
                this.InteligenceField += value;
                this.AttributePoints -= value;
            }
        }

        /// <summary>
        /// Change the value of the dexterity
        /// </summary>
        /// <param name="value">The value that will be added to the dexterity</param>
        private void ChangeDexterity(int value)
        {
            if (this.CanChangeAttribute(this.Dexterity, value))
            {
                this.DexterityField += value;
                this.AttributePoints -= value;
            }
        }

        #endregion

        #region Input Handling

        /// <summary>
        /// Input Handling for Cursor Down event.
        /// </summary>
        private void CursorDown(object sender, CursorDownArgs e)
        {
            // Touch at unit.            
            if ((e.Position.X > this.Position.X && e.Position.X < (this.Position.X + TextureManager.Instance.Sprites.Config.UnitGreen.Width)) &&
                (e.Position.Y > this.Position.Y && e.Position.Y < (this.Position.Y + TextureManager.Instance.Sprites.Config.UnitGreen.Height)))
            {
                this.Touch();
            }
            
            // Enable attributes only if the unit is selected.
            else if (this.Selected)
            {
                Vector2 attributePosition = new Vector2((640 * ((int)this.Position.X / 640)) + 38, 379);

                // Previous character
                if ((e.Position.X > (attributePosition.X + 23) && e.Position.X < (attributePosition.X + 104)) &&
                    (e.Position.Y > (attributePosition.Y + 49) && e.Position.Y < (attributePosition.Y + 247)))
                {
                    this.PreviousCharacter();
                }

                // Next character
                else if ((e.Position.X > (attributePosition.X + 104) && e.Position.X < (attributePosition.X + 184)) &&
                    (e.Position.Y > (attributePosition.Y + 49) && e.Position.Y < (attributePosition.Y + 247)))
                {
                    this.NextCharacter();
                }

                // -5
                else if (e.Position.X > (attributePosition.X + 345) && e.Position.X < (attributePosition.X + 378))
                {
                    // Strength
                    if (e.Position.Y > (attributePosition.Y + 82) && e.Position.Y < (attributePosition.Y + 115))
                    {
                        this.ChangeStrength(-5);
                    }

                    // Agility
                    else if (e.Position.Y > (attributePosition.Y + 115) && e.Position.Y < (attributePosition.Y + 147))
                    {
                        this.ChangeAgility(-5);
                    }

                    // Vitality
                    else if (e.Position.Y > (attributePosition.Y + 147) && e.Position.Y < (attributePosition.Y + 181))
                    {
                        this.ChangeVitality(-5);
                    }

                    // Inteligence
                    else if (e.Position.Y > (attributePosition.Y + 181) && e.Position.Y < (attributePosition.Y + 214))
                    {
                        this.ChangeInteligence(-5);
                    }

                    // Dexterity
                    else if (e.Position.Y > (attributePosition.Y + 214) && e.Position.Y < (attributePosition.Y + 247))
                    {
                        this.ChangeDexterity(-5);
                    }
                }

                // -1
                else if (e.Position.X > (attributePosition.X + 378) && e.Position.X < (attributePosition.X + 411))
                {
                    // Strength
                    if (e.Position.Y > (attributePosition.Y + 82) && e.Position.Y < (attributePosition.Y + 115))
                    {
                        this.ChangeStrength(-1);
                    }

                    // Agility
                    else if (e.Position.Y > (attributePosition.Y + 115) && e.Position.Y < (attributePosition.Y + 147))
                    {
                        this.ChangeAgility(-1);
                    }

                    // Vitality
                    else if (e.Position.Y > (attributePosition.Y + 147) && e.Position.Y < (attributePosition.Y + 181))
                    {
                        this.ChangeVitality(-1);
                    }

                    // Inteligence
                    else if (e.Position.Y > (attributePosition.Y + 181) && e.Position.Y < (attributePosition.Y + 214))
                    {
                        this.ChangeInteligence(-1);
                    }

                    // Dexterity
                    else if (e.Position.Y > (attributePosition.Y + 214) && e.Position.Y < (attributePosition.Y + 247))
                    {
                        this.ChangeDexterity(-1);
                    }
                }

                // +1
                else if (e.Position.X > (attributePosition.X + 475) && e.Position.X < (attributePosition.X + 508))
                {
                    // Strength
                    if (e.Position.Y > (attributePosition.Y + 82) && e.Position.Y < (attributePosition.Y + 115))
                    {
                        this.ChangeStrength(1);
                    }

                    // Agility
                    else if (e.Position.Y > (attributePosition.Y + 115) && e.Position.Y < (attributePosition.Y + 147))
                    {
                        this.ChangeAgility(1);
                    }

                    // Vitality
                    else if (e.Position.Y > (attributePosition.Y + 147) && e.Position.Y < (attributePosition.Y + 181))
                    {
                        this.ChangeVitality(1);
                    }

                    // Inteligence
                    else if (e.Position.Y > (attributePosition.Y + 181) && e.Position.Y < (attributePosition.Y + 214))
                    {
                        this.ChangeInteligence(1);
                    }

                    // Dexterity
                    else if (e.Position.Y > (attributePosition.Y + 214) && e.Position.Y < (attributePosition.Y + 247))
                    {
                        this.ChangeDexterity(1);
                    }
                }

                // +5
                else if (e.Position.X > (attributePosition.X + 508) && e.Position.X < (attributePosition.X + 541))
                {
                    // Strength
                    if (e.Position.Y > (attributePosition.Y + 82) && e.Position.Y < (attributePosition.Y + 115))
                    {
                        this.ChangeStrength(5);
                    }

                    // Agility
                    else if (e.Position.Y > (attributePosition.Y + 115) && e.Position.Y < (attributePosition.Y + 147))
                    {
                        this.ChangeAgility(5);
                    }

                    // Vitality
                    else if (e.Position.Y > (attributePosition.Y + 147) && e.Position.Y < (attributePosition.Y + 181))
                    {
                        this.ChangeVitality(5);
                    }

                    // Inteligence
                    else if (e.Position.Y > (attributePosition.Y + 181) && e.Position.Y < (attributePosition.Y + 214))
                    {
                        this.ChangeInteligence(5);
                    }

                    // Dexterity
                    else if (e.Position.Y > (attributePosition.Y + 214) && e.Position.Y < (attributePosition.Y + 247))
                    {
                        this.ChangeDexterity(5);
                    }
                }
            }
        }

        #endregion
    }
}