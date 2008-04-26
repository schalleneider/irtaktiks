using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace IRTaktiks.Components.Scenario
{
    /// <summary>
    /// Representation of an area.
    /// </summary>
    public class Area
    {
        #region Properties

        /// <summary>
        /// The position of the area.
        /// </summary>
        private Vector2 PositionField;

        /// <summary>
        /// The position of the area.
        /// </summary>
        public Vector2 Position
        {
            get { return PositionField; }
        }

        /// <summary>
        /// The radius of the area.
        /// </summary>
        private float RadiusField;

        /// <summary>
        /// The radius of the area.
        /// </summary>
        public float Radius
        {
            get { return RadiusField; }
        }

        /// <summary>
        /// The color of the area.
        /// </summary>
        private Color ColorField;

        /// <summary>
        /// The color of the area.
        /// </summary>
        public Color Color
        {
            get { return ColorField; }
        }

        /// <summary>
        /// The world matrix of the area.
        /// </summary>
        public Matrix World
        {
            get
            {
                return Matrix.CreateScale(this.Radius) * Matrix.CreateTranslation(this.Position.X, this.Position.Y, 0);
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="position">The position of the area.</param>
        /// <param name="radius">The radius of the area.</param>
        /// <param name="color">The color of the area.</param>
        public Area(Vector2 position, float radius, Color color)
        {
            this.PositionField = position;
            this.RadiusField = radius;
            this.ColorField = color;
        }	

        #endregion
    }
}
