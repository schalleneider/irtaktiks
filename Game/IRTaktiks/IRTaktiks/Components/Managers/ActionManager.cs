using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IRTaktiks.Components.Menu;
using IRTaktiks.Components.Playables;
using IRTaktiks.Input.EventArgs;

namespace IRTaktiks.Components.Managers
{
    /// <summary>
    /// Manager of unit's commands.
    /// </summary>
    public class ActionManager : DrawableGameComponent
    {
        #region Properties
        
        /// <summary>
        /// The unit owner of the commands.
        /// </summary>
        private Unit UnitField;

        /// <summary>
        /// The unit owner of the commands.
        /// </summary>
        public Unit Unit
        {
            get { return UnitField; }
        }

        /// <summary>
        /// The actions of the unit.
        /// </summary>
        private List<ActionMenu> ActionsField;
        
        /// <summary>
        /// The actions of the unit.
        /// </summary>
        public List<ActionMenu> Actions
        {
            get { return ActionsField; }
        }

        /// <summary>
        /// The top left position of the actions and commands.
        /// </summary>
        private Vector2 PositionField;

        /// <summary>
        /// The top left position of the actions and commands.
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
        /// <param name="unit">The unit owner of the commands.</param>
        public ActionManager(Game game, Unit unit)
			: base(game)
		{
            // Set the unit owner of the commands.
			this.UnitField = unit;

            // Create the actions.
            this.ActionsField = new List<ActionMenu>();

			// Set the top left position for the player one.
			if (this.Unit.Player.PlayerIndex == PlayerIndex.One)
			{
				this.PositionField = new Vector2(0, 300);
			}

            // Set the top left position for the player two
			if (this.Unit.Player.PlayerIndex == PlayerIndex.Two)
			{
				this.PositionField = new Vector2(IRTGame.Width - TextureManager.Instance.Sprites.Menu.Background.Width, 300);
			}
            
            // Construct the menu.
            this.Construct();

            // Input
            InputManager.Instance.CursorDown += new EventHandler<IRTaktiks.Input.EventArgs.CursorDownArgs>(CursorDown);
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
            // Set the positions of the actions.
            for (int index = 0; index < this.Actions.Count; index++)
            {
                this.Actions[index].Position = new Vector2(this.Position.X, this.Position.Y + (index * 40));
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
            IRTGame game = this.Game as IRTGame;

            game.SpriteBatch.Begin();

            // Draw the actions.
            for (int index = 0; index < this.Actions.Count; index++)
            {
                this.Actions[index].Draw(game.SpriteBatch);
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
            // Create the actions.
            ActionMenu moveAction = new ActionMenu(this.Unit, "Move");
            ActionMenu defendAction = new ActionMenu(this.Unit, "Defend");
            ActionMenu attackAction = new ActionMenu(this.Unit, "Attack");
            ActionMenu magicAction = new ActionMenu(this.Unit, "Magic");
            ActionMenu itemsAction = new ActionMenu(this.Unit, "Items");

            // Add the actions.
            this.Actions.Add(moveAction);
            this.Actions.Add(defendAction);
            this.Actions.Add(attackAction);
            this.Actions.Add(magicAction);
            this.Actions.Add(itemsAction);
        }

        #endregion

        #region Input Handling

        /// <summary>
        /// Input Handling for Cursor Down event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CursorDown(object sender, CursorDownArgs e)
        {
            // Check if the items can be selected or executed.
            if (this.Enabled && this.Visible)
            {

            }

            // Handle the event only if the unit is selected.
            if (this.Unit.IsSelected)
            {
                // Touch was inside of the menu.
                if (((e.Position.X > this.Position.X) && e.Position.X < (this.Position.X + this.ItemTexture.Width)) &&
                    ((e.Position.Y > this.Position.Y) && e.Position.Y < (this.Position.Y + this.ItemTexture.Height)))
                {
                    this.IsSelected = true;
                }
            }
        }

        #endregion
    }
}
