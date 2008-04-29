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
using IRTaktiks.Components.Scenario;
using IRTaktiks.Input.EventArgs;
using IRTaktiks.Input;

namespace IRTaktiks.Components.Menu
{
	/// <summary>
	/// Representation of move area of the unit.
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
        /// Indicates if the area is enabled.
        /// </summary>
        private bool EnabledField;

        /// <summary>
        /// Indicates if the area is enabled.
        /// </summary>
        public bool Enabled
        {
            get { return EnabledField; }
        }
        
        /// <summary>
		/// The central position of the area that the unit can move.
		/// </summary>
		private Vector2 PositionField;

		/// <summary>
		/// The central position of the area that the unit can move.
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
        /// Activate the input handling.
        /// </summary>
        /// <param name="limit">The limit distance that the unit can move.</param>
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
        /// Reset the logic properties of the mover.
        /// </summary>
        public void Reset()
        {
            this.PositionField = new Vector2(this.Unit.Position.X + this.Unit.Texture.Width / 2, this.Unit.Position.Y + this.Unit.Texture.Height / 4);
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
        }

        /// <summary>
        /// Handles the CursorUpdate event.
        /// </summary>
        /// <param name="sender">Always null.</param>
        /// <param name="e">Data of event.</param>
        private void CursorUpdate_Handler(object sender, CursorUpdateArgs e)
        {
        }

        /// <summary>
        /// Handles the CursorUp event.
        /// </summary>
        /// <param name="sender">Always null.</param>
        /// <param name="e">Data of event.</param>
        private void CursorUp_Handler(object sender, CursorUpArgs e)
        {
        }

        #endregion
    }
}