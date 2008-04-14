using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IRTaktiks.Components.Playables;
using IRTaktiks.Components.Managers;

namespace IRTaktiks.Components.Menu
{
	/// <summary>
	/// Representation of the action menu of the unit.
	/// </summary>
	public class ActionMenu : DrawableGameComponent
	{
        #region Properties
			
		/// <summary>
        /// The unit whose actions will be displayed.
        /// </summary>
        private Unit UnitField;

        /// <summary>
        /// The unit whose actions will be displayed.
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

        #endregion

        #region Constructor

        /// <summary>
		/// Constructor of class.
		/// </summary>
		/// <param name="game">The instance of game that is running.</param>
        /// <param name="unit">The unit who is the owner of the menu.</param>
		public ActionMenu(Game game, Unit unit)
			: base(game)
		{
            // Set the unit whose information will be displayed.
			this.UnitField = unit;

			// Player one informations.
			if (this.Unit.Player.PlayerIndex == PlayerIndex.One)
			{
				this.PositionField = new Vector2(0, 200);
			}

			// Player two informations.
			if (this.Unit.Player.PlayerIndex == PlayerIndex.Two)
			{
				this.PositionField = new Vector2(IRTGame.Width - TextureManager.Instance.Sprites.Menu.Background.Width, 200);
			}
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

            game.SpriteBatch.Begin();

            game.SpriteBatch.End();

            base.Draw(gameTime);
		}

		#endregion

        #region ActionMenuItem Class
        
        public class ActionMenuItem
        {
            #region Items

            /// <summary>
            /// The subitems of the menu item.
            /// </summary>
            private List<ActionMenuItem> ItemsField;

            /// <summary>
            /// The subitems of the menu item.
            /// </summary>
            public List<ActionMenuItem> Items
            {
                get { return ItemsField; }
            }

            #endregion

            #region Event

            

            #endregion

            #region Properties

            /// <summary>
            /// The texture that will be used to draw the itens of the menu.
            /// </summary>
            private Texture2D ItemTexture;

            /// <summary>
            /// The texture that will be used to draw the subitens of the menu.
            /// </summary>
            private Texture2D SubItemTexture;

            /// <summary>
            /// The texture that will be used to draw selected itens of the menu.
            /// </summary>
            private Texture2D NormalItemTexture;

            /// <summary>
            /// The sprite font that will be used to write the menu item.
            /// </summary>
            private SpriteFont TextSpriteFont;

            #endregion

            #region Constructor

            public ActionMenuItem()
            {

            }

            #endregion

            #region Methods

            /// <summary>
            /// Draws the menuitem at the specified position.
            /// </summary>
            /// <param name="position">The position that the menuitem will be drawed.</param>
            public void Draw(Vector2 position)
            {

            }

            #endregion
        }

        #endregion
    }
}