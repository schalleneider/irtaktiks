using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using System.Text;
using IRTaktiks.Components.Logic;

namespace IRTaktiks.Components.Config
{
    /// <summary>
    /// Representation of character textures.
    /// </summary>
    public class JobConfig
    {
        #region Properties

        /// <summary>
        /// The texture of the item.
        /// </summary>
        private Texture2D TextureField;

        /// <summary>
        /// The texture of the item.
        /// </summary>
        public Texture2D Texture
        {
            get { return TextureField; }
        }

        /// <summary>
        /// The job of the item.
        /// </summary>
        private Job JobField;

        /// <summary>
        /// The job of the item.
        /// </summary>
        public Job Job
        {
            get { return JobField; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="texture">The texture of the item.</param>
        /// <param name="job">The job of the item.</param>
        private JobConfig(Texture2D texture, Job job)
        {
            this.TextureField = texture;
            this.Job = job;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Construct all the job configuration items.
        /// </summary>
        /// <returns>A list with all the job configuration items.</returns>
        public static List<JobConfig> Construct()
        {
            List<JobConfig> list = new List<JobConfig>();

            list.Add(new JobConfig(null, Job.Assasin));

            return list;
        }

        #endregion
    }
}
