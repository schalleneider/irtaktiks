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
            (this.Game as IRTGame).MapManager.Visible = false;

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

        #region Event Handling

        /// <summary>
        /// Executes when the player one done its configuration.
        /// </summary>
        private void PlayerOne_Configurated()
        {
            this.PlayerOneConfigurated = true;
        }

        /// <summary>
        /// Executes when the player two done its configuration.
        /// </summary>
        private void PlayerTwo_Configurated()
        {
            this.PlayerTwoConfigurated = true;
        }

        #endregion
    }
}