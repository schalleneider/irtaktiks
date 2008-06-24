using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IRTaktiks.Input;
using IRTaktiks.Input.EventArgs;
using IRTaktiks.Components.Playable;
using IRTaktiks.Components.Logic;
using IRTaktiks.Components.Manager;
using IRTaktiks.Components.Config;

namespace IRTaktiks.Components.Screen
{
    /// <summary>
    /// Config screen of the game.
    /// </summary>
    public class ConfigScreen : IScreen
    {
        #region Properties

        /// <summary>
        /// The configuration manager of the player one.
        /// </summary>
        private ConfigurationManager PlayerOneConfigurationManagerField;

        /// <summary>
        /// The configuration manager of the player one.
        /// </summary>
        public ConfigurationManager PlayerOneConfigurationManager
        {
            get { return PlayerOneConfigurationManagerField; }
            set { PlayerOneConfigurationManagerField = value; }
        }

        /// <summary>
        /// The configuration manager of the player two.
        /// </summary>
        private ConfigurationManager PlayerTwoConfigurationManagerField;

        /// <summary>
        /// The configuration manager of the player two.
        /// </summary>
        public ConfigurationManager PlayerTwoConfigurationManager
        {
            get { return PlayerTwoConfigurationManagerField; }
            set { PlayerTwoConfigurationManagerField = value; }
        }

        /// <summary>
        /// Indicates that the player one done its configuration.
        /// </summary>
        private bool PlayerOneConfigurated;

        /// <summary>
        /// Indicates that the player two done its configuration.
        /// </summary>
        private bool PlayerTwoConfigurated;

        #endregion

        #region Constructor

        /// <summary>
		/// Constructor of class.
		/// </summary>
		/// <param name="game">The instance of game that is running.</param>
        /// <param name="priority">Drawing priority of screen. Higher indicates that the screen will be at the top of the others.</param>
        public ConfigScreen(Game game, int priority)
            : base(game, priority)
        {
            this.PlayerOneConfigurationManagerField = new ConfigurationManager(game, PlayerIndex.One);
            this.PlayerTwoConfigurationManagerField = new ConfigurationManager(game, PlayerIndex.Two);

            this.PlayerOneConfigurated = false;
            this.PlayerTwoConfigurated = false;

            this.PlayerOneConfigurationManager.Configurated += new ConfigurationManager.ConfiguratedEventHandler(PlayerOne_Configurated);
            this.PlayerTwoConfigurationManager.Configurated += new ConfigurationManager.ConfiguratedEventHandler(PlayerTwo_Configurated);
		}

		#endregion

		#region Component Methods

