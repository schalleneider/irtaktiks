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
    /// Component that represents a particle effect.
    /// </summary>
    public class Damage
    {
        #region Effect Type

        /// <summary>
        /// Representation of a type of damages.
        /// </summary>
        public enum DamageType
        {
            /// <summary>
            /// Beneful damage.
            /// </summary>
            Benefit,

            /// <summary>
            /// Harmful damage.
            /// </summary>
            Harmful,
        }

        #endregion

        #region Properties

        /// <summary>
        /// The value of the damage.
        /// </summary>
        private int ValueField;

        /// <summary>
        /// The value of the damage.
        /// </summary>
	    public int Value
	    {
		    get { return ValueField;}
	    }

        /// <summary>
        /// The position of the damage.
        /// </summary>
        private Vector2 PositionField;

        /// <summary>
        /// The position of the damage.
        /// </summary>
        public Vector2 Position
        {
            get { return PositionField; }
        }

        /// <summary>
        /// The type of the damage.
        /// </summary>
        private DamageType TypeField;

        /// <summary>
        /// The type of the damage.
        /// </summary>
        public DamageType Type
        {
            get { return TypeField; }
        }

        /// <summary>
        /// The color of the damage.
        /// </summary>
        private Color ColorField;

        /// <summary>
        /// The color of the damage.
        /// </summary>
        public Color Color
        {
            get { return ColorField; }
        }
        
        /// <summary>
        /// Indicates that the effect is alive.
        /// </summary>
        private bool AliveField;

        /// <summary>
        /// Indicates that the effect is alive.
        /// </summary>
        public bool Alive
        {
            get { return AliveField; }
        }

        /// <summary>
        /// The actual life of the damage.
        /// </summary>
        private float Life;

        /// <summary>
        /// The life increment at each update.
        /// </summary>
        private float LifeIncrement;

        /// <summary>
        /// The life time of the damage, before its destruction
        /// </summary>
        private float LifeTime;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="value">The value of the damage.</param>
        /// <param name="position">The position of the damage.</param>
        /// <param name="type">The type of the damage.</param>
        /// <param name="lifeIncrement">The life increment at each update.</param>
        /// <param name="lifeTime">The life time of the damage, before its destruction.</param>
        public Damage(int value, Vector2 position, DamageType type, float lifeIncrement, float lifeTime)
        {
            this.ValueField = value;
            this.PositionField = position;
            this.TypeField = type;
            this.AliveField = true;

            this.LifeIncrement = lifeIncrement;
            this.Life = 0;
            this.LifeTime = lifeTime;

            switch (this.Type)
            {
                case DamageType.Benefit:
                    this.ColorField = new Color(new Vector4(0.0f, 1.0f, 0.0f, 1.0f));
                    break;

                case DamageType.Harmful:
                    this.ColorField = new Color(new Vector4(1.0f, 0.0f, 0.0f, 1.0f));
                    break;
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Update the damage position.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            if (this.Life > this.LifeTime)
            {
                this.AliveField = false;
            }
            else
            {
                this.Life += this.LifeIncrement;
                this.ColorField = new Color(new Vector4(this.Color.ToVector3(), 1.0f - (this.Life / this.LifeTime)));
            }
        }

        #endregion
    }
}
