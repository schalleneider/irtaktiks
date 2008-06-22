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
        #region Constants

        /// <summary>
        /// The scaled factor for X.
        /// </summary>
        public const float ScaledFactorX = +1.00f;

        /// <summary>
        /// The scaled factor for Y.
        /// </summary>
        public const float ScaledFactorY = -0.85f;

        /// <summary>
        /// The scaled factor for the radius.
        /// </summary>
        public const float ScaledFactorRadius = +1.00f;

        #endregion

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
        /// The scaled position of the area.
        /// </summary>
        private Vector2 ScaledPositionField;
        
        /// <summary>
        /// The scaled position of the area.
        /// </summary>
        public Vector2 ScaledPosition
        {
            get { return ScaledPositionField; }
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
        /// The scaled radius of the area.
        /// </summary>
        private float ScaledRadiusField;
        
        /// <summary>
        /// The scaled radius of the area.
        /// </summary>
        public float ScaledRadius
        {
            get { return ScaledRadiusField; }
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
                return Matrix.CreateScale(this.ScaledRadius) * Matrix.CreateTranslation(this.ScaledPosition.X, this.ScaledPosition.Y, 0);
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
        public Area(Vector2 position, float diameter, Color color)
        {
            this.PositionField = position;
            this.RadiusField = diameter;
            this.ColorField = color;

            this.Scale();
        }	

        #endregion

        #region Methods

        /// <summary>
        /// Calculates the scaled position and radius, basead in the min and max values of x an y.
        /// </summary>
        private void Scale()
        {
            float scaledX = Area.ScaledFactorX * (2 * (this.Position.X / (float)IRTSettings.Default.Width) - 1);
            float scaledY = Area.ScaledFactorY * (2 * (this.Position.Y / (float)IRTSettings.Default.Height) - 1);
            float scaledRadius = Area.ScaledFactorRadius * (2 * this.Radius / (float)IRTSettings.Default.Width);

            this.ScaledPositionField = new Vector2(scaledX, scaledY);
            this.ScaledRadiusField = scaledRadius;
        }

        #endregion
    }
}
