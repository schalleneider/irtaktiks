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
            /// Texture of Characters/Assassin_M_001.
            /// </summary>
            private Texture2D Assassin_M_001Field;

            /// <summary>
            /// Texture of Characters/Assassin_M_001.
            /// </summary>
            public Texture2D Assassin_M_001
            {
                get { return Assassin_M_001Field; }
                set { Assassin_M_001Field = value; }
            }

            /// <summary>
            /// Texture of Characters/Assassin_M_002.
            /// </summary>
            private Texture2D Assassin_M_002Field;

            /// <summary>
            /// Texture of Characters/Assassin_M_002.
            /// </summary>
            public Texture2D Assassin_M_002
            {
                get { return Assassin_M_002Field; }
                set { Assassin_M_002Field = value; }
            }

            /// <summary>
            /// Texture of Characters/Assassin_W_001.
            /// </summary>
            private Texture2D Assassin_W_001Field;

            /// <summary>
            /// Texture of Characters/Assassin_W_001.
            /// </summary>
            public Texture2D Assassin_W_001
            {
                get { return Assassin_W_001Field; }
                set { Assassin_W_001Field = value; }
            }

            /// <summary>
            /// Texture of Characters/Assassin_W_002.
            /// </summary>
            private Texture2D Assassin_W_002Field;

            /// <summary>
            /// Texture of Characters/Assassin_W_002.
            /// </summary>
            public Texture2D Assassin_W_002
            {
                get { return Assassin_W_002Field; }
                set { Assassin_W_002Field = value; }
            }

            /// <summary>
            /// Texture of Characters/Knight_M_001.
            /// </summary>
            private Texture2D Knight_M_001Field;

            /// <summary>
            /// Texture of Characters/Knight_M_001.
            /// </summary>
            public Texture2D Knight_M_001
            {
                get { return Knight_M_001Field; }
                set { Knight_M_001Field = value; }
            }

            /// <summary>
            /// Texture of Characters/Knight_M_002.
            /// </summary>
            private Texture2D Knight_M_002Field;

            /// <summary>
            /// Texture of Characters/Knight_M_002.
            /// </summary>
            public Texture2D Knight_M_002
            {
                get { return Knight_M_002Field; }
                set { Knight_M_002Field = value; }
            }

            /// <summary>
            /// Texture of Characters/Knight_W_001.
            /// </summary>
            private Texture2D Knight_W_001Field;

            /// <summary>
            /// Texture of Characters/Knight_W_001.
            /// </summary>
            public Texture2D Knight_W_001
            {
                get { return Knight_W_001Field; }
                set { Knight_W_001Field = value; }
            }

            /// <summary>
            /// Texture of Characters/Knight_W_002.
            /// </summary>
            private Texture2D Knight_W_002Field;

            /// <summary>
            /// Texture of Characters/Knight_W_002.
            /// </summary>
            public Texture2D Knight_W_002
            {
                get { return Knight_W_002Field; }
                set { Knight_W_002Field = value; }
            }

            /// <summary>
            /// Texture of Characters/Monk_M_001.
            /// </summary>
            private Texture2D Monk_M_001Field;

            /// <summary>
            /// Texture of Characters/Monk_M_001.
            /// </summary>
            public Texture2D Monk_M_001
            {
                get { return Monk_M_001Field; }
                set { Monk_M_001Field = value; }
            }

            /// <summary>
            /// Texture of Characters/Monk_M_002.
            /// </summary>
            private Texture2D Monk_M_002Field;

            /// <summary>
            /// Texture of Characters/Monk_M_002.
            /// </summary>
            public Texture2D Monk_M_002
            {
                get { return Monk_M_002Field; }
                set { Monk_M_002Field = value; }
            }

            /// <summary>
            /// Texture of Characters/Monk_W_001.
            /// </summary>
            private Texture2D Monk_W_001Field;

            /// <summary>
            /// Texture of Characters/Monk_W_001.
            /// </summary>
            public Texture2D Monk_W_001
            {
                get { return Monk_W_001Field; }
                set { Monk_W_001Field = value; }
            }

            /// <summary>
            /// Texture of Characters/Monk_W_002.
            /// </summary>
            private Texture2D Monk_W_002Field;

            /// <summary>
            /// Texture of Characters/Monk_W_002.
            /// </summary>
            public Texture2D Monk_W_002
            {
                get { return Monk_W_002Field; }
                set { Monk_W_002Field = value; }
            }

            /// <summary>
            /// Texture of Characters/Paladin_M_001.
            /// </summary>
            private Texture2D Paladin_M_001Field;

            /// <summary>
            /// Texture of Characters/Paladin_M_001.
            /// </summary>
            public Texture2D Paladin_M_001
            {
                get { return Paladin_M_001Field; }
                set { Paladin_M_001Field = value; }
            }

            /// <summary>
            /// Texture of Characters/Paladin_M_002.
            /// </summary>
            private Texture2D Paladin_M_002Field;

            /// <summary>
            /// Texture of Characters/Paladin_M_002.
            /// </summary>
            public Texture2D Paladin_M_002
            {
                get { return Paladin_M_002Field; }
                set { Paladin_M_002Field = value; }
            }

            /// <summary>
            /// Texture of Characters/Paladin_W_001.
            /// </summary>
            private Texture2D Paladin_W_001Field;

            /// <summary>
            /// Texture of Characters/Paladin_W_001.
            /// </summary>
            public Texture2D Paladin_W_001
            {
                get { return Paladin_W_001Field; }
                set { Paladin_W_001Field = value; }
            }

            /// <summary>
            /// Texture of Characters/Paladin_W_002.
            /// </summary>
            private Texture2D Paladin_W_002Field;

            /// <summary>
            /// Texture of Characters/Paladin_W_002.
            /// </summary>
            public Texture2D Paladin_W_002
            {
                get { return Paladin_W_002Field; }
                set { Paladin_W_002Field = value; }
            }

            /// <summary>
            /// Texture of Characters/Priest_M_001.
            /// </summary>
            private Texture2D Priest_M_001Field;

            /// <summary>
            /// Texture of Characters/Priest_M_001.
            /// </summary>
            public Texture2D Priest_M_001
            {
                get { return Priest_M_001Field; }
                set { Priest_M_001Field = value; }
            }

            /// <summary>
            /// Texture of Characters/Priest_M_002.
            /// </summary>
            private Texture2D Priest_M_002Field;

            /// <summary>
            /// Texture of Characters/Priest_M_002.
            /// </summary>
            public Texture2D Priest_M_002
            {
                get { return Priest_M_002Field; }
                set { Priest_M_002Field = value; }
            }

            /// <summary>
            /// Texture of Characters/Priest_W_001.
            /// </summary>
            private Texture2D Priest_W_001Field;

            /// <summary>
            /// Texture of Characters/Priest_W_001.
            /// </summary>
            public Texture2D Priest_W_001
            {
                get { return Priest_W_001Field; }
                set { Priest_W_001Field = value; }
            }

            /// <summary>
            /// Texture of Characters/Priest_W_002.
            /// </summary>
            private Texture2D Priest_W_002Field;

            /// <summary>
            /// Texture of Characters/Priest_W_002.
            /// </summary>
            public Texture2D Priest_W_002
            {
                get { return Priest_W_002Field; }
                set { Priest_W_002Field = value; }
            }

            /// <summary>
            /// Texture of Characters/Wizard_M_001.
            /// </summary>
            private Texture2D Wizard_M_001Field;

            /// <summary>
            /// Texture of Characters/Wizard_M_001.
            /// </summary>
            public Texture2D Wizard_M_001
            {
                get { return Wizard_M_001Field; }
                set { Wizard_M_001Field = value; }
            }

            /// <summary>
            /// Texture of Characters/Wizard_M_002.
            /// </summary>
            private Texture2D Wizard_M_002Field;

            /// <summary>
            /// Texture of Characters/Wizard_M_002.
            /// </summary>
            public Texture2D Wizard_M_002
            {
                get { return Wizard_M_002Field; }
                set { Wizard_M_002Field = value; }
            }

            /// <summary>
            /// Texture of Characters/Wizard_W_001.
            /// </summary>
            private Texture2D Wizard_W_001Field;

            /// <summary>
            /// Texture of Characters/Wizard_W_001.
            /// </summary>
            public Texture2D Wizard_W_001
            {
                get { return Wizard_W_001Field; }
                set { Wizard_W_001Field = value; }
            }

            /// <summary>
            /// Texture of Characters/Wizard_W_002.
            /// </summary>
            private Texture2D Wizard_W_002Field;

            /// <summary>
            /// Texture of Characters/Wizard_W_002.
            /// </summary>
            public Texture2D Wizard_W_002
            {
                get { return Wizard_W_002Field; }
                set { Wizard_W_002Field = value; }
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
                /// Texture of Sprites/Config/AttributesPlayerOne.
                /// </summary>
                private Texture2D AttributesPlayerOneField;

                /// <summary>
                /// Texture of Sprites/Config/AttributesPlayerOne.
                /// </summary>
                public Texture2D AttributesPlayerOne
                {
                    get { return AttributesPlayerOneField; }
                    set { AttributesPlayerOneField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Config/AttributesPlayerTwo.
                /// </summary>
                private Texture2D AttributesPlayerTwoField;

                /// <summary>
                /// Texture of Sprites/Config/AttributesPlayerTwo.
                /// </summary>
                public Texture2D AttributesPlayerTwo
                {
                    get { return AttributesPlayerTwoField; }
                    set { AttributesPlayerTwoField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Config/KeyboardPlayerOne.
                /// </summary>
                private Texture2D KeyboardPlayerOneField;

                /// <summary>
                /// Texture of Sprites/Config/KeyboardPlayerOne.
                /// </summary>
                public Texture2D KeyboardPlayerOne
                {
                    get { return KeyboardPlayerOneField; }
                    set { KeyboardPlayerOneField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Config/KeyboardPlayerTwo.
                /// </summary>
                private Texture2D KeyboardPlayerTwoField;

                /// <summary>
                /// Texture of Sprites/Config/KeyboardPlayerTwo.
                /// </summary>
                public Texture2D KeyboardPlayerTwo
                {
                    get { return KeyboardPlayerTwoField; }
                    set { KeyboardPlayerTwoField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Config/PlayerOneGreen.
                /// </summary>
                private Texture2D PlayerOneGreenField;

                /// <summary>
                /// Texture of Sprites/Config/PlayerOneGreen.
                /// </summary>
                public Texture2D PlayerOneGreen
                {
                    get { return PlayerOneGreenField; }
                    set { PlayerOneGreenField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Config/PlayerOneRed.
                /// </summary>
                private Texture2D PlayerOneRedField;

                /// <summary>
                /// Texture of Sprites/Config/PlayerOneRed.
                /// </summary>
                public Texture2D PlayerOneRed
                {
                    get { return PlayerOneRedField; }
                    set { PlayerOneRedField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Config/PlayerSelected.
                /// </summary>
                private Texture2D PlayerSelectedField;

                /// <summary>
                /// Texture of Sprites/Config/PlayerSelected.
                /// </summary>
                public Texture2D PlayerSelected
                {
                    get { return PlayerSelectedField; }
                    set { PlayerSelectedField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Config/PlayerOneGreen.
                /// </summary>
                private Texture2D PlayerTwoGreenField;

                /// <summary>
                /// Texture of Sprites/Config/PlayerTwoGreen.
                /// </summary>
                public Texture2D PlayerTwoGreen
                {
                    get { return PlayerTwoGreenField; }
                    set { PlayerTwoGreenField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Config/PlayerTwoRed.
                /// </summary>
                private Texture2D PlayerTwoRedField;

                /// <summary>
                /// Texture of Sprites/Config/PlayerTwoRed.
                /// </summary>
                public Texture2D PlayerTwoRed
                {
                    get { return PlayerTwoRedField; }
                    set { PlayerTwoRedField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Config/UnitGreen.
                /// </summary>
                private Texture2D UnitGreenField;

                /// <summary>
                /// Texture of Sprites/Config/UnitGreen.
                /// </summary>
                public Texture2D UnitGreen
                {
                    get { return UnitGreenField; }
                    set { UnitGreenField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Config/UnitRed.
                /// </summary>
                private Texture2D UnitRedField;

                /// <summary>
                /// Texture of Sprites/Config/UnitRed.
                /// </summary>
                public Texture2D UnitRed
                {
                    get { return UnitRedField; }
                    set { UnitRedField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Config/UnitSelected.
                /// </summary>
                private Texture2D UnitSelectedField;

                /// <summary>
                /// Texture of Sprites/Config/UnitSelected.
                /// </summary>
                public Texture2D UnitSelected
                {
                    get { return UnitSelectedField; }
                    set { UnitSelectedField = value; }
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
                /// Texture of Sprites/Menu/ItemPlayerOne.
                /// </summary>
                private Texture2D ItemPlayerOneField;

                /// <summary>
                /// Texture of Sprites/Menu/ItemPlayerOne.
                /// </summary>
                public Texture2D ItemPlayerOne
                {
                    get { return ItemPlayerOneField; }
                    set { ItemPlayerOneField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Menu/ItemPlayerTwo.
                /// </summary>
                private Texture2D ItemPlayerTwoField;

                /// <summary>
                /// Texture of Sprites/Menu/ItemPlayerTwo.
                /// </summary>
                public Texture2D ItemPlayerTwo
                {
                    get { return ItemPlayerTwoField; }
                    set { ItemPlayerTwoField = value; }
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
                /// Texture of Sprites/Menu/SubmenuItemPlayerOne.
                /// </summary>
                private Texture2D SubmenuItemPlayerOneField;

                /// <summary>
                /// Texture of Sprites/Menu/SubmenuItemPlayerOne.
                /// </summary>
                public Texture2D SubmenuItemPlayerOne
                {
                    get { return SubmenuItemPlayerOneField; }
                    set { SubmenuItemPlayerOneField = value; }
                }

                /// <summary>
                /// Texture of Sprites/Menu/SubmenuItemPlayerTwo.
                /// </summary>
                private Texture2D SubmenuItemPlayerTwoField;

                /// <summary>
                /// Texture of Sprites/Menu/SubmenuItemPlayerTwo.
                /// </summary>
                public Texture2D SubmenuItemPlayerTwo
                {
                    get { return SubmenuItemPlayerTwoField; }
                    set { SubmenuItemPlayerTwoField = value; }
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
                /// Texture of Sprites/Unit/Attributes.
                /// </summary>
                private Texture2D AttributesField;

                /// <summary>
                /// Texture of Sprites/Unit/Attributes.
                /// </summary>
                public Texture2D Attributes
                {
                    get { return AttributesField; }
                    set { AttributesField = value; }
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
            this.Characters.Assassin_M_001 = game.Content.Load<Texture2D>("Characters/Assassin_M_001");
            this.Characters.Assassin_M_002 = game.Content.Load<Texture2D>("Characters/Assassin_M_002");
            this.Characters.Assassin_W_001 = game.Content.Load<Texture2D>("Characters/Assassin_W_001");
            this.Characters.Assassin_W_002 = game.Content.Load<Texture2D>("Characters/Assassin_W_002");

            this.Characters.Knight_M_001 = game.Content.Load<Texture2D>("Characters/Knight_M_001");
            this.Characters.Knight_M_002 = game.Content.Load<Texture2D>("Characters/Knight_M_002");
            this.Characters.Knight_W_001 = game.Content.Load<Texture2D>("Characters/Knight_W_001");
            this.Characters.Knight_W_002 = game.Content.Load<Texture2D>("Characters/Knight_W_002");

            this.Characters.Monk_M_001 = game.Content.Load<Texture2D>("Characters/Monk_M_001");
            this.Characters.Monk_M_002 = game.Content.Load<Texture2D>("Characters/Monk_M_002");
            this.Characters.Monk_W_001 = game.Content.Load<Texture2D>("Characters/Monk_W_001");
            this.Characters.Monk_W_002 = game.Content.Load<Texture2D>("Characters/Monk_W_002");

            this.Characters.Paladin_M_001 = game.Content.Load<Texture2D>("Characters/Paladin_M_001");
            this.Characters.Paladin_M_002 = game.Content.Load<Texture2D>("Characters/Paladin_M_002");
            this.Characters.Paladin_W_001 = game.Content.Load<Texture2D>("Characters/Paladin_W_001");
            this.Characters.Paladin_W_002 = game.Content.Load<Texture2D>("Characters/Paladin_W_002");

            this.Characters.Priest_M_001 = game.Content.Load<Texture2D>("Characters/Priest_M_001");
            this.Characters.Priest_M_002 = game.Content.Load<Texture2D>("Characters/Priest_M_002");
            this.Characters.Priest_W_001 = game.Content.Load<Texture2D>("Characters/Priest_W_001");
            this.Characters.Priest_W_002 = game.Content.Load<Texture2D>("Characters/Priest_W_002");

            this.Characters.Wizard_M_001 = game.Content.Load<Texture2D>("Characters/Wizard_M_001");
            this.Characters.Wizard_M_002 = game.Content.Load<Texture2D>("Characters/Wizard_M_002");
            this.Characters.Wizard_W_001 = game.Content.Load<Texture2D>("Characters/Wizard_W_001");
            this.Characters.Wizard_W_002 = game.Content.Load<Texture2D>("Characters/Wizard_W_002");

            this.Sprites.Config.AttributesPlayerOne = game.Content.Load<Texture2D>("Sprites/Config/AttributesPlayerOne");
            this.Sprites.Config.AttributesPlayerTwo = game.Content.Load<Texture2D>("Sprites/Config/AttributesPlayerTwo");
            this.Sprites.Config.KeyboardPlayerOne = game.Content.Load<Texture2D>("Sprites/Config/KeyboardPlayerOne");
            this.Sprites.Config.KeyboardPlayerTwo = game.Content.Load<Texture2D>("Sprites/Config/KeyboardPlayerTwo");
            this.Sprites.Config.PlayerOneGreen = game.Content.Load<Texture2D>("Sprites/Config/PlayerOneGreen");
            this.Sprites.Config.PlayerOneRed = game.Content.Load<Texture2D>("Sprites/Config/PlayerOneRed");
            this.Sprites.Config.PlayerSelected = game.Content.Load<Texture2D>("Sprites/Config/PlayerSelected");
            this.Sprites.Config.PlayerTwoGreen = game.Content.Load<Texture2D>("Sprites/Config/PlayerTwoGreen");
            this.Sprites.Config.PlayerTwoRed = game.Content.Load<Texture2D>("Sprites/Config/PlayerTwoRed");
            this.Sprites.Config.UnitGreen = game.Content.Load<Texture2D>("Sprites/Config/UnitGreen");
            this.Sprites.Config.UnitRed = game.Content.Load<Texture2D>("Sprites/Config/UnitRed");
            this.Sprites.Config.UnitSelected = game.Content.Load<Texture2D>("Sprites/Config/UnitSelected");

            this.Sprites.Debug.Arrow = game.Content.Load<Texture2D>("Sprites/Debug/Arrow");

            this.Sprites.Menu.AimAlly = game.Content.Load<Texture2D>("Sprites/Menu/AimAlly");
            this.Sprites.Menu.AimEnemy = game.Content.Load<Texture2D>("Sprites/Menu/AimEnemy");
            this.Sprites.Menu.AimNothing = game.Content.Load<Texture2D>("Sprites/Menu/AimNothing");
            this.Sprites.Menu.Background = game.Content.Load<Texture2D>("Sprites/Menu/Background");
            this.Sprites.Menu.ItemPlayerOne = game.Content.Load<Texture2D>("Sprites/Menu/ItemPlayerOne");
            this.Sprites.Menu.ItemPlayerTwo = game.Content.Load<Texture2D>("Sprites/Menu/ItemPlayerTwo");
            this.Sprites.Menu.Orientation = game.Content.Load<Texture2D>("Sprites/Menu/Orientation");
            this.Sprites.Menu.SelectedItem = game.Content.Load<Texture2D>("Sprites/Menu/SelectedItem");
            this.Sprites.Menu.SubmenuDisabledItem = game.Content.Load<Texture2D>("Sprites/Menu/SubmenuDisabledItem");
            this.Sprites.Menu.SubmenuItemPlayerOne = game.Content.Load<Texture2D>("Sprites/Menu/SubmenuItemPlayerOne");
            this.Sprites.Menu.SubmenuItemPlayerTwo = game.Content.Load<Texture2D>("Sprites/Menu/SubmenuItemPlayerTwo");
            this.Sprites.Menu.SubmenuSelectedItem = game.Content.Load<Texture2D>("Sprites/Menu/SubmenuSelectedItem");

            this.Sprites.Player.PlayerOneBackground = game.Content.Load<Texture2D>("Sprites/Player/PlayerOneBackground");
            this.Sprites.Player.PlayerTwoBackground = game.Content.Load<Texture2D>("Sprites/Player/PlayerTwoBackground");

            this.Sprites.Unit.Arrow = game.Content.Load<Texture2D>("Sprites/Unit/Arrow");
            this.Sprites.Unit.Attributes = game.Content.Load<Texture2D>("Sprites/Unit/Attributes");
            this.Sprites.Unit.FullStatusAlive = game.Content.Load<Texture2D>("Sprites/Unit/FullStatusAlive");
            this.Sprites.Unit.FullStatusDamaged = game.Content.Load<Texture2D>("Sprites/Unit/FullStatusDamaged");
            this.Sprites.Unit.FullStatusDeading = game.Content.Load<Texture2D>("Sprites/Unit/FullStatusDeading");
            this.Sprites.Unit.LifeBar = game.Content.Load<Texture2D>("Sprites/Unit/LifeBar");
            this.Sprites.Unit.ManaBar = game.Content.Load<Texture2D>("Sprites/Unit/ManaBar");
            this.Sprites.Unit.QuickStatus = game.Content.Load<Texture2D>("Sprites/Unit/QuickStatus");
            this.Sprites.Unit.TimeBar = game.Content.Load<Texture2D>("Sprites/Unit/TimeBar");
            
            this.Sprites.TitleScreen = game.Content.Load<Texture2D>("Sprites/TitleScreen");

            this.Terrains.Terrain = game.Content.Load<Texture2D>("Terrains/Terrain");

            this.Textures.Grass = game.Content.Load<Texture2D>("Textures/Grass");
            this.Textures.Particle = game.Content.Load<Texture2D>("Textures/Particle");
            this.Textures.Rock = game.Content.Load<Texture2D>("Textures/Rock");
            this.Textures.Sand = game.Content.Load<Texture2D>("Textures/Sand");
            this.Textures.Snow = game.Content.Load<Texture2D>("Textures/Snow");
        }

        #endregion
    }
}
