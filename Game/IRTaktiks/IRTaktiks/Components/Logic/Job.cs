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
        /// Crusader. High str and int.
        /// </summary>
        Crusader,

        /// <summary>
        /// Wizard. Hight int and dex.
        /// </summary>
        Wizard,

        /// <summary>
        /// Priest. High vit and int.
        /// </summary>
        Priest,

        /// <summary>
        /// Thief. High agi and dex.
        /// </summary>
        Thief,

        /// <summary>
        /// Monk. High agi and int.
        /// </summary>
        Monk,
    }
}
