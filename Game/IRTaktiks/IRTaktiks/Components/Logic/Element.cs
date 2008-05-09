using System;
using System.Collections.Generic;
using System.Text;

namespace IRTaktiks.Components.Logic
{
    /// <summary>
    /// Representation of the elements of the unit.
    /// </summary>
    public enum Element
    {
        /// <summary>
        /// Fire element. Strong against earth, weak against water.
        /// </summary>
        Fire,
        
        /// <summary>
        /// Ice element. Strong against fire, weak against wind.
        /// </summary>
        Water,

        /// <summary>
        /// Wind element. Strong against water, weak against earth.
        /// </summary>
        Wind,

        /// <summary>
        /// Earth element. Strong against wind, weak against fire.
        /// </summary>
        Earth,

        /// <summary>
        /// Holy element. Strong against dark, weak against dark.
        /// </summary>
        Holy,

        /// <summary>
        /// Dark element. Strong against holy, weak against holy.
        /// </summary>
        Dark,
    }
}
