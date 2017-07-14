using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using System.Text;

namespace IRTaktiks.Components.Config
{
    /// <summary>
    /// Representation of one configuration item.
    /// </summary>
    public abstract class Configurable : DrawableGameComponent
    {
        #region Event

        /// <summary>
        /// The delegate of methods that will handle the touched event.
        /// </summary>
        /// <param name="item">The item touched</param>
        public delegate void TouchedEventHandler(Configurable item);

        /// <summary>
        /// Event fired when the item is touched.
        /// </summary>
        public event TouchedEventHandler Touched;

        #endregion

        #region Properties

        /// <summary>
        /// The text that the item will display.
        /// </summary>
        protected StringBuilder DisplayTextField;

        /// <summary>
        /// The text that the item will display.
        /// </summary>
        public StringBuilder DisplayText
        {
            get { return DisplayTextField; }
        }

        /// <summary>
        /// Indicate that the item is configurated.
        /// </summary>
        public abstract bool Configurated
        {
            get;
        }

        /// <summary>
        /// Indicate that the item is selected.
        /// </summary>
        private bool SelectedField;
        
        /// <summary>
        /// Indicate that the item is selected.
        /// </summary>
        public bool Selected
        {
            get { return SelectedField; }
            set { SelectedField = value; }
        }
        
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
        /// The position of the item.
        /// </summary>
        protected Vector2 PositionField;

        /// <summary>
        /// The position of the item.
        /// </summary>
        public Vector2 Position
        {
            get { return PositionField; }
        }

        /// <summary>
        /// The texture of the item.
        /// </summary>
        protected Texture2D TextureField;

        /// <summary>
        /// The texture of the item.
        /// </summary>
        public Texture2D Texture
        {
            get { return TextureField; }
        }
                                
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="game">The instance of game that is running.</param>
        /// <param name="playerIndex">The index of the player.</param>
        public Configurable(Game game, PlayerIndex playerIndex)
            : base(game)
        {
            this.DisplayTextField = new StringBuilder();
            this.PlayerIndexField = playerIndex;

            this.SelectedField = false;
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
            base.Draw(gameTime);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Touch the item.
        /// </summary>
        protected void Touch()
        {
            this.Touched(this);
        }

        /// <summary>
        /// Unregister all touch events in the keyboard.
        /// </summary>
        public abstract void Unregister();

        #endregion
    }
}