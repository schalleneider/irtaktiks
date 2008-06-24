using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IRTaktiks.Components.Config;
using IRTaktiks.Input;
using IRTaktiks.Input.EventArgs;

namespace IRTaktiks.Components.Manager
{
    /// <summary>
    /// Manager of the configuration.
    /// </summary>
    public class ConfigurationManager : DrawableGameComponent
    {
        #region Event

        /// <summary>
        /// The delegate of methods that will handle the touched event.
        /// </summary>
        public delegate void ConfiguratedEventHandler();

        /// <summary>
        /// Event fired when a player end the configuration of the items.
        /// </summary>
        public event ConfiguratedEventHandler Configurated;

        #endregion

        #region Properties

        /// <summary>
        /// The index of the player.
        /// </summary>
        private PlayerIndex PlayerIndexField;

        /// <summary>
        /// The index of the player.
        /// </summary>
        public PlayerIndex PlayerIndex
        {
            get { return PlayerIndexField; }
        }

        /// <summary>
        /// The value of the base position.
        /// </summary>
        private Vector2 BasePositionField;
        
        /// <summary>
        /// The value of the base position.
        /// </summary>
        public Vector2  BasePosition
        {
            get { return BasePositionField; }
        }
        
        /// <summary>
        /// The actual selected item.
        /// </summary>
        private Configurable SelectedItemField;

        /// <summary>
        /// The actual selected item.
        /// </summary>
        public Configurable SelectedItem
        {
            get { return SelectedItemField; }
        }

        /// <summary>
        /// The config keyboard.
        /// </summary>
        private Keyboard KeyboardField;

        /// <summary>
        /// The config keyboard.
        /// </summary>
        public Keyboard Keyboard
        {
            get { return KeyboardField; }
        }

        /// <summary>
        /// The config of player.
        /// </summary>
        private PlayerConfig PlayerConfigField;

        /// <summary>
        /// The config of player.
        /// </summary>
        public PlayerConfig PlayerConfig
        {
            get { return PlayerConfigField; }
        }

        /// <summary>
        /// The list of the config of unit
        /// </summary>
        private List<UnitConfig> UnitsConfigField;

        /// <summary>
        /// The list of the config of unit
        /// </summary>
        public List<UnitConfig> UnitsConfig
        {
            get { return UnitsConfigField; }
        }

        /// <summary>
        /// Indicate that the game is ready to start;
        /// </summary>
        private bool ReadyToStart;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="game">The instance of game that is running.</param>
        public ConfigurationManager(Game game, PlayerIndex playerIndex)
            : base(game)
        {
            this.PlayerIndexField = playerIndex;

            switch (playerIndex)
            {
                case PlayerIndex.One:
                    this.BasePositionField = new Vector2(0, 0);
                    break;

                case PlayerIndex.Two:
                    this.BasePositionField = new Vector2(640, 0);
                    break;
            }

            // Items creation
            this.KeyboardField = new Keyboard(game, playerIndex);
            this.PlayerConfigField = new PlayerConfig(game, playerIndex);
            
            // Units config creation
            this.UnitsConfigField = new List<UnitConfig>();
            for (int index = 0; index < IRTSettings.Default.UnitsPerPlayer; index++)
            {
                UnitConfig unitConfig = new UnitConfig(game, playerIndex, index);
                unitConfig.Touched += new Configurable.TouchedEventHandler(Configurable_Touched);
                this.UnitsConfig.Add(unitConfig);
            }

            // Event handling
            this.Keyboard.Typed += new Keyboard.TypedEventHandler(Keyboard_Typed);
            this.PlayerConfig.Touched += new Configurable.TouchedEventHandler(Configurable_Touched);

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
            this.ReadyToStart = this.Keyboard.Configurated && this.PlayerConfig.Configurated && 
                this.UnitsConfig.FindAll(
                    delegate(UnitConfig unitConfig)
                    { 
                        return unitConfig.Configurated;
                    }
                ).Count == this.UnitsConfig.Count;
            
            base.Update(gameTime);
        }

        /// <summary>
        /// Called when the DrawableGameComponent needs to be drawn. Override this method
        /// with component-specific drawing code.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            if (this.ReadyToStart)
            {
                IRTGame game = this.Game as IRTGame;

                game.SpriteManager.Draw(TextureManager.Instance.Sprites.Config.Start, new Vector2(this.BasePositionField.X + 39, this.BasePositionField.Y + 655), Color.White, 30);
            }
                        
            base.Draw(gameTime);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handle the CursorDown event.
        /// </summary>
        private void CursorDown(object sender, CursorDownArgs e)
        {
            if (this.ReadyToStart)
            {
                if ((e.Position.X > 39 && e.Position.X < (39 + TextureManager.Instance.Sprites.Config.Start.Width)) &&
                    (e.Position.Y > 655 && e.Position.Y < (655 + TextureManager.Instance.Sprites.Config.Start.Height)))
                {
                    this.Configurated();
                }
            }
        }

        /// <summary>
        /// Unregister all events of the item.
        /// </summary>
        public void Unregister()
        {
            InputManager.Instance.CursorDown -= CursorDown;
        }

        #endregion

        #region Event Handling

        /// <summary>
        /// Handle the pressed event of the keyboard.
        /// </summary>
        /// <param name="letter">The letther that was touched on the keyboard.</param>
        private void Keyboard_Typed(string letter)
        {
            if (this.SelectedItem != null)
            {
                if (letter != null)
                {
                    this.SelectedItem.DisplayText.Append(letter);
                }
                else
                {
                    if (this.SelectedItem.DisplayText.Length > 0)
                    {
                        this.SelectedItem.DisplayText.Remove(this.SelectedItem.DisplayText.Length - 1, 1);
                    }
                }
            }
        }

        /// <summary>
        /// Handle the touched event of the items.
        /// </summary>
        /// <param name="item">The item touched</param>
        private void Configurable_Touched(Configurable item)
        {
            if (item.Selected)
            {
                this.SelectedItemField = null;
                item.Selected = false;
            }
            else
            {
                this.Keyboard.Selected = false;
                this.PlayerConfig.Selected = false;

                for (int index = 0; index < this.UnitsConfig.Count; index++)
                {
                    this.UnitsConfig[index].Selected = false;
                }

                this.SelectedItemField = item;
                item.Selected = true;
            }
        }

        #endregion
    }
}
