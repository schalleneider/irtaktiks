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

            // Player two configuration items.
            this.Components.Add(this.PlayerTwoConfigurationManager.Keyboard);

            // Shows the map.
            (this.Game as IRTGame).MapManager.Visible = true;

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
        //  with component-specific drawing code.
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


/*

// Create the players
game.PlayerOne = new Player(this.Game, "Willians", PlayerIndex.One);
game.PlayerTwo = new Player(this.Game, "Sakura", PlayerIndex.Two);

// Create attributes
Attributes attributesAssasin = new Attributes(99, Job.Assasin, Element.Wind, 20, 99, 20, 1, 40);
Attributes attributesKnight = new Attributes(99, Job.Knight, Element.Fire, 99, 1, 50, 1, 50);
Attributes attributesMonk = new Attributes(99, Job.Monk, Element.Earth, 50, 30, 10, 30, 10);

Attributes attributesPaladin = new Attributes(99, Job.Paladin, Element.Water, 80, 1, 60, 80, 20);
Attributes attributesPriest = new Attributes(99, Job.Priest, Element.Holy, 1, 1, 80, 99, 30);
Attributes attributesWizard = new Attributes(99, Job.Wizard, Element.Dark, 1, 1, 1, 99, 99);

Unit assasin = new Unit(this.Game, game.PlayerOne, new Vector2(300, 300), attributesAssasin, Orientation.Right, TextureManager.Instance.Characters.Knight, "Assasin");
Unit knight = new Unit(this.Game, game.PlayerOne, new Vector2(300, 400), attributesKnight, Orientation.Right, TextureManager.Instance.Characters.Knight, "Knight");
Unit monk = new Unit(this.Game, game.PlayerOne, new Vector2(300, 500), attributesMonk, Orientation.Right, TextureManager.Instance.Characters.Knight, "Monk");

Unit paladin = new Unit(this.Game, game.PlayerTwo, new Vector2(800, 300), attributesPaladin, Orientation.Left, TextureManager.Instance.Characters.Wizard, "Paladin");
Unit priest = new Unit(this.Game, game.PlayerTwo, new Vector2(800, 400), attributesPriest, Orientation.Left, TextureManager.Instance.Characters.Wizard, "Priest");
Unit wizard = new Unit(this.Game, game.PlayerTwo, new Vector2(800, 500), attributesWizard, Orientation.Left, TextureManager.Instance.Characters.Wizard, "Wizard");

// Add the units for player one.
game.PlayerOne.Units.Add(knight);
game.PlayerOne.Units.Add(assasin);
game.PlayerOne.Units.Add(monk);

// Add the units for player two.
game.PlayerTwo.Units.Add(paladin);
game.PlayerTwo.Units.Add(priest);
game.PlayerTwo.Units.Add(wizard);

*/