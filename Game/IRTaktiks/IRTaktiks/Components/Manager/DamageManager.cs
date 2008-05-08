using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using System.Threading;
using IRTaktiks.Components.Playable;
using IRTaktiks.Components.Scenario;

namespace IRTaktiks.Components.Manager
{
    /// <summary>
    /// Manager of damages.
    /// </summary>
    public class DamageManager
    {
        #region Properties

        /// <summary>
        /// Enable sprites to be drawn.
        /// </summary>
        private SpriteBatch SpriteBatchField;

        /// <summary>
        /// Enable sprites to be drawn.
        /// </summary>
        public SpriteBatch SpriteBatch
        {
            get { return SpriteBatchField; }
        }
        
        /// <summary>
        /// The list of damages to be drawn.
        /// </summary>
        private List<Damage> DamagesField;

        /// <summary>
        /// The list of damages to be drawn.
        /// </summary>
        public List<Damage> Damages
        {
            get { return DamagesField; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="game">The instance of the game.</param>
        public DamageManager(Game game)
        {
            this.DamagesField = new List<Damage>(10);
            this.SpriteBatchField = new SpriteBatch(game.GraphicsDevice);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Queue the damage to be drawn at the flush.
        /// </summary>
        /// <param name="damage">The damage to be drawed.</param>
        public void Queue(Damage damage)
        {
            this.Damages.Add(damage);
        }

        /// <summary>
        /// Update all the damages, removing the dead ones.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            for (int index = 0; index < this.Damages.Count; index++)
            {
                if (this.Damages[index].Alive)
                {
                    this.Damages[index].Update(gameTime);
                }
                else
                {
                    this.Damages.RemoveAt(index);
                }
            }
        }

        /// <summary>
        /// Draw all queued damages to be drawn.
        /// </summary>
        public void Draw()
        {
            if (this.Damages.Count > 0)
            {
                for (int index = 0; index < this.Damages.Count; index++)
		        {
                    this.SpriteBatch.Begin();
                    this.SpriteBatch.DrawString(this.Damages[index].Font, this.Damages[index].Text.ToString(), this.Damages[index].Position, this.Damages[index].Color);
                    this.SpriteBatch.End();
		        }
            }
        }

        #endregion
    }
}
