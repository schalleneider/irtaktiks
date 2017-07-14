using System;
using System.Collections.Generic;
using System.Text;

namespace IRTaktiks.Input.EventArgs
{
    /// <summary>
    /// ObjectRemoved event data.
    /// </summary>
    public class ObjectRemovedArgs : System.EventArgs
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

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
		/// <param name="identifier">Fiducial identifier.</param>
        public ObjectRemovedArgs(int identifier)
        {
            this.identifierField = identifier;
        }

        #endregion
    }
}
