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
using IRTaktiks.Components.Logic;

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

        /// <summary>
        /// Indicates if the unit is moving.
        /// </summary>
        private bool Moving;

        /// <summary>
        /// Indicates if the mover is ready to orient.
        /// </summary>
        private bool ReadyToOrient;

        /// <summary>
        /// Indicates if the unit is orienting.
        /// </summary>
        private bool Orienting;
        
        /// <summary>
        /// The cursor identifier to track the CursorUpdate and CursorUp event.
        /// </summary>
        private int CursorIdentifier;

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
            
            this.EnabledField = false;
            
            this.Moving = false;
            this.Orienting = false;
            this.ReadyToOrient = false;
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

            this.Moving = false;
            this.Orienting = false;
            this.ReadyToOrient = false;

            // Register the mover to listen the input events.
            InputManager.Instance.CursorDown += new EventHandler<CursorDownArgs>(CursorDown_Handler);
            InputManager.Instance.CursorUpdate += new EventHandler<CursorUpdateArgs>(CursorUpdate_Handler);
            InputManager.Instance.CursorUp += new EventHandler<CursorUpArgs>(CursorUp_Handler);

            // Create the area.
            Vector2 areaPosition = new Vector2(this.Unit.Position.X + this.Unit.Texture.Width / 2, this.Unit.Position.Y + this.Unit.Texture.Height / 4);
            this.AreaField = new Area(areaPosition, limit, this.Unit.Player.PlayerIndex == PlayerIndex.One ? AreaManager.PlayerOneMovementAreaColor : AreaManager.PlayerTwoMovementAreaColor);
        }

        /// <summary>
        /// Deactivate the mover.
        /// </summary>
        public void Deactivate()
        {
            // Disables the mover.
            this.EnabledField = false;
            this.LimitField = 0;

            this.Moving = false;
            this.Orienting = false;
            this.ReadyToOrient = false;

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
        /// <param name="spriteBatchManager">SpriteBatchManager used to draw sprites.</param>
        /// <param name="areaManager">AreaManager used to draw areas.</param>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(SpriteManager spriteBatchManager, AreaManager areaManager, GameTime gameTime)
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
                if (this.ReadyToOrient)
                {
                    // Touch was inside of the area to orient.
                    if (Vector2.Distance(e.Position, this.Area.Position) < this.Area.Radius)
                    {
                        this.ReadyToOrient = false;
                        this.Orienting = true;
                        
                        // Store the cursor identifier to track the CursorUpdate and CursorUp event.
                        this.CursorIdentifier = e.Identifier;
                    }
                }
                else
                {
                    // Touch was inside of the X area of the unit.
                    if (e.Position.X < (this.Unit.Position.X + this.Unit.Texture.Width) && e.Position.X > this.Unit.Position.X)
                    {
                        // Touch was inside of the Y area of the unit.
                        if (e.Position.Y < (this.Unit.Position.Y + this.Unit.Texture.Height / 4) && e.Position.Y > this.Unit.Position.Y)
                        {
                            this.Moving = true;
                            
                            // Store the cursor identifier to track the CursorUpdate and CursorUp event.
                            this.CursorIdentifier = e.Identifier;
                        }
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
            // Check the cursor identifier 
            if (this.CursorIdentifier == e.Identifier)
            {
                if (this.Enabled)
                {
                    if (this.Moving)
                    {
                        // If the foot of the unit is inside the area.
                        Vector2 footPosition = new Vector2(e.Position.X, e.Position.Y + this.Unit.Texture.Height / 8);
                        if (Vector2.Distance(footPosition, this.Area.Position) < this.Limit - 10)
                        {
                            // Updates the position of the unit.
                            this.Unit.Position = new Vector2(e.Position.X - this.Unit.Texture.Width / 2, e.Position.Y - this.Unit.Texture.Height / 8);
                        }
                    }
                    else if (this.Orienting)
                    {
                        // Touch was inside of the area to orient.
                        if (Vector2.Distance(e.Position, this.Area.Position) < this.Area.Radius)
                        {
                            // Calculates the angle between the unit and the touch.
                            Vector2 distance = new Vector2(e.Position.X - this.Area.Position.X, e.Position.Y - this.Area.Position.Y);
                            double angle = MathHelper.ToDegrees((float)Math.Atan2(distance.X, distance.Y));

                            // Unit looking to down.
                            if (angle >= -45 && angle <= 45)
                            {
                                this.Unit.Orientation = Orientation.Down;
                            }

                            // Unit looking to right.
                            else if (angle >= 45 && angle <= 135)
                            {
                                this.Unit.Orientation = Orientation.Right;
                            }

                            // Unit looking to left.
                            else if (angle >= -135 && angle <= -45)
                            {
                                this.Unit.Orientation = Orientation.Left;
                            }

                            // Unit looking to up.
                            else
                            {
                                this.Unit.Orientation = Orientation.Up;
                            }
                        }
                    }
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
            // Check the cursor identifier 
            if (this.CursorIdentifier == e.Identifier)
            {
                if (this.Enabled)
                {
                    if (this.Moving)
                    {
                        // End the moving of the unit.
                        this.Moving = false;

                        // Start the orienting of the unit.
                        this.ReadyToOrient = true;

                        Vector2 areaPosition = new Vector2(this.Unit.Position.X + this.Unit.Texture.Width / 2, this.Unit.Position.Y + this.Unit.Texture.Height / 4);
                        this.AreaField = new Area(areaPosition, 150, this.Unit.Player.PlayerIndex == PlayerIndex.One ? AreaManager.PlayerOneOrientationAreaColor : AreaManager.PlayerTwoOrientationAreaColor);
                    }
                    else if (this.Orienting)
                    {
                        // Dispatch the Moved event.
                        if (this.Moved != null)
                        {
                            this.Moved();
                        }

                        // Deactivate the mover.
                        this.Deactivate();
                    }
                }
            }
        }

        #endregion
    }
}