using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace IRTaktiks.Input.EventArgs
{
    /// <summary>
	/// CursorUpdate event data.
    /// </summary>
    public class CursorUpdateArgs : System.EventArgs
    {
        #region Properties

		/// <summary>
		/// Id of the cursor created by a finger touch.
		/// </summary>
		private int identifierField;

		/// <summary>
		/// Id of the cursor created by a finger touch.
		/// </summary>
		public int Identifier
		{
			get { return this.identifierField; }
		}

		/// <summary>
		/// Center position of the finger.
		/// </summary>
		private Vector2 positionField;

		/// <summary>
		/// Center position of the finger.
		/// </summary>
		public Vector2 Position
		{
			get { return this.positionField; }
		}

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
		/// <param name="identifier">Id of the cursor created by a finger touch.</param>
		/// <param name="position">Center position of the finger.</param>
        public CursorUpdateArgs(int identifier, Vector2 position)
        {
            this.identifierField = identifier;
            this.positionField = position;
        }

        #endregion
    }
}
