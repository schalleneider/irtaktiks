using System;
using System.Collections.Generic;
using System.Text;

namespace IRTaktiks.Components.Logic
{
    /// <summary>
    /// The logic representation of the unit's atributes.
    /// </summary>
    public class UnitAttributes
    {
        #region Constants

        /// <summary>
        /// The minimum value for an attribute.
        /// </summary>
        public const int Minimum = 1;

        /// <summary>
        /// The maximum value for an attribute.
        /// </summary>
        public const int Maximum = 99;

        #endregion

        #region Properties

        /// <summary>
        /// The strength of the unit. Influences the attack power.
        /// </summary>
        private int StrengthField;

        /// <summary>
        /// The strength of the unit. Influences the attack power.
        /// </summary>
        public int Strength
        {
            get { return StrengthField; }
        }

        /// <summary>
        /// The agility of the unit. Influences the time.
        /// </summary>
        private int AgilityField;

        /// <summary>
        /// The agility of the unit. Influences the time.
        /// </summary>
        public int Agility
        {
            get { return AgilityField; }
        }

        /// <summary>
        /// The vitality of the unit. Influences the deffense.
        /// </summary>
        private int VitalityField;
        
        /// <summary>
        /// The vitality of the unit. Influences the deffense.
        /// </summary>
        public int Vitality
        {
            get { return VitalityField; }
        }

        /// <summary>
        /// The magic of the unit. Influences the magic attack and deffense.
        /// </summary>
        private int MagicField;

        /// <summary>
        /// The magic of the unit. Influences the magic attack and deffense.
        /// </summary>
        public int Magic
        {
            get { return MagicField; }
        }

        /// <summary>
        /// The dexteriry of unit. Influences the range of attacks.
        /// </summary>
        private int DexterityField;

        /// <summary>
        /// The dexteriry of unit. Influences the range of attacks.
        /// </summary>
        public int Dexterity
        {
            get { return DexterityField; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="strength">The strenght value.</param>
        /// <param name="agility">The agility value.</param>
        /// <param name="vitality">The vitality value.</param>
        /// <param name="magic">The magic value.</param>
        /// <param name="dexterity">The dexterity value.</param>
        public UnitAttributes(int strength, int agility, int vitality, int magic, int dexterity)
        {
            this.StrengthField = strength;
            this.AgilityField = agility;
            this.VitalityField = vitality;
            this.MagicField = magic;
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
            return 200 + 1.5 * (double)this.Dexterity + 1.0 * (double)this.Magic;
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
            return 0.001 + 0.001 * ((double)this.Agility / (double)UnitAttributes.Maximum);
        }

        /// <summary>
        /// Calculates the magic attack factor.
        /// </summary>
        /// <returns>The magic attack factor.</returns>
        public double CalculateMagicAttackFactor()
        {
            return 3.25 * (double)this.Magic;
        }

        /// <summary>
        /// Calculates the magic defense factor.
        /// </summary>
        /// <returns>The magic defense factor.</returns>
        public double CalculateMagicDefenseFactor()
        {
            return 3.05 * (double)this.Magic;
        }

        #endregion

        #endregion
    }
}
