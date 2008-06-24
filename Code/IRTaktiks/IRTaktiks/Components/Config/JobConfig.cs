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
using IRTaktiks.Components.Manager;

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
            this.JobField = job;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Construct all the job configuration items.
        /// </summary>
        /// <returns>An array with all the job configuration items.</returns>
        public static JobConfig[] Construct()
        {
            List<JobConfig> list = new List<JobConfig>();

            list.Add(new JobConfig(TextureManager.Instance.Characters.Assassin_M_001, Job.Assasin));
            list.Add(new JobConfig(TextureManager.Instance.Characters.Assassin_M_002, Job.Assasin));
            list.Add(new JobConfig(TextureManager.Instance.Characters.Assassin_W_001, Job.Assasin));
            list.Add(new JobConfig(TextureManager.Instance.Characters.Assassin_W_002, Job.Assasin));

            list.Add(new JobConfig(TextureManager.Instance.Characters.Knight_M_001, Job.Knight));
            list.Add(new JobConfig(TextureManager.Instance.Characters.Knight_M_002, Job.Knight));
            list.Add(new JobConfig(TextureManager.Instance.Characters.Knight_W_001, Job.Knight));
            list.Add(new JobConfig(TextureManager.Instance.Characters.Knight_W_002, Job.Knight));

            list.Add(new JobConfig(TextureManager.Instance.Characters.Monk_M_001, Job.Monk));
            list.Add(new JobConfig(TextureManager.Instance.Characters.Monk_M_002, Job.Monk));
            list.Add(new JobConfig(TextureManager.Instance.Characters.Monk_W_001, Job.Monk));
            list.Add(new JobConfig(TextureManager.Instance.Characters.Monk_W_002, Job.Monk));

            list.Add(new JobConfig(TextureManager.Instance.Characters.Paladin_M_001, Job.Paladin));
            list.Add(new JobConfig(TextureManager.Instance.Characters.Paladin_M_002, Job.Paladin));
            list.Add(new JobConfig(TextureManager.Instance.Characters.Paladin_W_001, Job.Paladin));
            list.Add(new JobConfig(TextureManager.Instance.Characters.Paladin_W_002, Job.Paladin));

            list.Add(new JobConfig(TextureManager.Instance.Characters.Priest_M_001, Job.Priest));
            list.Add(new JobConfig(TextureManager.Instance.Characters.Priest_M_002, Job.Priest));
            list.Add(new JobConfig(TextureManager.Instance.Characters.Priest_W_001, Job.Priest));
            list.Add(new JobConfig(TextureManager.Instance.Characters.Priest_W_002, Job.Priest));

            list.Add(new JobConfig(TextureManager.Instance.Characters.Wizard_M_001, Job.Wizard));
            list.Add(new JobConfig(TextureManager.Instance.Characters.Wizard_M_002, Job.Wizard));
            list.Add(new JobConfig(TextureManager.Instance.Characters.Wizard_W_001, Job.Wizard));
            list.Add(new JobConfig(TextureManager.Instance.Characters.Wizard_W_002, Job.Wizard));

            return list.ToArray();
        }

        #endregion
    }
}
