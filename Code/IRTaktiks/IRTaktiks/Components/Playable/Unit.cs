using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IRTaktiks.Components.Logic;
using IRTaktiks.Components.Menu;
using IRTaktiks.Components.Manager;
using IRTaktiks.Input;
using IRTaktiks.Input.EventArgs;
using IRTaktiks.Components.Action;

namespace IRTaktiks.Components.Playable
{
    /// <summary>
    /// Representation of one combat unit of the game.
    /// </summary>
    public class Unit : DrawableGameComponent
    {
        #region Statuses

        /// <summary>
        /// The actual life points of the unit.
        /// </summary>
        private int LifeField;

        /// <summary>
        /// The actual life points of the unit.
        /// </summary>
        public int Life
        {
            get { return LifeField; }
            set
            {
                if (value >= this.Attributes.MaximumLife)
                {
                    LifeField = this.Attributes.MaximumLife;
                }
                else if (value <= 0)
                {
                    LifeField = 0;
                }
                else
                {
                    LifeField = value;
                }
            }

        }

        /// <summary>
        /// The actual mana points of the unit.
        /// </summary>
        private int ManaField;

        /// <summary>
        /// The actual mana points of the unit.
        /// </summary>
        public int Mana
        {
            get { return ManaField; }
            set
            {
                if (value >= this.Attributes.MaximumMana)
                {
                    ManaField = this.Attributes.MaximumMana;
                }
                else if (value <= 0)
                {
                    ManaField = 0;
                }
                else
                {
                    ManaField = value;
                }
            }
        }

        /// <summary>
        /// The actual time of the wait to act. 
        /// Mininum value is 0 and maximum is 1.
        /// </summary>
        private double TimeField;

        /// <summary>
        /// The actual time of the wait to act.
        /// Mininum value is 0 and maximum is 1.
        /// </summary>
        public double Time
        {
            get { return TimeField; }
            set
            {
                if (value >= 1)
                {
                    TimeField = 1;
                }
                else if (value <= 0)
                {
                    TimeField = 0;
                }
                else
                {
                    TimeField = value;
                }
            }
        }

        /// <summary>
        /// The orientation of the unit.
        /// </summary>
        private Orientation OrientationField;

        /// <summary>
        /// The orientation of the unit.
        /// </summary>
        public Orientation Orientation
        {
            get { return OrientationField; }
            set { OrientationField = value; }
        }

        #endregion

        #region Verifications

        /// <summary>
        /// True if the unit is dead. Else otherwise.
        /// </summary>
        public bool IsDead
        {
            get { return this.Life <= 0; }
        }

        /// <summary>
        /// True if the unit is waiting. Else otherwise.
        /// </summary>
        public bool IsWaiting
        {
            get { return this.Time < 1; }
        }

        /// <summary>
        /// True if the actual life of the unit is above 50%.
        /// </summary>
        public bool IsStatusAlive
        {
            get { return ((float)this.Life / (float)this.Attributes.MaximumLife) > 0.5; }
        }

        /// <summary>
        /// True if the actual life of the unit is above 10% and less 50%.
        /// </summary>
        public bool IsStatusDamaged
        {
            get { return (((float)this.Life / (float)this.Attributes.MaximumLife) <= 0.5 && (((float)this.Life / (float)this.Attributes.MaximumLife) >= 0.1)); }
        }

        /// <summary>
        /// True if the actual life of the unit is less 10%.
        /// </summary>
        public bool IsStatusDeading
        {
            get { return ((float)this.Life / (float)this.Attributes.MaximumLife) < 0.1; }
        }