		/// <summary>
		/// Allows the game component to perform any initialization it needs to before starting
		/// to run. This is where it can query for any required services and load content.
		/// </summary>
		public override void Initialize()
		{
            // Configuration managers
            this.Components.Add(this.PlayerOneConfigurationManager);
            this.Components.Add(this.PlayerTwoConfigurationManager);

            // Player one configuration items.
            this.Components.Add(this.PlayerOneConfigurationManager.Keyboard);
            this.Components.Add(this.PlayerOneConfigurationManager.PlayerConfig);
            
            for (int index = 0; index < this.PlayerOneConfigurationManager.UnitsConfig.Count; index++)
            {
                this.Components.Add(this.PlayerOneConfigurationManager.UnitsConfig[index]);
            }

            // Player two configuration items.
            this.Components.Add(this.PlayerTwoConfigurationManager.Keyboard);
            this.Components.Add(this.PlayerTwoConfigurationManager.PlayerConfig);

            for (int index = 0; index < this.PlayerTwoConfigurationManager.UnitsConfig.Count; index++)
            {
                this.Components.Add(this.PlayerTwoConfigurationManager.UnitsConfig[index]);
            }

            // Shows the map.
            IRTGame game = this.Game as IRTGame; 
            game.MapManager.Visible = true;

            base.Initialize();
		}

		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
            if (this.PlayerOneConfigurated && this.PlayerTwoConfigurated)
            {
                this.ConfigGame();
                (this.Game as IRTGame).ChangeScreen(IRTGame.GameScreens.GameScreen);
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
            base.Draw(gameTime);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Configure the game, creating all players and its units.
        /// </summary>
        private void ConfigGame()
        {
            IRTGame game = this.Game as IRTGame;

            game.PlayerOne = this.ConfigPlayer(this.PlayerOneConfigurationManager);
            game.PlayerTwo = this.ConfigPlayer(this.PlayerTwoConfigurationManager);
        }

        /// <summary>
        /// Configure one player, based on the given ConfigurationManager informations.
        /// </summary>
        /// <param name="configurationManager">The ConfigurationManager used to configure the player.</param>
        /// <returns>One player with its units configured.</returns>
        private Player ConfigPlayer(ConfigurationManager configurationManager)
        {
            Player player = new Player(this.Game, configurationManager.PlayerConfig.DisplayText.ToString(), configurationManager.PlayerIndex);

            for (int index = 0; index < configurationManager.UnitsConfig.Count; index++)
            {
                Attributes unitAttribute =
                    new Attributes(
                        IRTSettings.Default.BaseLevel,
                        Character.Instance[configurationManager.UnitsConfig[index].CharacterIndex].Job,
                        Element.Holy,
                        configurationManager.UnitsConfig[index].Strength,
                        configurationManager.UnitsConfig[index].Agility,
                        configurationManager.UnitsConfig[index].Vitality,
                        configurationManager.UnitsConfig[index].Inteligence,
                        configurationManager.UnitsConfig[index].Dexterity
                    );

                string unitName = configurationManager.UnitsConfig[index].DisplayText.ToString();
                Texture2D unitTexture = Character.Instance[configurationManager.UnitsConfig[index].CharacterIndex].Texture;
                
                Vector2 unitPosition = default(Vector2);
                Orientation unitOrientation = default(Orientation);

                switch (configurationManager.PlayerIndex)
                {
                    case PlayerIndex.One:
                        unitPosition = new Vector2((TextureManager.Instance.Sprites.Menu.Background.Width + 50), TextureManager.Instance.Sprites.Menu.Background.Width + (index * 60));
                        unitOrientation = Orientation.Right;
                        break;

                    case PlayerIndex.Two:
                        unitPosition = new Vector2(IRTSettings.Default.Width - (TextureManager.Instance.Sprites.Menu.Background.Width + 50), TextureManager.Instance.Sprites.Menu.Background.Width + (index * 60));
                        unitOrientation = Orientation.Left;
                        break;
                }

                Unit unit = new Unit(this.Game, player, unitPosition, unitAttribute, unitOrientation, unitTexture, unitName);

                player.Units.Add(unit);
            }

            return player;
        }

        #endregion

        #region Event Handling

        /// <summary>
        /// Executes when the player one done its configuration.
        /// </summary>
        private void PlayerOne_Configurated()
        {
            this.PlayerOneConfigurated = true;

            this.PlayerOneConfigurationManager.Unregister();
            this.PlayerOneConfigurationManager.Keyboard.Unregister();
            this.PlayerOneConfigurationManager.PlayerConfig.Unregister();
            for (int index = 0; index < this.PlayerOneConfigurationManager.UnitsConfig.Count; index++)
            {
                this.PlayerOneConfigurationManager.UnitsConfig[index].Unregister();
            }
        }

        /// <summary>
        /// Executes when the player two done its configuration.
        /// </summary>
        private void PlayerTwo_Configurated()
        {
            this.PlayerTwoConfigurated = true;

            this.PlayerTwoConfigurationManager.Unregister();
            this.PlayerTwoConfigurationManager.Keyboard.Unregister();
            this.PlayerTwoConfigurationManager.PlayerConfig.Unregister();
            for (int index = 0; index < this.PlayerTwoConfigurationManager.UnitsConfig.Count; index++)
            {
                this.PlayerTwoConfigurationManager.UnitsConfig[index].Unregister();
            }
        }

        #endregion
    }
}