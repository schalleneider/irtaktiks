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
        #region Constants

        /// <summary>
        /// The job factor used in the formules.
        /// </summary>
        private static float[][] Factors = new float[][] {
            // HP   : Knight | Paladin | Wizard | Priest | Assasin | Monk
            new float[] { 0.85f, 1.00f, 0.55f, 0.60f, 0.75f, 0.70f },
            // MP   : Knight | Paladin | Wizard | Priest | Assasin | Monk
            new float[] { 0.50f, 0.75f, 0.90f, 1.00f, 0.55f, 0.65f },
            // ATK  : Knight | Paladin | Wizard | Priest | Assasin | Monk
            new float[] { 1.00f, 0.70f, 0.50f, 0.60f, 0.90f, 0.80f },
            // DEF  : Knight | Paladin | Wizard | Priest | Assasin | Monk
            new float[] { 0.60f, 1.00f, 0.80f, 0.85f, 0.55f, 0.65f },
            // MATK : Knight | Paladin | Wizard | Priest | Assasin | Monk
            new float[] { 0.40f, 0.85f, 1.00f, 0.90f, 0.50f, 0.60f },
            // MDEF : Knight | Paladin | Wizard | Priest | Assasin | Monk
            new float[] { 0.40f, 1.00f, 0.85f, 0.95f, 0.55f, 0.65f },
        };

        #endregion

        #region Parameters

        /// <summary>
        /// Factor of the MaximumLife formula.
        /// </summary>
        public float LifeFactor;

        /// <summary>
        /// Factor of the MaximumMana formula.
        /// </summary>
        public float ManaFactor;

        /// <summary>
        /// Factor of the Attack formula.
        /// </summary>
        public float AttackFactor;

        /// <summary>
        /// Factor of the Defense formula.
        /// </summary>
        public float DefenseFactor;

        /// <summary>
        /// Factor of the MagicAttack formula.
        /// </summary>
        public float MagicAttackFactor;

        /// <summary>
        /// Factor of the MagicDefense formula.
        /// </summary>
        public float MagicDefenseFactor;

        #endregion

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
            set { StrengthField = value; }
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
            set { AgilityField = value; }
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
            set { VitalityField = value; }
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
            set { InteligenceField = value; }
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
            set { DexterityField = value; }
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
                return Convert.ToInt32((1 + (this.Vitality / 80)) * (20 + (5 * this.Level) + (this.LifeFactor * ((1 + this.Level) * (this.Level / 2.5)))));
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
                return Convert.ToInt32((1 + (this.Inteligence / 50)) * (5 + (this.ManaFactor * this.Level * 10))); ;
            }
        }

        /// <summary>
        /// The attack value of the unit.
        /// ((STR + (((STR / 10)^3) + (2 * LVL)) / 2) * @) + (DEX / 5))
        /// </summary>
        public int Attack
        {
            get
            {
                return Convert.ToInt32(((this.Strength + (((this.Strength / 10) * (this.Strength / 10) * (this.Strength / 10)) + (2 * this.Level)) / 2) * this.AttackFactor) + (this.Dexterity / 5));
            }
        }

        /// <summary>
        /// The defense value of the unit.
        /// ((2.5 * VIT + (VIT / 3) + (INT / 5) + LVL) * @)
        /// </summary>
        public int Defense
        {
            get
            {
                return Convert.ToInt32((2.5f * this.Vitality + (this.Vitality / 3) + (this.Inteligence / 5) + this.Level) * this.DefenseFactor);
            }
        }

        /// <summary>
        /// The magic attack value of the unit.
        /// (((INT / 5) * (INT / 5) + LVL) * @)
        /// </summary>
        public int MagicAttack
        {
            get
            {
                return Convert.ToInt32(((this.Inteligence / 5) * (this.Inteligence / 5) + this.Level) * this.MagicAttackFactor);
            }
        }

        /// <summary>
        /// The magic defense value of the unit.
        /// ((2.5 * INT + (INT / 3) + (VIT / 5) + LVL) * @)
        /// </summary>
        public int MagicDefense
        {
            get
            {
                return Convert.ToInt32((2.5f * this.Inteligence + (this.Inteligence / 3) + (this.Vitality / 5) + this.Level) * this.MagicDefenseFactor);
            }
        }

        /// <summary>
        /// The flee value of the unit.
        /// (20 + AGI)
        /// </summary>
        public int Flee
        {
            get
            {
                return Convert.ToInt32(20 + this.Agility);
            }
        }

        /// <summary>
        /// The hit value of the unit.
        /// (80 + DEX)
        /// </summary>
        public int Hit
        {
            get
            {
                return Convert.ToInt32(80 + this.Dexterity);
            }
        }

        /// <summary>
        /// The range value of the unit.
        /// </summary>
        public int Range
        {
            get
            {
                return Convert.ToInt32(100 + this.Dexterity + this.Agility);
            }
        }

        /// <summary>
        /// The short attack range value of the unit.
        /// </summary>
        public int ShortAttackRange
        {
            get
            {
                return 100;
            }
        }

        /// <summary>
        /// The long attack range value of the unit.
        /// </summary>
        public int LongAttackRange
        {
            get
            {
                return Convert.ToInt32(100 + (2 * this.Dexterity));
            }
        }

        /// <summary>
        /// The skill range value of the unit.
        /// </summary>
        public int SkillRange
        {
            get
            {
                return Convert.ToInt32(100 + (1.5 * this.Inteligence) + (1.5 * this.Dexterity));
            }
        }

        /// <summary>
        /// The delay value of the unit.
        /// </summary>
        public double Delay
        {
            get
            {
                return (0.001 + 0.001 * ((double)this.Agility / 99));
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

            this.Strength = strength;
            this.Agility = agility;
            this.Vitality = vitality;
            this.Inteligence = magic;
            this.Dexterity = dexterity;

            this.LifeFactor = Attributes.Factors[0][(int)this.Job];
            this.ManaFactor = Attributes.Factors[1][(int)this.Job];
            this.AttackFactor = Attributes.Factors[2][(int)this.Job];
            this.DefenseFactor = Attributes.Factors[3][(int)this.Job];
            this.MagicAttackFactor = Attributes.Factors[4][(int)this.Job];
            this.MagicDefenseFactor = Attributes.Factors[5][(int)this.Job];
        }

        #endregion
    }
}
