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

        #endregion

        #region Logic Properties
        
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

        #endregion

        #region Event

        /// <summary>
        /// The method template who will used to handle the Aimed event.
        /// </summary>
        /// <param name="position"></param>
        public delegate void AimedEventHandler(Vector2 position);

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
		}

		#endregion

		#region Methods

        /// <summary>
        /// Activate the input handling.
        /// </summary>
        public void Activate()
        {
            this.EnabledField = true;

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

            InputManager.Instance.CursorDown -= new EventHandler<CursorDownArgs>(CursorDown_Handler);
            InputManager.Instance.CursorUpdate -= new EventHandler<CursorUpdateArgs>(CursorUpdate_Handler);
            InputManager.Instance.CursorUp -= new EventHandler<CursorUpArgs>(CursorUp_Handler);
        }

        /// <summary>
        /// Draws the aim.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch that will be used to draw the textures.</param>
        public void Draw(SpriteBatch spriteBatch)
		{
            // If the aim is enabled.
            if (this.Enabled)
            {
                Texture2D textureToDraw = this.AimNothing;

                for (int index = 0; index < this.Unit.Player.Units.Count; index++)
                {
                    Vector2 unitPosition = this.Unit.Player.Units[index].Position;

                    //if (
                }

                Vector2 position = new Vector2(this.Position.X - this.AimAlly.Width / 2, this.Position.Y - this.AimAlly.Height / 2);
                spriteBatch.Draw(this.AimNothing, position, Color.White);
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
                // Updates the position of the aim.
                this.PositionField = e.Position;
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
                    this.Aimed(this.Position);
                }

                // Unregister the event handler.
                this.Deactivate();
            }
        }

        #endregion
    }
}