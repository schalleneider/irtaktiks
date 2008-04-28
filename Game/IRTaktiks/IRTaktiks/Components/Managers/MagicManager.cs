using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IRTaktiks.Components.Playables;

namespace IRTaktiks.Components.Managers
{
    /// <summary>
    /// Manager of magic's units.
    /// </summary>
    public class MagicManager
    {
        #region Singleton

        /// <summary>
        /// The instance of the class.
        /// </summary>
        private static MagicManager InstanceField;

        /// <summary>
        /// The instance of the class.
        /// </summary>
        public static MagicManager Instance
        {
            get { if (InstanceField == null) InstanceField = new MagicManager(); return InstanceField; }
        }

        /// <summary>
        /// Private constructor.
        /// </summary>
        private MagicManager()
        { }

        #endregion 

        #region Costs

        /// <summary>
        /// The cost of mp of the heal magic.
        /// </summary>
        public const int HealCost = 200;

        #endregion

        #region Bases

        /// <summary>
        /// The base calculation factor of the heal.
        /// </summary>
        public const float HealBase = 22;  

        #endregion

        #region Validations

        /// <summary>
        /// Check if the caster can use the Heal magic.
        /// </summary>
        /// <param name="caster">The unit that cast the magic.</param>
        /// <returns>True, if the unit can cast the magic. False otherwise.</returns>
        public bool CanHeal(Unit caster)
        {
            return caster.Mana >= MagicManager.HealCost;
        }

        #endregion

        #region Magics

        /// <summary>
        /// Cast the Heal magic.
        /// </summary>
        /// <param name="caster">The unit that cast the magic.</param>
        /// <param name="target">The unit targeted by the magic.</param>
        public void Heal(Unit caster, Unit target)
        {
            // Effects on caster.
            caster.Mana -= MagicManager.HealCost;            
            caster.Time = 0;

            // Effects on target.
            if (target != null)
            {
                double heal = MagicManager.HealBase * 2.95 * caster.Attributes.CalculateMagicAttackFactor() * 0.1;
                target.Life -= (int)heal;
            }
        }

        #endregion
    }
}
