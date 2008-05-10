using System;
using System.Collections.Generic;
using System.Text;

namespace IRTaktiks.Components.Logic
{
    /// <summary>
    /// Representation of the jobs of the unit.
    /// </summary>
    public enum Job
    {
        /// <summary>
        /// Knight. High atk and dex.
        /// </summary>
        Knight,
        
        /// <summary>
        /// Paladin. High str and int.
        /// </summary>
        Paladin,

        /// <summary>
        /// Wizard. Hight int and dex.
        /// </summary>
        Wizard,

        /// <summary>
        /// Priest. High vit and int.
        /// </summary>
        Priest,

        /// <summary>
        /// Assasin. High agi and dex.
        /// </summary>
        Assasin,

        /// <summary>
        /// Monk. High agi and int.
        /// </summary>
        Monk,
    }
}
