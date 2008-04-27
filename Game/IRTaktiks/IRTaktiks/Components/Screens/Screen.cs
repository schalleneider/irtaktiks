using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace IRTaktiks.Components.Screens
{
    /// <summary>
    /// Mother class of all screens in the game.
    /// </summary>
    public class Screen : DrawableGameComponent, IComparable
    {
        #region Properties

        /// <summary>
        /// Child game components of screen.
        /// </summary>
        private GameComponentCollection ComponentsField;

        /// <summary>
        /// Child game components of screen.
        /// </summary>
        public GameComponentCollection Components
        {
            get { return ComponentsField; }
        }

        /// <summary>
        /// Drawing priority of screen. Higher indicates that the screen will be at the top of the others.
        /// </summary>
        private int PriorityField;
        
        /// <summary>
        /// Drawing priority of screen. Higher indicates that the screen will be at the top of the others.
        /// </summary>
        public int Priority
        {
            get { return PriorityField; }
            set { PriorityField = value; }
        }

		/// <summary>
		/// Indicates that the screen was inicialized.
		/// </summary>
		private bool InitializedField;

		/// <summary>
		/// Indicates that the screen was inicialized.
		/// </summary>
		public bool Initialized
		{
			get { return InitializedField; }
			set { InitializedField = value; }
		}

        #endregion

		#region Constructor

		/// <summary>
		/// Constructor of class.
		/// </summary>
		/// <param name="game">The instance of game that is running.</param>
        /// <param name="priority">Drawing priority of screen. Higher indicates that the screen will be at the top of the others.</param>
        public Screen(Game game, int priority)
            : base(game)
		{
			this.ComponentsField = new GameComponentCollection();

			this.Enabled = false;
			this.Visible = false;
			this.Initialized = false;
			this.Priority = priority;
		}

		#endregion

        #region Component Methods

		/// <summary>
		/// Allows the game component to perform any initialization it needs to before starting
		/// to run. This is where it can query for any required services and load content.
		/// </summary>
		public override void Initialize()
		{
            // Initializes all child components of the screen.
            foreach (GameComponent component in this.Components)
            {
				component.Initialize();
            }
            
            base.Initialize();

			this.Initialized = true;
		} 

		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
            // Update all child components of the screen.
            foreach (GameComponent component in this.Components)
            {
                // Check if the component can be updated.
                if (component.Enabled)
                {
                    component.Update(gameTime);
                }
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
            // Draws all child drawable components of the screen.
            foreach (GameComponent component in this.Components)
            {
                // Check if the component is a drawable component.
                if (component is DrawableGameComponent)
                {
                    // Check if the component can be drawed
                    if ((component as DrawableGameComponent).Visible)
                    {
                        (component as DrawableGameComponent).Draw(gameTime);
                    }
                }
            }

            // Flushes all the sprites to be drawn.
            (this.Game as IRTGame).AreaManager.Flush();
            (this.Game as IRTGame).SpriteManager.Flush();
            
            base.Draw(gameTime);
        }

		#endregion

		#region Other Methods

		/// <summary>
		/// Compare this screen with another, based in its priorities.
		/// </summary>
		/// <param name="screen">The screen to compare with this screen.</param>
		/// <returns>The diference between the priorities.</returns>
		public int CompareTo(object screen)
		{
			return this.Priority - (screen as Screen).Priority;
		}

		#endregion
	}
}