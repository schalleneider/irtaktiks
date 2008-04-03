using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IRTaktiks.Components.Drawables;

namespace IRTaktiks.Components.Playables
{
	/// <summary>
	/// Representation of one player of game.
	/// </summary>
	public class Player : DrawableGameComponent
	{
		#region Properties

        /// <summary>
        /// Index of the player.
        /// </summary>
        private PlayerIndex PlayerIndexField;

        /// <summary>
        /// Index of the player.
        /// </summary>
        public PlayerIndex PlayerIndex
        {
            get { return PlayerIndexField; }
        }
        
        /// <summary>
		/// Name of player.
		/// </summary>
		private String NameField;

		/// <summary>
		/// Name of player.
		/// </summary>
		public String Name
		{
			get { return NameField; }
		}

		/// <summary>
		/// The combat units that the player posseses.
		/// </summary>
		private List<Unit> UnitsField;

		/// <summary>
		/// The combat units that the player posseses.
		/// </summary>
		public List<Unit> Units
		{
			get { return UnitsField; }
		}

        /// <summary>
        /// Menu of the player.
        /// </summary>
        private PlayerMenu MenuField;

        /// <summary>
        /// Menu of the player.
        /// </summary>
        public PlayerMenu Menu
        {
            get { return MenuField; }
            set { MenuField = value; }
        }

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor of class.
		/// </summary>
		/// <param name="game">The instance of game that is running.</param>
        /// <param name="name">The name of the player.</param>
        /// <param name="index">The index of the player.</param>
		public Player(Game game, String name, PlayerIndex index)
            : base(game)
		{
            this.NameField = name;
            this.PlayerIndexField = index;

            this.MenuField = new PlayerMenu(game, this);

			this.UnitsField = new List<Unit>();
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
            base.Draw(gameTime);
        }

		#endregion
	}
}