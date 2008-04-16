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
using IRTaktiks.Input;
using IRTaktiks.Input.EventArgs;

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
        /// The items of the menu.
        /// </summary>
        private List<ActionMenuItem> ItemsField;

        /// <summary>
        /// The items of the menu.
        /// </summary>
        public List<ActionMenuItem> Items
        {
            get { if (ItemsField == null) ItemsField = new List<ActionMenuItem>(); return ItemsField; }
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
				this.PositionField = new Vector2(0, 300);
			}

			// Player two informations.
			if (this.Unit.Player.PlayerIndex == PlayerIndex.Two)
			{
				this.PositionField = new Vector2(IRTGame.Width - TextureManager.Instance.Sprites.Menu.Background.Width, 300);
			}

            // Construct the menu.
            this.Construct();

            // Input Handling
            InputManager.Instance.CursorUp += new EventHandler<CursorUpArgs>(CursorUp_Handler); 
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
            
            // Draw all the menuitems.
            for (int index = 0; index < this.Items.Count; index++)
            {
                // Draw the menuitem.
                this.Items[index].Draw(new Vector2(this.Position.X, this.Position.Y + (index * 40)), game.SpriteBatch);
                                
                // If the menuitem is selected.
                if (this.Items[index].IsSelected)
                {
                    // Draw all the subitems.
                    for (int subindex = 0; subindex < this.Items[index].Items.Count; subindex++)
                    {
                        this.Items[index].Items[subindex].Draw(new Vector2(this.Position.X, this.Position.Y + (index * 40)), game.SpriteBatch);
                    }
                }
            }

            game.SpriteBatch.End();

            base.Draw(gameTime);
		}

		#endregion

        #region Methods

        /// <summary>
        /// Construct the menu.
        /// </summary>
        private void Construct()
        {
            // 1st level items.
            ActionMenuItem move = new ActionMenuItem(null, "Move");
            ActionMenuItem defend = new ActionMenuItem(null, "Defend");
            ActionMenuItem attack = new ActionMenuItem(null, "Attack");
            ActionMenuItem magic = new ActionMenuItem(null, "Magic");
            ActionMenuItem item = new ActionMenuItem(null, "Item");

            // 2nd level items.
            ActionMenuItem closeAttack = new ActionMenuItem(attack, "Close");
            ActionMenuItem longAttack = new ActionMenuItem(attack, "Long");

            ActionMenuItem healMagic = new ActionMenuItem(magic, "Heal");
            ActionMenuItem fireMagic = new ActionMenuItem(magic, "Fire");
            ActionMenuItem iceMagic = new ActionMenuItem(magic, "Ice");
            ActionMenuItem thunderMagic = new ActionMenuItem(magic, "Thunder");

            ActionMenuItem potionMagic = new ActionMenuItem(item, "Potion");
            ActionMenuItem etherMagic = new ActionMenuItem(item, "Ether");
            ActionMenuItem elixirMagic = new ActionMenuItem(item, "Elixir");

            // Construct the root menu.
            this.Items.Add(move);
            this.Items.Add(defend);
            this.Items.Add(attack);
            this.Items.Add(magic);
            this.Items.Add(item);

            // Construct the attack menu.
            attack.Items.Add(closeAttack);
            attack.Items.Add(longAttack);

            // Construct the magic menu.
            magic.Items.Add(healMagic);
            magic.Items.Add(fireMagic);
            magic.Items.Add(iceMagic);
            magic.Items.Add(thunderMagic);

            // Construct the item menu.
            item.Items.Add(potionMagic);
            item.Items.Add(etherMagic);
            item.Items.Add(elixirMagic);
        }

        #endregion

        #region Input Handling

        /// <summary>
        /// Handles the CursorUp event.
        /// </summary>
        /// <param name="sender">Always null.</param>
        /// <param name="e">Data of event</param>
        private void CursorUp_Handler(object sender, CursorUpArgs e)
        {
            // Handles the input only if the unit is selected.
            if (this.Unit.IsSelected)
            {

            }
        }

        #endregion
    }
}