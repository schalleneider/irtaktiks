using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IRTaktiks.Components.Logic;
using IRTaktiks.Components.Menu;

namespace IRTaktiks.Components.Playables
{
	/// <summary>
	/// Representation of one combat unit of the game.
	/// </summary>
	public class Unit : DrawableGameComponent
    {
        #region Attributes

        /// <summary>
        /// The strength of the unit. Influences the attack power.
        /// </summary>
        private int StrengthField;

        /// <summary>
        /// The strength of the unit. Influences the attack power.
        /// </summary>
        public int Strength
        {
            get { return StrengthField; }
        }

        /// <summary>
        /// The agility of the unit. Influences the time.
        /// </summary>
        private int AgilityField;

        /// <summary>
        /// The agility of the unit. Influences the time.
        /// </summary>
        public int Agility
        {
            get { return AgilityField; }
        }

        /// <summary>
        /// The vitality of the unit. Influences the deffense.
        /// </summary>
        private int VitalityField;

        public int Vitality
        {
            get { return VitalityField; }
        }

        /// <summary>
        /// The magic of the unit. Influences the magic attack and deffense.
        /// </summary>
        private int MagicField;

        /// <summary>
        /// The magic of the unit. Influences the magic attack and deffense.
        /// </summary>
        public int Magic
        {
            get { return MagicField; }
        }

        /// <summary>
        /// The dexteriry of unit. Influences the range of attacks.
        /// </summary>
        private int DexterityField;

        /// <summary>
        /// The dexteriry of unit. Influences the range of attacks.
        /// </summary>
        public int Dexterity
        {
            get { return DexterityField; }
        }

        #endregion

        #region Statuses

        /// <summary>
		/// The actual life points of the unit.
		/// </summary>
		private int LifeField;

		/// <summary>
        /// The actual life points of the unit.
		/// </summary>
		public int Life
		{
			get { return LifeField; }
		}

		/// <summary>
        /// The actual mana points of the unit.
		/// </summary>
		private int ManaField;

		/// <summary>
        /// The actual mana points of the unit.
		/// </summary>
		public int Mana
		{
			get { return ManaField; }
		}

		/// <summary>
		/// The actual time of the wait to act. 
		/// Mininum value is 0 and maximum is 1.
		/// </summary>
		private double TimeField;

		/// <summary>
		/// The actual time of the wait to act.
		/// Mininum value is 0 and maximum is 1.
		/// </summary>
		public double Time
		{
			get { return TimeField; }
		}

        /// <summary>
        /// The position of the Unit.
        /// </summary>
        private Vector2 PositionField;

        /// <summary>
        /// The position of the Unit.
        /// </summary>
        public Vector2 Position
        {
            get { return PositionField; }
            set { PositionField = value; }
        }

        /// <summary>
        /// The orientation of the unit.
        /// </summary>
        private UnitOrientation OrientationField;

        /// <summary>
        /// The orientation of the unit.
        /// </summary>
        public UnitOrientation Orientation
        {
            get { return OrientationField; }
            set { OrientationField = value; }
        }

        /// <summary>
        /// The character texture of the unit.
        /// </summary>
        private Texture2D UnitTextureField;

        /// <summary>
        /// The character texture of the unit.
        /// </summary>
        public Texture2D UnitTexture
        {
            get { return UnitTextureField; }
            set { UnitTextureField = value; }
        }

        #endregion

        #region Verifications

        /// <summary>
        /// True if the unit is dead. Else otherwise.
        /// </summary>
        public bool IsDead
        {
            get { return this.Life <= 0; }
        }

        /// <summary>
        /// True if the unit is waiting. Else otherwise.
        /// </summary>
        public bool IsWaiting
        {
            get { return this.Time < 1; }
        }

        /// <summary>
        /// True if the actual life of the unit is above 50%.
        /// </summary>
        public bool IsStatusAlive
        {
            get { return (this.Life / this.FullLife) > 0.5; }
        }
        
        /// <summary>
        /// True if the actual life of the unit is above 10% and less 50%.
        /// </summary>
        public bool IsStatusDamaged
        {
            get { return ((this.Life / this.FullLife) <= 0.5 && ((this.Life / this.FullLife) >= 0.1)); }
        }
       
        /// <summary>
        /// True if the actual life of the unit is less 10%.
        /// </summary>
        public bool IsStatusDeading
        {
            get { return (this.Life / this.FullLife) < 0.1; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// The owner of the unit.
        /// </summary>
        private Player PlayerField;

        /// <summary>
        /// The owner of the unit.
        /// </summary>
        public Player Player
        {
            get { return PlayerField; }
        }
                
        /// <summary>
		/// Name of unit.
		/// </summary>
		private String NameField;

		/// <summary>
		/// Name of unit.
		/// </summary>
		public String Name
		{
			get { return NameField; }
		}

        /// <summary>
        /// Menu of the unit.
        /// </summary>
        private UnitMenu MenuField;

        /// <summary>
        /// Menu of the unit.
        /// </summary>
        public UnitMenu Menu
        {
            get { return MenuField; }
        }
        
        /// <summary>
        /// The total life points of the unit.
        /// </summary>
        private int FullLifeField;
        
        /// <summary>
        /// The total life points of the unit.
        /// </summary>
        public int FullLife
        {
            get { return FullLifeField; }
        }
        
        /// <summary>
        /// The total mana points of the unit.
        /// </summary>
        private int FullManaField;
        
        /// <summary>
        /// The total mana points of the unit.
        /// </summary>
        public int FullMana
        {
            get { return FullManaField; }
        }
        
		#endregion

		#region Constructor

        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="game">The instance of game that is running.</param>
        /// <param name="player">The owner of the unit.</param>
		/// <param name="name">The name of unit.</param>
		/// <param name="life">The total life points.</param>
		/// <param name="mana">The total mana points.</param>
		/// <param name="strenght">The strenght attribute.</param>
		/// <param name="agility">The agility attribute.</param>
		/// <param name="vitality">The vittality attribute.</param>
		/// <param name="magic">The magic attribute.</param>
		/// <param name="dexterity">The dexterity attribute.</param>
		public Unit(Game game, Player player, string name, int life, int mana, int strenght, int agility, int vitality, int magic, int dexterity)
            : base(game)
		{
            this.PlayerField = player;
            this.NameField = name;

            this.MenuField = new UnitMenu(game, this);

            this.LifeField = life;
            this.FullLifeField = life;

            this.ManaField = mana;
            this.FullManaField = mana;

            this.StrengthField = strenght;
            this.AgilityField = agility;
            this.VitalityField = vitality;
            this.MagicField = magic;
            this.DexterityField = dexterity;

            this.TimeField = 0;
		}

		#endregion

        #region Component Methods

		/// <summary>
		/// Allows the game component to perform any initialization it needs to before starting
		/// to run. This is where it can query for any required services and load content.
		/// </summary>
		public override void Initialize()
		{
            base.Initialize();
		} 

		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
            base.Update(gameTime);
		}

        /// <summary>
        /// Called when the DrawableGameComponent needs to be drawn. Override this method
        //  with component-specific drawing code.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            IRTGame game = this.Game as IRTGame;

            game.SpriteBatch.Begin();

            game.SpriteBatch.Draw(this.UnitTexture, this.Position, new Rectangle(0, 48 * (int)this.Orientation, 32, 48), Color.White);

            game.SpriteBatch.End();

            base.Draw(gameTime);
        }

		#endregion
	}
}