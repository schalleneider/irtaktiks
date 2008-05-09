using System;
using System.Collections.Generic;
using System.Text;

namespace IRTaktiks.Components.Logic
{
    /// <summary>
    /// The logic representation of the unit's atributes.
    /// </summary>
    public class Attributes
    {
        #region Basic Attributes

        /// <summary>
        /// The level of the unit.
        /// </summary>
        private float LevelField;

        /// <summary>
        /// The level of the unit.
        /// </summary>
        public float Level
        {
            get { return LevelField; }
        }

        /// <summary>
        /// The job of the unit.
        /// </summary>
        private Job JobField;

        /// <summary>
        /// The job of the unit.
        /// </summary>
        public Job Job
        {
            get { return JobField; }
        }

        /// <summary>
        /// The element of the unit.
        /// </summary>
        private Element ElementField;

        /// <summary>
        /// The element of the unit.
        /// </summary>
        public Element Element
        {
            get { return ElementField; }
        }

        /// <summary>
        /// The strength of the unit.
        /// </summary>
        private float StrengthField;

        /// <summary>
        /// The strength of the unit.
        /// </summary>
        public float Strength
        {
            get { return StrengthField; }
        }

        /// <summary>
        /// The agility of the unit.
        /// </summary>
        private float AgilityField;

        /// <summary>
        /// The agility of the unit.
        /// </summary>
        public float Agility
        {
            get { return AgilityField; }
        }

        /// <summary>
        /// The vitality of the unit.
        /// </summary>
        private float VitalityField;

        /// <summary>
        /// The vitality of the unit.
        /// </summary>
        public float Vitality
        {
            get { return VitalityField; }
        }

        /// <summary>
        /// The magic of the unit.
        /// </summary>
        private float InteligenceField;

        /// <summary>
        /// The magic of the unit.
        /// </summary>
        public float Inteligence
        {
            get { return InteligenceField; }
        }

        /// <summary>
        /// The dexteriry of unit.
        /// </summary>
        private float DexterityField;

        /// <summary>
        /// The dexteriry of unit.
        /// </summary>
        public float Dexterity
        {
            get { return DexterityField; }
        }

        #endregion

        #region Calculated Attributes

        /// <summary>
        /// The maximum life value of the unit.
        /// (1 + (VIT / 80)) * (20 + (5 * LVL) + (@ * ((1 + LVL) * (LVL / 2.5)))))
        /// </summary>
        public int MaximumLife
        {
            get
            {
                float jobHP = 0.85f;
                return Convert.ToInt32((1 + (this.Vitality / 80)) * (20 + (5 * this.Level) + (jobHP * ((1 + this.Level) * (this.Level / 2.5)))));
            }
        }

        /// <summary>
        /// The maximum mana value of the unit.
        /// (1 + (INT / 50)) * (5 + (@ * LVL * 10)))
        /// </summary>
        public int MaximumMana
        {
            get
            {
                float jobMP = 0.70f;
                return Convert.ToInt32((1 + (this.Inteligence / 50)) * (5 + (jobMP * this.Level * 10))); ;
            }
        }

        /// <summary>
        /// The attack value of the unit.
        /// </summary>
        public int Attack
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// The defense value of the unit.
        /// </summary>
        public int Defense
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// The magic attack value of the unit.
        /// </summary>
        public int MagicAttack
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// The magic defense value of the unit.
        /// </summary>
        public int MagicDefense
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// The flee value of the unit.
        /// </summary>
        public int Flee
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// The hit value of the unit.
        /// </summary>
        public int Hit
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// The range value of the unit.
        /// </summary>
        public int Range
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// The delay value of the unit.
        /// </summary>
        public int Delay
        {
            get
            {
                return 0;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="level">The level of the unit.</param>
        /// <param name="job">The job of the unit.</param>
        /// <param name="element">The element of the unit.</param>
        /// <param name="strength">The strenght value.</param>
        /// <param name="agility">The agility value.</param>
        /// <param name="vitality">The vitality value.</param>
        /// <param name="magic">The magic value.</param>
        /// <param name="dexterity">The dexterity value.</param>
        public Attributes(int level, Job job, Element element, int strength, int agility, int vitality, int magic, int dexterity)
        {
            this.LevelField = level;
            this.JobField = job;
            this.ElementField = element;

            this.StrengthField = strength;
            this.AgilityField = agility;
            this.VitalityField = vitality;
            this.InteligenceField = magic;
            this.DexterityField = dexterity;
        }

        #endregion

        #region Methods

        #region Areas

        /// <summary>
        /// Calculates the area that a magic can be spelled.
        /// Based on dexterity and magic.
        /// </summary>
        /// <returns>The area.</returns>
        public double CalculateMagicArea()
        {
            return 100 + 1.5 * (double)this.Dexterity + 1.0 * (double)this.Inteligence;
        }

        #endregion

        #region Factors

        /// <summary>
        /// Calculates the time factor, used on time update.
        /// Based on agility.
        /// </summary>
        /// <returns>The time factor.</returns>
        public double CalculateTimeFactor()
        {
            return 0.001 + 0.001 * ((double)this.Agility / 99);
        }

        /// <summary>
        /// Calculates the magic attack factor.
        /// </summary>
        /// <returns>The magic attack factor.</returns>
        public double CalculateMagicAttackFactor()
        {
            return 3.25 * (double)this.Inteligence;
        }

        /// <summary>
        /// Calculates the magic defense factor.
        /// </summary>
        /// <returns>The magic defense factor.</returns>
        public double CalculateMagicDefenseFactor()
        {
            return 3.05 * (double)this.Inteligence;
        }

        #endregion

        #endregion
    }
}
