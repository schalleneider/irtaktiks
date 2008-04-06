using System;
using System.Text;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using IRTaktiks.Input.EventArgs;

namespace IRTaktiks.Input
{
    /// <summary>
    /// Responsible by input events through the Tuio listener.
    /// </summary>
    public class InputManager
    {
        #region Singleton

        /// <summary>
        /// Class instance.
        /// </summary>
        private static InputManager InstanceField;

        /// <summary>
        /// Class instance.
        /// </summary>
        public static InputManager Instance
        {
            get { if (InstanceField == null) InstanceField = new InputManager(); return InstanceField; }
        }

        /// <summary>
        /// Private constructor.
        /// </summary>
        private InputManager()
        {
		}

        #endregion

		#region Properties

		#endregion
		
        #region Events
        
        /// <summary>
        /// Dispatched when a finger touches the table.
        /// </summary>
        public event EventHandler<CursorDownArgs> CursorDown;

        /// <summary>
        /// Dispatched when a finger still touching the table.
        /// </summary>
        public event EventHandler<CursorUpdateArgs> CursorUpdate;

        /// <summary>
        /// Dispatched when a finger touching the table leaves.
        /// </summary>
        public event EventHandler<CursorUpArgs> CursorUp;

        #endregion

        #region RaiseEvents

		/// <summary>
		/// Queue the PressCursor event for dispatch.
		/// </summary>
		/// <param name="identifier">Id of the cursor created by a finger touch.</param>
		/// <param name="position">Center position of the finger.</param>
        public void RaiseCursorDown(int identifier, Vector2 position)
        {
            ThreadPool.QueueUserWorkItem(PressCursor, new CursorDownArgs(identifier, position));
        }
        
        /// <summary>
        /// Queue the UpdateCursor event for dispatch.
        /// </summary>
        /// <param name="identifier">Id of the cursor created by a finger touch.</param>
        /// <param name="position">Center position of the finger.</param>
        public void RaiseCursorUpdate(int identifier, Vector2 position)
        {
            ThreadPool.QueueUserWorkItem(UpdateCursor, new CursorUpdateArgs(identifier, position));
        }

		/// <summary>
		/// Queue the ReleaseCursor event for dispatch.
		/// </summary>
		/// <param name="identifier">Id of the cursor created by a finger touch.</param>
		/// <param name="position">Center position of the finger.</param>
        public void RaiseCursorUp(int identifier, Vector2 position)
        {
            ThreadPool.QueueUserWorkItem(ReleaseCursor, new CursorUpArgs(identifier, position));
        }

        #endregion

        #region Event Dispatcher

		/// <summary>
		/// Dispatch the CursorDown event.
		/// </summary>
		/// <param name="data">Event data.</param>
        private void PressCursor(object data)
        {
            if (this.CursorDown != null)
                this.CursorDown(null, data as CursorDownArgs);
        }

        /// <summary>
        /// Dispatch the CursorDown event.
        /// </summary>
        /// <param name="data">Event data.</param>
        private void UpdateCursor(object data)
        {
            if (this.CursorUpdate != null)
                this.CursorUpdate(null, data as CursorUpdateArgs);
        }

		/// <summary>
		/// Dispatch the ReleaseCursor event.
		/// </summary>
		/// <param name="data">Event data.</param>
        private void ReleaseCursor(object data)
        {
            if (this.CursorUp != null)
                this.CursorUp(null, data as CursorUpArgs);
        }

        #endregion
    }
}