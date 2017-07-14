using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace IRTaktiks.Input.EventArgs
{
    /// <summary>
	/// ObjectAdded event data.
    /// </summary>
    public class ObjectAddedArgs : System.EventArgs
    {
        #region Properties

		/// <summary>
		/// Fiducial identifier.
		/// </summary>
		private int identifierField;

		/// <summary>
		/// Fiducial identifier.
		/// </summary>
		public int Identifier
		{
			get { return this.identifierField; }
		}
		
		/// <summary>
        /// Center position of the fiducial.
        /// </summary>
        private Vector2 positionField;

        /// <summary>
		/// Center position of the fiducial.
        /// </summary>
        public Vector2 Position
        {
            get { return this.positionField; }
        }
		
		/// <summary>
        /// Orientation in radians.
        /// </summary>
        private float orientationField;

		/// <summary>
		/// Orientation in radians.
		/// </summary>
        public float Orientation
        {
            get { return this.orientationField; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
		/// <param name="identifier">Fiducial identifier.</param>
		/// <param name="position">Center position of the fiducial.</param>
		/// <param name="orientation">Orientation in radians.</param>
        public ObjectAddedArgs(int identifier, Vector2 position, float orientation)
        {
            this.identifierField = identifier;
            this.positionField = position;
            this.orientationField = orientation;
        }

        #endregion
    }
}
