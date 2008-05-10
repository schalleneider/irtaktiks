using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace IRTaktiks.Components.Manager
{
    /// <summary>
    /// Manager of textures.
    /// </summary>
    public class TextureManager
    {
        #region Singleton

        /// <summary>
        /// The instance of the class.
        /// </summary>
        private static TextureManager InstanceField;

        /// <summary>
        /// The instance of the class.
        /// </summary>
        public static TextureManager Instance
        {
            get { if (InstanceField == null) InstanceField = new TextureManager(); return InstanceField; }
        }

        /// <summary>
        /// Private constructor.
        /// </summary>
        private TextureManager()
        { }

        #endregion

        #region Folders

        #region Characters

        /// <summary>
        /// Abstration for Characters folder.
        /// </summary>
        public class CharactersFolder
        {
            #region Folders

            #endregion

            #region Properties

            #endregion

            #region Textures

            /// <summary>
            /// Texture of Characters/Knight.
            /// </summary>
            private Texture2D KnightField;

            /// <summary>
            /// Texture of Characters/Knight.
            /// </summary>
            public Texture2D Knight
            {
                get { return KnightField; }
                set { KnightField = value; }
            }

            /// <summary>
            /// Texture of Characters/Wizard.
            /// </summary>
            private Texture2D WizardField;

            /// <summary>
            /// Texture of Characters/Wizard.
            /// </summary>
            public Texture2D Wizard
            {
                get { return WizardField; }
                set { WizardField = value; }
            }

            #endregion
        }

        #endregion

        #region Sprites

        /// <summary>
        /// Abstration for Sprites folder.
        /// </summary>
        public class SpritesFolder
        {
            #region Folders

            #region Sprites/Config

            /// <summary>
            /// Abstration for Sprites/Config folder.
            /// </summary>
            public class ConfigFolder
            {
                #region Folders

                #endregion

                #region Properties

                #endregion

                #region Textures

                /// <summary>
                /// Texture of Sprites/Config/Background.
                /// </summary>
                private Texture2D BackgroundField;

                /// <summary>
                /// Texture of Sprites/Config/Background.
                /// </summary>
                public Texture2D Background
                {
                    get { return BackgroundField; }
                    set { BackgroundField = value; }
                }

                #endregion
            }

            #endregion

            #region Sprites/Debug

            /// <summary>
            /// Abstration for Sprites/Debug folder.
            /// </summary>
            public class DebugFolder
            {
                #region Folders

                #endregion

                #region Properties

                #endregion

                #region Textures

                /// <summary>
                /// Texture of Sprites/Debug/Background.
                /// </summary>
                private Texture2D ArrowField;

                /// <summary>
                /// Texture of Sprites/Debug/Background.
                /// </summary>
                public Texture2D Arrow
                {
                    get { return ArrowField; }
                    set { ArrowField = value; }
                }

                #endregion
            }

            #endregion

            #region Sprites/Menu

            /// <summary>
            /// Abstration for Sprites/Menu folder.
            /// </summary>
            public class MenuFolder
            {
                #region Folders

                #endregion

                #region Properties

                #endregion

                #region Textures

                /// <summary>
                /// Texture of Sprites/Menu/AimAlly.
                /// </summary>
                private Texture2D AimAllyField;

                /// <summary>
                /// Texture of Sprites/Menu/AimAlly.
                /// </summary>
                public Texture2D AimAlly
                {
                    get { return AimAllyField; }
                    set { AimAllyField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Menu/AimEnemy.
                /// </summary>
                private Texture2D AimEnemyField;

                /// <summary>
                /// Texture of Sprites/Menu/AimEnemy.
                /// </summary>
                public Texture2D AimEnemy
                {
                    get { return AimEnemyField; }
                    set { AimEnemyField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Menu/AimNothing.
                /// </summary>
                private Texture2D AimNothingField;

                /// <summary>
                /// Texture of Sprites/Menu/AimNothing.
                /// </summary>
                public Texture2D AimNothing
                {
                    get { return AimNothingField; }
                    set { AimNothingField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Menu/Background.
                /// </summary>
                private Texture2D BackgroundField;
                
                /// <summary>
                /// Texture of Sprites/Menu/Background.
                /// </summary>
                public Texture2D Background
                {
                    get { return BackgroundField; }
                    set { BackgroundField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Menu/Item.
                /// </summary>
                private Texture2D ItemField;

                /// <summary>
                /// Texture of Sprites/Menu/Item.
                /// </summary>
                public Texture2D Item
                {
                    get { return ItemField; }
                    set { ItemField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Menu/Orientation.
                /// </summary>
                private Texture2D OrientationField;

                /// <summary>
                /// Texture of Sprites/Menu/Orientation.
                /// </summary>
                public Texture2D Orientation
                {
                    get { return OrientationField; }
                    set { OrientationField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Menu/SelectedItem.
                /// </summary>
                private Texture2D SelectedItemField;

                /// <summary>
                /// Texture of Sprites/Menu/SelectedItem.
                /// </summary>
                public Texture2D SelectedItem
                {
                    get { return SelectedItemField; }
                    set { SelectedItemField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Menu/SubmenuDisabled.
                /// </summary>
                private Texture2D SubmenuDisabledItemField;

                /// <summary>
                /// Texture of Sprites/Menu/SubmenuItem.
                /// </summary>
                public Texture2D SubmenuDisabledItem
                {
                    get { return SubmenuDisabledItemField; }
                    set { SubmenuDisabledItemField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Menu/SubmenuItem.
                /// </summary>
                private Texture2D SubmenuItemField;

                /// <summary>
                /// Texture of Sprites/Menu/SubmenuItem.
                /// </summary>
                public Texture2D SubmenuItem
                {
                    get { return SubmenuItemField; }
                    set { SubmenuItemField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Menu/SubmenuSelectedItem.
                /// </summary>
                private Texture2D SubmenuSelectedItemField;

                /// <summary>
                /// Texture of Sprites/Menu/SubmenuSelectedItem.
                /// </summary>
                public Texture2D SubmenuSelectedItem
                {
                    get { return SubmenuSelectedItemField; }
                    set { SubmenuSelectedItemField = value; }
                }

                #endregion
            }

            #endregion

            #region Sprites/Player

            /// <summary>
            /// Abstration for Sprites/Player folder.
            /// </summary>
            public class PlayerFolder
            {
                #region Folders

                #endregion

                #region Properties

                #endregion

                #region Textures

                /// <summary>
                /// Texture of Sprites/Player/PlayerOneBackground.
                /// </summary>
                private Texture2D PlayerOneBackgroundField;

                /// <summary>
                /// Texture of Sprites/Player/PlayerOneBackground.
                /// </summary>
                public Texture2D PlayerOneBackground
                {
                    get { return PlayerOneBackgroundField; }
                    set { PlayerOneBackgroundField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Player/PlayerTwoBackground.
                /// </summary>
                private Texture2D PlayerTwoBackgroundField;

                /// <summary>
                /// Texture of Sprites/Player/PlayerTwoBackground.
                /// </summary>
                public Texture2D PlayerTwoBackground
                {
                    get { return PlayerTwoBackgroundField; }
                    set { PlayerTwoBackgroundField = value; }
                }

                #endregion
            }

            #endregion

            #region Sprites/Unit

            /// <summary>
            /// Abstration for Sprites/Unit folder.
            /// </summary>
            public class UnitFolder
            {
                #region Folders

                #endregion

                #region Properties

                #endregion

                #region Textures

                /// <summary>
                /// Texture of Sprites/Unit/Arrow.
                /// </summary>
                private Texture2D ArrowField;

                /// <summary>
                /// Texture of Sprites/Unit/Arrow.
                /// </summary>
                public Texture2D Arrow
                {
                    get { return ArrowField; }
                    set { ArrowField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Unit/FullStatusAlive.
                /// </summary>
                private Texture2D FullStatusAliveField;

                /// <summary>
                /// Texture of Sprites/Unit/FullStatusAlive.
                /// </summary>
                public Texture2D FullStatusAlive
                {
                    get { return FullStatusAliveField; }
                    set { FullStatusAliveField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Unit/FullStatusDamaged.
                /// </summary>
                private Texture2D FullStatusDamagedField;

                /// <summary>
                /// Texture of Sprites/Unit/FullStatusDamaged.
                /// </summary>
                public Texture2D FullStatusDamaged
                {
                    get { return FullStatusDamagedField; }
                    set { FullStatusDamagedField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Unit/FullStatusDeading.
                /// </summary>
                private Texture2D FullStatusDeadingField;

                /// <summary>
                /// Texture of Sprites/Unit/FullStatusDeading.
                /// </summary>
                public Texture2D FullStatusDeading
                {
                    get { return FullStatusDeadingField; }
                    set { FullStatusDeadingField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Unit/LifeBar.
                /// </summary>
                private Texture2D LifeBarField;

                /// <summary>
                /// Texture of Sprites/Unit/LifeBar.
                /// </summary>
                public Texture2D LifeBar
                {
                    get { return LifeBarField; }
                    set { LifeBarField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Unit/ManaBar.
                /// </summary>
                private Texture2D ManaBarField;

                /// <summary>
                /// Texture of Sprites/Unit/ManaBar.
                /// </summary>
                public Texture2D ManaBar
                {
                    get { return ManaBarField; }
                    set { ManaBarField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Unit/QuickStatus.
                /// </summary>
                private Texture2D QuickStatusField;

                /// <summary>
                /// Texture of Sprites/Unit/QuickStatus.
                /// </summary>
                public Texture2D QuickStatus
                {
                    get { return QuickStatusField; }
                    set { QuickStatusField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Unit/TimeBar.
                /// </summary>
                private Texture2D TimeBarField;

                /// <summary>
                /// Texture of Sprites/Unit/TimeBar.
                /// </summary>
                public Texture2D TimeBar
                {
                    get { return TimeBarField; }
                    set { TimeBarField = value; }
                }

                #endregion
            }

            #endregion

            #endregion

            #region Properties

            /// <summary>
            /// Abstration for Sprites/Config folder.
            /// </summary>
            private ConfigFolder ConfigField;

            /// <summary>
            /// Abstration for Sprites/Config folder.
            /// </summary>
            public ConfigFolder Config
            {
                get { if (ConfigField == null) ConfigField = new ConfigFolder(); return ConfigField; }
            }

            /// <summary>
            /// Abstration for Sprites/Debug folder.
            /// </summary>
            private DebugFolder DebugField;

            /// <summary>
            /// Abstration for Sprites/Debug folder.
            /// </summary>
            public DebugFolder Debug
            {
                get { if (DebugField == null) DebugField = new DebugFolder(); return DebugField; }
            }
            
            /// <summary>
            /// Abstration for Sprites/Menu folder.
            /// </summary>
            private MenuFolder MenuField;
            
            /// <summary>
            /// Abstration for Sprites/Menu folder.
            /// </summary>
            public MenuFolder Menu
            {
                get { if (MenuField == null) MenuField = new MenuFolder(); return MenuField; }
            }
            
            /// <summary>
            /// Abstration for Sprites/Player folder.
            /// </summary>
            private PlayerFolder PlayerField;
            
            /// <summary>
            /// Abstration for Sprites/Player folder.
            /// </summary>
            public PlayerFolder Player
            {
                get { if (PlayerField == null) PlayerField = new PlayerFolder(); return PlayerField; }
            }
            
            /// <summary>
            /// Abstration for Sprites/Unit folder.
            /// </summary>
            private UnitFolder UnitField;
            
            /// <summary>
            /// Abstration for Sprites/Unit folder.
            /// </summary>
            public UnitFolder Unit
            {
                get { if (UnitField == null) UnitField = new UnitFolder(); return UnitField; }
            }
            
            #endregion

            #region Textures

            /// <summary>
            /// Texture of Sprites/TitleScreen.
            /// </summary>
            private Texture2D TitleScreenField;

            /// <summary>
            /// Texture of Sprites/TitleScreen.
            /// </summary>
            public Texture2D TitleScreen
            {
                get { return TitleScreenField; }
                set { TitleScreenField = value; }
            }

            #endregion
        }

        #endregion

        #region Terrains
        
        /// <summary>
        /// Abstration for Terrains folder.
        /// </summary>
        public class TerrainsFolder
        {
            #region Folders

            #endregion

            #region Properties

            #endregion

            #region Textures

            /// <summary>
            /// Texture of Terrains/Terrain.
            /// </summary>
            private Texture2D TerrainField;

            /// <summary>
            /// Texture of Terrains/Terrain.
            /// </summary>
            public Texture2D Terrain
            {
                get { return TerrainField; }
                set { TerrainField = value; }
            }

            #endregion
        }

        #endregion

        #region Textures

        /// <summary>
        /// Abstration for Textures folder.
        /// </summary>
        public class TexturesFolder
        {
            #region Folders

            #endregion

            #region Properties

            #endregion

            #region Textures

            /// <summary>
            /// Texture of Textures/Grass.
            /// </summary>
            private Texture2D GrassField;

            /// <summary>
            /// Texture of Textures/Grass.
            /// </summary>
            public Texture2D Grass
            {
                get { return GrassField; }
                set { GrassField = value; }
            }

            /// <summary>
            /// Texture of Textures/Particle.
            /// </summary>
            private Texture2D ParticleField;

            /// <summary>
            /// Texture of Textures/Particle.
            /// </summary>
            public Texture2D Particle
            {
                get { return ParticleField; }
                set { ParticleField = value; }
            }

            /// <summary>
            /// Texture of Textures/Rock.
            /// </summary>
            private Texture2D RockField;

            /// <summary>
            /// Texture of Textures/Rock.
            /// </summary>
            public Texture2D Rock
            {
                get { return RockField; }
                set { RockField = value; }
            }

            /// <summary>
            /// Texture of Textures/Sand.
            /// </summary>
            private Texture2D SandField;

            /// <summary>
            /// Texture of Textures/Sand.
            /// </summary>
            public Texture2D Sand
            {
                get { return SandField; }
                set { SandField = value; }
            }

            /// <summary>
            /// Texture of Textures/Snow.
            /// </summary>
            private Texture2D SnowField;

            /// <summary>
            /// Texture of Textures/Snow.
            /// </summary>
            public Texture2D Snow
            {
                get { return SnowField; }
                set { SnowField = value; }
            }

            #endregion
        }

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// Abstraction for the Characters folder.
        /// </summary>
        private CharactersFolder CharactersField;

        /// <summary>
        /// Abstraction for the Characters folder.
        /// </summary>
        public CharactersFolder Characters
        {
            get { if (CharactersField == null) CharactersField = new CharactersFolder(); return CharactersField; }
        }

        /// <summary>
        /// Abstraction for the Sprites folder.
        /// </summary>
        private SpritesFolder SpritesField;

        /// <summary>
        /// Abstraction for the Sprites folder.
        /// </summary>
        public SpritesFolder Sprites
        {
            get { if (SpritesField == null) SpritesField = new SpritesFolder(); return SpritesField; }
        }

        /// <summary>
        /// Abstraction for the Terrains folder.
        /// </summary>
        private TerrainsFolder TerrainsField;

        /// <summary>
        /// Abstraction for the Terrains folder.
        /// </summary>
        public TerrainsFolder Terrains
        {
            get { if (TerrainsField == null) TerrainsField = new TerrainsFolder(); return TerrainsField; }
        }

        /// <summary>
        /// Abstraction for the Textures folder.
        /// </summary>
        private TexturesFolder TexturesField;

        /// <summary>
        /// Abstraction for the Textures folder.
        /// </summary>
        public TexturesFolder Textures
        {
            get { if (TexturesField == null) TexturesField = new TexturesFolder(); return TexturesField; }
        }

        #endregion

        #region Initialize

        /// <summary>
        /// Initialize the instance of the class.
        /// </summary>
        public void Initialize(Game game)
        {
            this.Characters.Knight = game.Content.Load<Texture2D>("Characters/Knight");
            this.Characters.Wizard = game.Content.Load<Texture2D>("Characters/Wizard");

            this.Sprites.Config.Background = game.Content.Load<Texture2D>("Sprites/Config/Background");

            this.Sprites.Debug.Arrow = game.Content.Load<Texture2D>("Sprites/Debug/Arrow");

            this.Sprites.Menu.AimAlly = game.Content.Load<Texture2D>("Sprites/Menu/AimAlly");
            this.Sprites.Menu.AimEnemy = game.Content.Load<Texture2D>("Sprites/Menu/AimEnemy");
            this.Sprites.Menu.AimNothing = game.Content.Load<Texture2D>("Sprites/Menu/AimNothing");
            this.Sprites.Menu.Background = game.Content.Load<Texture2D>("Sprites/Menu/Background");
            this.Sprites.Menu.Item = game.Content.Load<Texture2D>("Sprites/Menu/Item");
            this.Sprites.Menu.Orientation = game.Content.Load<Texture2D>("Sprites/Menu/Orientation");
            this.Sprites.Menu.SelectedItem = game.Content.Load<Texture2D>("Sprites/Menu/SelectedItem");
            this.Sprites.Menu.SubmenuDisabledItem = game.Content.Load<Texture2D>("Sprites/Menu/SubmenuDisabledItem");
            this.Sprites.Menu.SubmenuItem = game.Content.Load<Texture2D>("Sprites/Menu/SubmenuItem");
            this.Sprites.Menu.SubmenuSelectedItem = game.Content.Load<Texture2D>("Sprites/Menu/SubmenuSelectedItem");

            this.Sprites.Player.PlayerOneBackground = game.Content.Load<Texture2D>("Sprites/Player/PlayerOneBackground");
            this.Sprites.Player.PlayerTwoBackground = game.Content.Load<Texture2D>("Sprites/Player/PlayerTwoBackground");

            this.Sprites.Unit.Arrow = game.Content.Load<Texture2D>("Sprites/Unit/Arrow");
            this.Sprites.Unit.FullStatusAlive = game.Content.Load<Texture2D>("Sprites/Unit/FullStatusAlive");
            this.Sprites.Unit.FullStatusDamaged = game.Content.Load<Texture2D>("Sprites/Unit/FullStatusDamaged");
            this.Sprites.Unit.FullStatusDeading = game.Content.Load<Texture2D>("Sprites/Unit/FullStatusDeading");
            this.Sprites.Unit.LifeBar = game.Content.Load<Texture2D>("Sprites/Unit/LifeBar");
            this.Sprites.Unit.ManaBar = game.Content.Load<Texture2D>("Sprites/Unit/ManaBar");
            this.Sprites.Unit.QuickStatus = game.Content.Load<Texture2D>("Sprites/Unit/QuickStatus");
            this.Sprites.Unit.TimeBar = game.Content.Load<Texture2D>("Sprites/Unit/TimeBar");
            
            this.Sprites.TitleScreen = game.Content.Load<Texture2D>("Sprites/TitleScreen");

            //this.Terrains.Terrain = game.Content.Load<Texture2D>("Terrains/Terrain");

            this.Textures.Grass = game.Content.Load<Texture2D>("Textures/Grass");
            this.Textures.Particle = game.Content.Load<Texture2D>("Textures/Particle");
            this.Textures.Rock = game.Content.Load<Texture2D>("Textures/Rock");
            this.Textures.Sand = game.Content.Load<Texture2D>("Textures/Sand");
            this.Textures.Snow = game.Content.Load<Texture2D>("Textures/Snow");
        }

        #endregion
    }
}
