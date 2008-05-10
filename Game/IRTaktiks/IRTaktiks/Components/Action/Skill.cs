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
using IRTaktiks.Components.Playable;
using IRTaktiks.Components.Manager;

namespace IRTaktiks.Components.Action
{
    /// <summary>
    /// The logic representation of one skill.
    /// </summary>
    public class Skill : Command
    {
        #region Properties

        /// <summary>
        /// The magic points cost of the skill.
        /// </summary>
        protected int CostField;

        /// <summary>
        /// The magic points cost of the skill.
        /// </summary>
        public int Cost
        {
            get { return CostField; }
        }
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="unit">The owner of the skill.</param>
        /// <param name="name">The name of the skill.</param>
        /// <param name="cost">The cost of the skill.</param>
        /// <param name="animationType">The type of the animation that the skill will display.</param>
        /// <param name="commandEnabled">The method that will be invoked when the enabled property is checked.</param>
        /// <param name="commandExecute">The methods that will be invoked when the execute method is invoked.</param>
        public Skill(Unit unit, string name, int cost, AnimationManager.AnimationType animationType, CommandEnabledDelegate commandEnabled, CommandExecuteDelegate commandExecute)
            : base(unit, name, animationType, commandEnabled, commandExecute)
        {
            this.CostField = cost;
        }

        #endregion
    }
}
