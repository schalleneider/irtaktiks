using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IRTaktiks.Components.Playable;
using IRTaktiks.Components.Scenario;

namespace IRTaktiks.Components.Manager
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
            // Obtain the game instance.
            IRTGame game = caster.Game as IRTGame;

            // Effects on caster.
            caster.Mana -= MagicManager.HealCost;            
            caster.Time = 0;

            // Show the mp cost.
            if (caster != target)
            {
                game.DamageManager.Queue(new Damage(MagicManager.HealCost, "MP", caster.Position, Damage.DamageType.Harmful, 0.1f, 10f));
            }

            // Effects on target.
            if (target != null)
            {
                // Heal the target
                double heal = MagicManager.HealBase * 2.95 * caster.Attributes.CalculateMagicAttackFactor() * 0.1;
                target.Life += (int)heal;

                // Show the hp gain.
                game.DamageManager.Queue(new Damage((int)heal, null, target.Position, Damage.DamageType.Benefit, 0.1f, 10f));
            }
        }

        #endregion
    }
}