        /// <summary>
        /// True if the unit can act on the menu.
        /// </summary>
        public bool CanAct
        {
            get { return this.Time == 1; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// The owner of the unit.
        /// </summary>
        private Player PlayerField;

        /// <summary>
        /// The owner of the unit.
        /// </summary>
        public Player Player
        {
            get { return PlayerField; }
        }

        /// <summary>
        /// Name of unit.
        /// </summary>
        private String NameField;

        /// <summary>
        /// Name of unit.
        /// </summary>
        public String Name
        {
            get { return NameField; }
        }

        /// <summary>
        /// Menu of the status of the unit.
        /// </summary>
        private StatusMenu StatusMenuField;

        /// <summary>
        /// Menu of the status of the unit.
        /// </summary>
        public StatusMenu StatusMenu
        {
            get { return StatusMenuField; }
        }

        /// <summary>
        /// The attributes of the unit.
        /// </summary>
        private Attributes AttributesField;

        /// <summary>
        /// The attributes of the unit.
        /// </summary>
        public Attributes Attributes
        {
            get { return AttributesField; }
        }

        /// <summary>
        /// The position of the Unit.
        /// </summary>
        private Vector2 PositionField;

        /// <summary>
        /// The position of the Unit.
        /// </summary>
        public Vector2 Position
        {
            get { return PositionField; }
            set { PositionField = value; }
        }

        /// <summary>
        /// The character texture of the unit.
        /// </summary>
        private Texture2D TextureField;

        /// <summary>
        /// The character texture of the unit.
        /// </summary>
        public Texture2D Texture
        {
            get { return TextureField; }
        }

        #endregion

        #region Actions

        /// <summary>
        /// The attack list of the unit.
        /// </summary>
        private List<Attack> AttacksField;

        /// <summary>
        /// The attack list of the unit.
        /// </summary>
        public List<Attack> Attacks
        {
            get { return AttacksField; }
        }
        
        /// <summary>
        /// The skill list of the unit.
        /// </summary>
        private List<Skill> SkillsField;

        /// <summary>
        /// The skill list of the unit.
        /// </summary>
        public List<Skill> Skills
        {
            get { return SkillsField; }
        }

        /// <summary>
        /// The item list of the unit.
        /// </summary>
        private List<Item> ItemsField;

        /// <summary>
        /// The item list of the unit.
        /// </summary>
        public List<Item> Items
        {
            get { return ItemsField; }
        }

        #endregion

        #region Managers
                
        /// <summary>
        /// The action manager of the unit.
        /// </summary>
        private ActionManager ActionManagerField;

        /// <summary>
        /// The action manager of the unit.
        /// </summary>
        public ActionManager ActionManager
        {
            get { return ActionManagerField; }
        }

        #endregion

        #region Logic Properties

        /// <summary>
        /// Indicates that the unit is selected.
        /// </summary>
        private bool IsSelectedField;

        /// <summary>
        /// Indicates that the unit is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return IsSelectedField; }
            set { IsSelectedField = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="game">The instance of game that is running.</param>
        /// <param name="player">The owner of the unit.</param>
        /// <param name="position">The position of the unit.</param>
        /// <param name="attributes">The attributes of the unit.</param>
        /// <param name="orientation">The orientation of the unit.</param>
        /// <param name="texture">The texture of the unit.</param>
        /// <param name="name">The name of unit.</param>
        public Unit(Game game, Player player, Vector2 position, Attributes attributes, Orientation orientation, Texture2D texture, String name)
            : base(game)
        {
            // Set the properties.
            this.PositionField = position;
            this.AttributesField = attributes;
            this.OrientationField = orientation;
            this.TextureField = texture;
            this.PlayerField = player;
            this.NameField = name;

            // Set the logic properties.
            this.LifeField = this.Attributes.MaximumLife;
            this.ManaField = this.Attributes.MaximumMana;
            this.TimeField = 0;
            this.IsSelectedField = false;

            // Load the actions
            this.AttacksField = AttackManager.Instance.Construct(this);
            this.SkillsField = SkillManager.Instance.Construct(this);
            this.ItemsField = ItemManager.Instance.Construct(this);

            // Create the menus.
            this.ActionManagerField = new ActionManager(game, this);
            this.StatusMenuField = new StatusMenu(game, this);

            InputManager.Instance.CursorDown += new EventHandler<CursorDownArgs>(CursorDown_Handler);
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
            // Update the status of the StatusMenu.
            this.StatusMenu.Enabled = this.IsSelected && !this.IsDead;
            this.StatusMenu.Visible = this.IsSelected && !this.IsDead;

            // Update the status of the ActionManager.
            this.ActionManager.Enabled = this.IsSelected && !this.IsDead && this.CanAct;
            this.ActionManager.Visible = this.IsSelected && !this.IsDead && this.CanAct;

            // Update the actual time of the unit.
            this.Time += this.Attributes.Delay;

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

            // Draws the unit character.
            game.SpriteManager.Draw(this.Texture, this.Position, new Rectangle(0, 48 * (int)this.Orientation, 32, 48), Color.White, 30);

            // Draws the quick status.
            Vector2 statusPosition = new Vector2(this.Position.X + (this.Texture.Width / 2) - (TextureManager.Instance.Sprites.Unit.QuickStatus.Width / 2), this.Position.Y + 50);
            game.SpriteManager.Draw(TextureManager.Instance.Sprites.Unit.QuickStatus, statusPosition, Color.White, 31);
            
            // Measure the maximum width and height for the bars.
            int barMaxWidth = TextureManager.Instance.Sprites.Unit.QuickStatus.Width - 2;
            int barMaxHeight = (TextureManager.Instance.Sprites.Unit.QuickStatus.Height - 2) / 3;

            // Calculates the position of the life, mana and time bars
            Vector2 lifePosition = new Vector2(statusPosition.X + 1, statusPosition.Y + 1);
            Vector2 manaPosition = new Vector2(statusPosition.X + 1, statusPosition.Y + 2 + (1 * barMaxHeight));
            Vector2 timePosition = new Vector2(statusPosition.X + 1, statusPosition.Y + 3 + (2 * barMaxHeight));

            // Calculates the area of the life, mana and time bars, based in unit's values.
            Rectangle lifeBar = new Rectangle((int)lifePosition.X, (int)lifePosition.Y, (int)((barMaxWidth * this.Life) / this.Attributes.MaximumLife), barMaxHeight);
            Rectangle manaBar = new Rectangle((int)manaPosition.X, (int)manaPosition.Y, (int)((barMaxWidth * this.Mana) / this.Attributes.MaximumMana), barMaxHeight);
            Rectangle timeBar = new Rectangle((int)timePosition.X, (int)timePosition.Y, (int)(barMaxWidth * this.Time), barMaxHeight);

            // Draws the life, mana and time bars.
            game.SpriteManager.Draw(TextureManager.Instance.Sprites.Unit.LifeBar, lifeBar, Color.White, 32);
            game.SpriteManager.Draw(TextureManager.Instance.Sprites.Unit.ManaBar, manaBar, Color.White, 32);
            game.SpriteManager.Draw(TextureManager.Instance.Sprites.Unit.TimeBar, timeBar, Color.White, 32);

            // Draws the arrow, if the unit is selected.
            if (this.IsSelected)
            {
                Vector2 arrowPosition = new Vector2(this.Position.X + (this.Texture.Width / 2) - (TextureManager.Instance.Sprites.Unit.Arrow.Width / 2), this.Position.Y - TextureManager.Instance.Sprites.Unit.Arrow.Height);
                game.SpriteManager.Draw(TextureManager.Instance.Sprites.Unit.Arrow, arrowPosition, Color.White, 31);
            }

            base.Draw(gameTime);
        }

        #endregion

        #region Input Handling

        /// <summary>
        /// Handles the CursorDown event.
        /// </summary>
        /// <param name="sender">Always null.</param>
        /// <param name="e">Data of event</param>
        private void CursorDown_Handler(object sender, CursorDownArgs e)
        {
            // Test if the unit can be selected
            if (!this.IsDead && !this.IsSelected)
            {
                // Touch was inside of the X area of the character.
                if (e.Position.X < (this.Position.X + this.Texture.Width) && e.Position.X > this.Position.X)
                {
                    // Touch was inside of the Y area of the character.
                    if (e.Position.Y < (this.Position.Y + this.Texture.Height / 4) && e.Position.Y > this.Position.Y)
                    {
                        // Unselect the player's units.                
                        foreach (Unit unit in this.Player.Units)
                        {
                            unit.IsSelected = false;
                        }

                        this.IsSelected = true;
                    }
                }
            }
        }

        #endregion
    }
}