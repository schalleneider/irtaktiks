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
using IRTaktiks.Input;

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
        /// The aim of the unit.
        /// </summary>
        private AimMenu AimField;

        /// <summary>
        /// The aim of the unit.
        /// </summary>
        public AimMenu Aim
        {
            get { return AimField; }
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

        #region Logic Properties

        /// <summary>
        /// Indicates if the menu needs to be updated.
        /// </summary>
        private bool ChangedField;

        /// <summary>
        /// Indicates if the menu needs to be updated.
        /// </summary>
        public bool Changed
        {
            get { return ChangedField; }
        }

        /// <summary>
        /// Indicates if the menu is freezed and its selected items cannot be changed.
        /// </summary>
        private bool FreezedField;

        /// <summary>
        /// Indicates if the menu is freezed and its selected items cannot be changed.
        /// </summary>
        public bool Freezed
        {
            get { return FreezedField; }
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

            this.ChangedField = true;
            this.FreezedField = false;

            // Create the actions.
            this.ActionsField = new List<ActionMenu>();

            // Create the aim.
            this.AimField = new AimMenu(unit);

            // Set the top left position for the player one.
            if (this.Unit.Player.PlayerIndex == PlayerIndex.One)
            {
                this.PositionField = new Vector2(0, 300);
            }

            // Set the top left position for the player two
            if (this.Unit.Player.PlayerIndex == PlayerIndex.Two)
            {
                this.PositionField = new Vector2(IRTGame.Width - TextureManager.Instance.Sprites.Menu.Item.Width, 300);
            }

            // Construct the menu.
            this.Construct();

            // Input
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
            // Check if the menu needs updates
            if (this.Changed)
            {
                // Set the positions of the items.
                for (int index = 0, positionIndex = 0; index < this.Actions.Count; index++)
                {
                    this.Actions[index].Position = new Vector2(this.Position.X, this.Position.Y + (positionIndex++ * TextureManager.Instance.Sprites.Menu.Item.Height));

                    // If the menu item is selected, must set the position for its subitems.
                    if (this.Actions[index].IsSelected)
                    {
                        // Set the position for all the subitems.
                        for (int subindex = 0; subindex < this.Actions[index].Commands.Count; subindex++)
                        {
                            this.Actions[index].Commands[subindex].Position = new Vector2(this.Position.X, this.Position.Y + (positionIndex++ * TextureManager.Instance.Sprites.Menu.Item.Height));
                        }
                    }
                }

                this.ChangedField = false;
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

            // Draw the items.
            for (int index = 0; index < this.Actions.Count; index++)
            {
                this.Actions[index].Draw(game.SpriteManager);

                // If the item is selected, draws the subitems.
                if (this.Actions[index].IsSelected)
                {
                    // Draws all the subitems.
                    for (int subindex = 0; subindex < this.Actions[index].Commands.Count; subindex++)
                    {
                        this.Actions[index].Commands[subindex].Draw(game.SpriteManager);
                    }
                }
            }

            // Draw the aim.
            this.Aim.Draw(game.SpriteManager, gameTime);

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

            // Handle the Execute event.
            moveAction.Execute += new ActionMenu.ExecuteEventHandler(moveAction_Execute);
            defendAction.Execute += new ActionMenu.ExecuteEventHandler(defendAction_Execute);

            // Create the commands.
            CommandMenu longAttackCommand = new CommandMenu(this.Unit, "Long", 5);
            CommandMenu shortAttackCommand = new CommandMenu(this.Unit, "Short", 0);

            CommandMenu healMagicCommand = new CommandMenu(this.Unit, "Heal", 200);
            CommandMenu fireMagicCommand = new CommandMenu(this.Unit, "Fire", 75);
            CommandMenu iceMagicCommand = new CommandMenu(this.Unit, "Ice", 105);
            CommandMenu thunderMagicCommand = new CommandMenu(this.Unit, "Thunder", 145);

            CommandMenu potionItemCommand = new CommandMenu(this.Unit, "Potion", 5);
            CommandMenu elixirItemCommand = new CommandMenu(this.Unit, "Elixir", 1);

            // Handle the Execute event.
            longAttackCommand.Execute += new ActionMenu.ExecuteEventHandler(longAttackCommand_Execute);
            shortAttackCommand.Execute += new ActionMenu.ExecuteEventHandler(shortAttackCommand_Execute);

            healMagicCommand.Execute += new ActionMenu.ExecuteEventHandler(healMagicCommand_Execute);
            fireMagicCommand.Execute += new ActionMenu.ExecuteEventHandler(fireMagicCommand_Execute);
            iceMagicCommand.Execute += new ActionMenu.ExecuteEventHandler(iceMagicCommand_Execute);
            thunderMagicCommand.Execute += new ActionMenu.ExecuteEventHandler(thunderMagicCommand_Execute);

            potionItemCommand.Execute += new ActionMenu.ExecuteEventHandler(potionItemCommand_Execute);
            elixirItemCommand.Execute += new ActionMenu.ExecuteEventHandler(elixirItemCommand_Execute);

            // Add the commands.
            attackAction.Commands.Add(longAttackCommand);
            attackAction.Commands.Add(shortAttackCommand);

            magicAction.Commands.Add(healMagicCommand);
            magicAction.Commands.Add(fireMagicCommand);
            magicAction.Commands.Add(iceMagicCommand);
            magicAction.Commands.Add(thunderMagicCommand);

            itemsAction.Commands.Add(potionItemCommand);
            itemsAction.Commands.Add(elixirItemCommand);

            // Add the actions.
            this.Actions.Add(moveAction);
            this.Actions.Add(defendAction);
            this.Actions.Add(attackAction);
            this.Actions.Add(magicAction);
            this.Actions.Add(itemsAction);
        }

        /// <summary>
        /// Reset the logic properties of the menu.
        /// </summary>
        public void Reset()
        {
            for (int index = 0; index < this.Actions.Count; index++)
            {
                for (int subindex = 0; subindex < this.Actions[index].Commands.Count; subindex++)
                {
                    this.Actions[index].Commands[subindex].IsSelected = false;
                }

                this.Actions[index].IsSelected = false;
            }

            this.ChangedField = true;
            this.FreezedField = false;

            this.Aim.Deactivate();
            this.Aim.Reset();
        }

        #endregion

        #region Input Handling

        /// <summary>
        /// Input Handling for Cursor Down event.
        /// </summary>
        private void CursorDown(object sender, CursorDownArgs e)
        {
            // Check if the items can be selected or executed.
            if (this.Enabled && this.Visible && !this.Freezed)
            {
                // Define if the touch was inside the menu area.
                float limitX = this.Position.X + TextureManager.Instance.Sprites.Menu.Item.Width;
                if (e.Position.X < limitX && e.Position.X > this.Position.X)
                {
                    // Calculate the count of items displayed in the menu.
                    int count = this.Actions.Count;
                    int selected = -1;
                    for (int index = 0; index < this.Actions.Count; index++)
                    {
                        // Guard the item that is selected.
                        if (this.Actions[index].IsSelected)
                        {
                            count += this.Actions[index].Commands.Count;
                            selected = index;
                        }

                        // Unselect all the items.
                        this.Actions[index].IsSelected = false;
                    }

                    // Define if the touch was inside the action menu area.
                    float limitY = this.Position.Y + (TextureManager.Instance.Sprites.Menu.Item.Width * count);
                    if (e.Position.Y < limitY && e.Position.Y > this.Position.Y)
                    {
                        // If items were selected
                        if (selected != -1)
                        {
                            // Verify the position in all items.
                            for (int index = 0; index < this.Actions.Count; index++)
                            {
                                // Check if the actual item is that was selected.
                                if (index == selected)
                                {
                                    // Check if the actual item has subitems
                                    if (this.Actions[index].Commands.Count > 0)
                                    {
                                        // Check if the touch was inside a subitems of the selected item.
                                        float sublimitY = this.Actions[index].Commands[this.Actions[index].Commands.Count - 1].Position.Y + TextureManager.Instance.Sprites.Menu.Item.Height;
                                        if (e.Position.Y < sublimitY && e.Position.Y > this.Actions[index].Commands[0].Position.Y)
                                        {
                                            // Calculates the index of the subitem touched.
                                            int subindex = (int)(e.Position.Y - this.Actions[index].Commands[0].Position.Y) / TextureManager.Instance.Sprites.Menu.Item.Height;

                                            // Reselect the item that was selected.
                                            this.Actions[index].IsSelected = true;

                                            // Select and queue the Execute event of the subitem touched.
                                            this.Actions[index].Commands[subindex].IsSelected = true;
                                            this.Actions[index].Commands[subindex].RaiseExecute();

                                            this.FreezedField = true;
                                            this.ChangedField = true;
                                        }
                                    }
                                }
                                else
                                {
                                    // Check if the touch was inside of the item.
                                    limitY = this.Actions[index].Position.Y + TextureManager.Instance.Sprites.Menu.Item.Height;
                                    if (e.Position.Y < limitY && e.Position.Y > this.Actions[index].Position.Y)
                                    {
                                        this.Actions[index].IsSelected = true;
                                        this.Actions[index].RaiseExecute();

                                        this.ChangedField = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Calculates the index of the item touched.
                            int index = (int)(e.Position.Y - this.Position.Y) / TextureManager.Instance.Sprites.Menu.Item.Height;

                            // Selects the menu item for the first time and queue the Execute event for dispatch.
                            this.Actions[index].IsSelected = true;
                            this.Actions[index].RaiseExecute();

                            this.ChangedField = true;
                        }
                    }
                }
            }
        }

        #endregion

        #region Action Events

        /// <summary>
        /// Handle the Execute event for the Move action.
        /// </summary>
        private void moveAction_Execute()
        {
            
        }

        /// <summary>
        /// Handle the Execute event for the Defend action.
        /// </summary>
        private void defendAction_Execute()
        {
            
        }

        #endregion

        #region Command Events

        /// <summary>
        /// Handle the Execute event for the Long command.
        /// </summary>
        private void longAttackCommand_Execute()
        {
            
        }

        /// <summary>
        /// Handle the Execute event for the Short command.
        /// </summary>
        private void shortAttackCommand_Execute()
        {
            
        }

        /// <summary>
        /// Handle the Execute event for the Heal command.
        /// </summary>
        private void healMagicCommand_Execute()
        {
            this.Aim.Activate();
            this.Aim.Aimed += new AimMenu.AimedEventHandler(HealMagic_Aimed);
        }

        /// <summary>
        /// Handle the Execute event for the Fire command.
        /// </summary>
        private void fireMagicCommand_Execute()
        {
            
        }

        /// <summary>
        /// Handle the Execute event for the Ice command.
        /// </summary>
        private void iceMagicCommand_Execute()
        {
            
        }

        /// <summary>
        /// Handle the Execute event for the Thunder command.
        /// </summary>
        private void thunderMagicCommand_Execute()
        {
            
        }

        /// <summary>
        /// Handle the Execute event for the Potion command.
        /// </summary>
        private void potionItemCommand_Execute()
        {
            
        }

        /// <summary>
        /// Handle the Execute event for the Elixir command.
        /// </summary>
        private void elixirItemCommand_Execute()
        {
            
        }

        #endregion

        #region Aim

        private void HealMagic_Aimed(Vector2 position)
        {
        }

        #endregion
    }
}
