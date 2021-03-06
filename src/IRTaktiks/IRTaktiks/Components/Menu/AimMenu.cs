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
	/// Representation of the aim of attack of the unit.
	/// </summary>
	public class AimMenu
	{
		#region Properties

        /// <summary>
        /// The unit who will be the owner of the aim.
        /// </summary>
        private Unit UnitField;

        /// <summary>
        /// The unit who will be the owner of the aim.
        /// </summary>
        public Unit Unit
        {
            get { return UnitField; }
        }
                
        /// <summary>
        /// Indicates if the aim is enabled.
        /// </summary>
        private bool EnabledField;

        /// <summary>
        /// Indicates if the aim is enabled.
        /// </summary>
        public bool Enabled
        {
            get { return EnabledField; }
        }
        
        /// <summary>
		/// The position of the aim.
		/// </summary>
		private Vector2 PositionField;

		/// <summary>
		/// The position of the aim.
		/// </summary>
		public Vector2 Position
		{
            get { return PositionField; }
		}
        
        /// <summary>
        /// Indicates if the aim is being moved.
        /// </summary>
        private bool AimingField;
        
        /// <summary>
        /// Indicates if the aim is being moved.
        /// </summary>
        public bool Aiming
        {
            get { return AimingField; }
        }

        /// <summary>
        /// The limit distance that the aim can be away from the unit.
        /// </summary>
        private float LimitField;

        /// <summary>
        /// The limit distance that the aim can be away from the unit.
        /// </summary>
        public float Limit
        {
            get { return LimitField; }
        }

        /// <summary>
        /// The unit targeted by the aim. Null when the unit is over anything.
        /// </summary>
        private Unit Target;

        /// <summary>
        /// The texture of the aim when its over nothing.
        /// </summary>
        private Texture2D AimNothing;

        /// <summary>
        /// The texture of the aim when its over some ally.
        /// </summary>
        private Texture2D AimAlly;

        /// <summary>
        /// The texture of the aim when its over some enemy.
        /// </summary>
        private Texture2D AimEnemy;

        /// <summary>
        /// The texture that will be used to draw the aim.
        /// </summary>
        private Texture2D TextureToDraw;

        #endregion

        #region Event

        /// <summary>
        /// The method template who will used to handle the Aimed event.
        /// </summary>
        /// <param name="target">The unit targeted by the aim. Null when the aim is over nothing.</param>
        public delegate void AimedEventHandler(Unit target);

        /// <summary>
        /// The Aimed event.
        /// </summary>
        public event AimedEventHandler Aimed;

        #endregion

        #region Constructor

        /// <summary>
		/// Constructor of class.
		/// </summary>
        /// <param name="unit">The unit who will be the owner of the aim.</param>
        public AimMenu(Unit unit)
        {
            this.UnitField = unit;

            this.PositionField = new Vector2(unit.Position.X + unit.Texture.Width / 2, unit.Position.Y + unit.Texture.Height / 8);

            this.AimingField = false;
            this.EnabledField = false;

            this.AimAlly = TextureManager.Instance.Sprites.Menu.AimAlly;
            this.AimEnemy = TextureManager.Instance.Sprites.Menu.AimEnemy;
            this.AimNothing = TextureManager.Instance.Sprites.Menu.AimNothing;
            this.TextureToDraw = TextureManager.Instance.Sprites.Menu.AimAlly;
		}

		#endregion

		#region Methods

        /// <summary>
        /// Activate the input handling.
        /// </summary>
        /// <param name="limit">The limit distance that the aim can be away from the unit.</param>
        public void Activate(float limit)
        {
            this.EnabledField = true;
            this.LimitField = limit;

            InputManager.Instance.CursorDown += new EventHandler<CursorDownArgs>(CursorDown_Handler);
            InputManager.Instance.CursorUpdate += new EventHandler<CursorUpdateArgs>(CursorUpdate_Handler);
            InputManager.Instance.CursorUp += new EventHandler<CursorUpArgs>(CursorUp_Handler);
        }

        /// <summary>
        /// Deactivate the input handling.
        /// </summary>
        public void Deactivate()
        {
            this.EnabledField = false;
            this.LimitField = 0;

            InputManager.Instance.CursorDown -= new EventHandler<CursorDownArgs>(CursorDown_Handler);
            InputManager.Instance.CursorUpdate -= new EventHandler<CursorUpdateArgs>(CursorUpdate_Handler);
            InputManager.Instance.CursorUp -= new EventHandler<CursorUpArgs>(CursorUp_Handler);
        }

        /// <summary>
        /// Reset the logic properties of the aim.
        /// </summary>
        public void Reset()
        {
            this.PositionField = new Vector2(this.Unit.Position.X + this.Unit.Texture.Width / 2, this.Unit.Position.Y + this.Unit.Texture.Height / 8);
        }

        /// <summary>
        /// Draws the aim.
        /// </summary>
        /// <param name="spriteBatchManager">SpriteBatchManager used to draw.</param>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(SpriteManager spriteBatchManager, GameTime gameTime)
		{
            // If the aim is enabled.
            if (this.Enabled)
            {
                // Updates the aim ten times per second.
                if (gameTime.TotalGameTime.Milliseconds % 100 == 0)
                {
                    // Get all the units of the game.
                    List<Unit> units = new List<Unit>();
                    units.AddRange(this.Unit.Player.Units);
                    units.AddRange(this.Unit.Player.Enemy.Units);

                    bool isOverSomething = false;

                    // Check if the aim is over some unit.
                    for (int index = 0; index < units.Count; index++)
                    {
                        // Get the center position of the unit.
                        Vector2 unitPosition = new Vector2(units[index].Position.X + units[index].Texture.Width / 2, units[index].Position.Y + units[index].Texture.Height / 8);

                        // Calculates the distance between the unit and the aim.
                        if (Vector2.Distance(this.Position, unitPosition) < 40)
                        {
                            isOverSomething = true;
                            
                            // Determine if the player owner of the unit is the enemy;
                            if (units[index].Player != this.Unit.Player)
                            {
                                this.TextureToDraw = this.AimEnemy;
                            }
                            else
                            {
                                this.TextureToDraw = this.AimAlly;
                            }

                            this.Target = units[index];
                        }
                    }

                    if (!isOverSomething)
                    {
                        this.TextureToDraw = this.AimNothing;
                        this.Target = null;
                    }
                }

                Vector2 position = new Vector2(this.Position.X - this.AimAlly.Width / 2, this.Position.Y - this.AimAlly.Height / 2);
                spriteBatchManager.Draw(this.TextureToDraw, position, Color.White, 60);
            }
		}

		#endregion

        #region Input Handling

        /// <summary>
        /// Handles the CursorDown event.
        /// </summary>
        /// <param name="sender">Always null.</param>
        /// <param name="e">Data of event.</param>
        private void CursorDown_Handler(object sender, CursorDownArgs e)
        {
            // Handles the event only if the aim is enabled.
            if (this.Enabled)
            {
                // If the touch was in the area of the aim
                if ((e.Position.X < (this.Position.X + this.AimAlly.Width / 2) && e.Position.X > (this.Position.X - this.AimAlly.Width / 2)) &&
                    (e.Position.Y < (this.Position.Y + this.AimAlly.Height / 2) && e.Position.Y > (this.Position.Y - this.AimAlly.Height / 2)))
                {
                    this.AimingField = true;
                }
            }
        }

        /// <summary>
        /// Handles the CursorUpdate event.
        /// </summary>
        /// <param name="sender">Always null.</param>
        /// <param name="e">Data of event.</param>
        private void CursorUpdate_Handler(object sender, CursorUpdateArgs e)
        {
            // If the aim is enabled and aiming.
            if (this.Enabled && this.Aiming)
            {
                // If the aim is inside the area
                Vector2 areaPosition = new Vector2(this.Unit.Position.X + this.Unit.Texture.Width / 2, this.Unit.Position.Y + this.Unit.Texture.Height / 4);
                if (Vector2.Distance(e.Position, areaPosition) < (this.Limit / 2) - (this.AimAlly.Width / 2))
                {
                    // Updates the position of the aim.
                    this.PositionField = e.Position;
                }
            }
        }

        /// <summary>
        /// Handles the CursorUp event.
        /// </summary>
        /// <param name="sender">Always null.</param>
        /// <param name="e">Data of event.</param>
        private void CursorUp_Handler(object sender, CursorUpArgs e)
        {
            // If the aim is enabled and aiming.
            if (this.Enabled && this.Aiming)
            {
                // End the aiming and set the aimed true.
                this.AimingField = false;

                // Dispatch the Aimed event.
                if (this.Aimed != null)
                {
                    this.Aimed(this.Target);
                }

                // Unregister the event handler.
                this.Deactivate();
            }
        }

        #endregion
    }
}