using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IRTaktiks.Components.Playable;
using IRTaktiks.Components.Scenario;
using IRTaktiks.Input.EventArgs;
using IRTaktiks.Input;
using IRTaktiks.Components.Manager;

namespace IRTaktiks.Components.Menu
{
	/// <summary>
	/// Representation of the area that the unit can move.
	/// </summary>
	public class Mover
	{
		#region Properties

        /// <summary>
        /// The unit who will move.
        /// </summary>
        private Unit UnitField;

        /// <summary>
        /// The unit who will move.
        /// </summary>
        public Unit Unit
        {
            get { return UnitField; }
        }

        /// <summary>
        /// The area that the unit can move.
        /// </summary>
        private Area AreaField;

        /// <summary>
        /// The area that the unit can move.
        /// </summary>
        public Area Area
        {
            get { return AreaField; }
        }
                        
        /// <summary>
        /// Indicates if the mover is enabled.
        /// </summary>
        private bool EnabledField;

        /// <summary>
        /// Indicates if the mover is enabled.
        /// </summary>
        public bool Enabled
        {
            get { return EnabledField; }
        }
        
        /// <summary>
		/// The central position of the mover.
		/// </summary>
		private Vector2 PositionField;

		/// <summary>
		/// The central position of the mover.
		/// </summary>
		public Vector2 Position
		{
            get { return PositionField; }
		}
        
        /// <summary>
        /// Indicates if the unit is moving.
        /// </summary>
        private bool MovingField;
        
        /// <summary>
        /// Indicates if the unit is moving.
        /// </summary>
        public bool Moving
        {
            get { return MovingField; }
        }

        /// <summary>
        /// The limit distance that the unit can move.
        /// </summary>
        private float LimitField;

        /// <summary>
        /// The limit distance that the unit can move.
        /// </summary>
        public float Limit
        {
            get { return LimitField; }
        }

        #endregion

        #region Event

        /// <summary>
        /// The method template who will used to handle the Moved event.
        /// </summary>
        public delegate void MovedEventHandler();

        /// <summary>
        /// The Moved event.
        /// </summary>
        public event MovedEventHandler Moved;

        #endregion

        #region Constructor

        /// <summary>
		/// Constructor of class.
		/// </summary>
        /// <param name="unit">The unit who will move.</param>
        public Mover(Unit unit)
        {
            this.UnitField = unit;

            this.PositionField = new Vector2(this.Unit.Position.X + this.Unit.Texture.Width / 2, this.Unit.Position.Y + this.Unit.Texture.Height / 4);
            
            this.MovingField = false;
            this.EnabledField = false;
		}

		#endregion

		#region Methods

        /// <summary>
        /// Activate the mover.
        /// </summary>
        /// <param name="limit">The limit distance that the unit can move.</param>
        public void Activate(float limit)
        {
            // Enables the mover with the specified limit.
            this.EnabledField = true;
            this.LimitField = limit;

            // Register the mover to listen the input events.
            InputManager.Instance.CursorDown += new EventHandler<CursorDownArgs>(CursorDown_Handler);
            InputManager.Instance.CursorUpdate += new EventHandler<CursorUpdateArgs>(CursorUpdate_Handler);
            InputManager.Instance.CursorUp += new EventHandler<CursorUpArgs>(CursorUp_Handler);

            // Create the area.
            Vector2 areaPosition = new Vector2(this.Unit.Position.X + this.Unit.Texture.Width / 2, this.Unit.Position.Y + this.Unit.Texture.Height / 4);
            this.AreaField = new Area(areaPosition, limit, this.Unit.Player.PlayerIndex == PlayerIndex.One ? AreaManager.PlayerOneAreaColor : AreaManager.PlayerTwoAreaColor);
        }

        /// <summary>
        /// Deactivate the mover.
        /// </summary>
        public void Deactivate()
        {
            // Disables the mover.
            this.EnabledField = false;
            this.LimitField = 0;

            // Unregister the mover to listen the input events.
            InputManager.Instance.CursorDown -= new EventHandler<CursorDownArgs>(CursorDown_Handler);
            InputManager.Instance.CursorUpdate -= new EventHandler<CursorUpdateArgs>(CursorUpdate_Handler);
            InputManager.Instance.CursorUp -= new EventHandler<CursorUpArgs>(CursorUp_Handler);

            // Reset the position of the mover.
            this.PositionField = new Vector2(this.Unit.Position.X + this.Unit.Texture.Width / 2, this.Unit.Position.Y + this.Unit.Texture.Height / 4);

            // Destroy the area.
            this.AreaField = null;
        }

        /// <summary>
        /// Draw the mover's area.
        /// </summary>
        /// <param name="areaManager">AreaManager used to draw areas.</param>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(AreaManager areaManager, GameTime gameTime)
        {
            // If the mover is enabled.
            if (this.Enabled)
            {
                // Draw the area.
                areaManager.Draw(this.Area);
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
            // Handles the event only if the mover is enabled.
            if (this.Enabled)
            {
                // Touch was inside of the X area of the unit.
                if (e.Position.X < (this.Unit.Position.X + this.Unit.Texture.Width) && e.Position.X > this.Unit.Position.X)
                {
                    // Touch was inside of the Y area of the unit.
                    if (e.Position.Y < (this.Unit.Position.Y + this.Unit.Texture.Height / 4) && e.Position.Y > this.Unit.Position.Y)
                    {
                        this.MovingField = true;
                    }
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
            // If the mover is enabled and the unit is moving.
            if (this.Enabled && this.Moving)
            {
                // If the unit is inside the area.
                if (Vector2.Distance(e.Position, this.Area.Position) < this.Limit)
                {
                    // Updates the position of the aim.
                    this.Unit.Position = e.Position;
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
            // If the mover is enabled and the unit is moving.
            if (this.Enabled && this.Moving)
            {
                // End the moving of the unit.
                this.MovingField = false;

                // Dispatch the Aimed event.
                if (this.Moved != null)
                {
                    this.Moved();
                }

                // Deactivate the mover.
                this.Deactivate();
            }
        }

        #endregion
    }
}