using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IRTaktiks.Components.Playable;
using IRTaktiks.Components.Scenario;
using IRTaktiks.Components.Action;
using System.Collections.Generic;
using IRTaktiks.Components.Logic;

namespace IRTaktiks.Components.Manager
{
    /// <summary>
    /// Manager of items.
    /// </summary>
    public class ItemManager
    {
        #region Singleton

        /// <summary>
        /// The instance of the class.
        /// </summary>
        private static ItemManager InstanceField;

        /// <summary>
        /// The instance of the class.
        /// </summary>
        public static ItemManager Instance
        {
            get { if (InstanceField == null) InstanceField = new ItemManager(); return InstanceField; }
        }

        /// <summary>
        /// Private constructor.
        /// </summary>
        private ItemManager()
        {
            this.Random = new Random();
        }

        #endregion 

        #region Properties

        /// <summary>
        /// Random number generator.
        /// </summary>
        private Random Random;

        #endregion

        #region Construct

        /// <summary>
        /// Construct the list of the items based on attributes.
        /// </summary>
        /// <param name="unit">The unit that the items will be constructed.</param>
        /// <returns>The list of items.</returns>
        public List<Item> Construct(Unit unit)
        {
            List<Item> items = new List<Item>();

            #region Same items in all Jobs
            /*
            switch (unit.Attributes.Job)
            {
                case Job.Assasin:
                    {
                        return attacks;
                    }

                case Job.Knight:
                    {
                        return attacks;
                    }

                case Job.Monk:
                    {
                        return attacks;
                    }

                case Job.Paladin:
                    {
                        return attacks;
                    }

                case Job.Priest:
                    {
                        return attacks;
                    }

                case Job.Wizard:
                    {
                        return attacks;
                    }

                default:
                    {
                        return attacks;
                    }
            }
            */
            #endregion

            items.Add(new Item(unit, "Potion", 10, Item.ItemType.Target, Potion));
            items.Add(new Item(unit, "Ether", 5, Item.ItemType.Target, Ether));
            items.Add(new Item(unit, "Elixir", 1, Item.ItemType.Self, Elixir));

            return items;
        }

        #endregion

        #region Item

        /// <summary>
        /// Restores 500 life points.
        /// </summary>
        /// <param name="command">Item used.</param>
        /// <param name="caster">The caster of the item.</param>
        /// <param name="target">The target of the item.</param>
        /// <param name="position">The position of the target.</param>
        private void Potion(Command command, Unit caster, Unit target, Vector2 position)
        {
            // Obtain the game instance.
            IRTGame game = caster.Game as IRTGame;

            // Show the animation.
            Vector2 animationPosition = target == null ? position : new Vector2(target.Position.X + target.Texture.Width / 2, target.Position.Y + target.Texture.Height / 8);
            AnimationManager.Instance.QueueAnimation(AnimationManager.AnimationType.Item, animationPosition);

            // Effects on caster.
            caster.Time = 0;

            // Effects on target.
            if (target != null)
            {
                target.Life += 500;

                // Show the damage.
                game.DamageManager.Queue(new Damage(500, null, target.Position, Damage.DamageType.Benefit));
            }
        }

        /// <summary>
        /// Restore 350 mana points.
        /// </summary>
        /// <param name="command">Item used.</param>
        /// <param name="caster">The caster of the item.</param>
        /// <param name="target">The target of the item.</param>
        /// <param name="position">The position of the target.</param>
        private void Ether(Command command, Unit caster, Unit target, Vector2 position)
        {
            // Obtain the game instance.
            IRTGame game = caster.Game as IRTGame;

            // Show the animation.
            Vector2 animationPosition = target == null ? position : new Vector2(target.Position.X + target.Texture.Width / 2, target.Position.Y + target.Texture.Height / 8);
            AnimationManager.Instance.QueueAnimation(AnimationManager.AnimationType.Item, animationPosition);

            // Effects on caster.
            caster.Time = 0;

            // Effects on target.
            if (target != null)
            {
                target.Mana += 350;

                // Show the damage.
                game.DamageManager.Queue(new Damage(350, "MP", target.Position, Damage.DamageType.Benefit));
            }
        }

        /// <summary>
        /// Restore all life points and mana poins.
        /// </summary>
        /// <param name="command">Item used.</param>
        /// <param name="caster">The caster of the item.</param>
        /// <param name="target">The target of the item.</param>
        /// <param name="position">The position of the target.</param>
        private void Elixir(Command command, Unit caster, Unit target, Vector2 position)
        {
            // Obtain the game instance.
            IRTGame game = caster.Game as IRTGame;

            // Show the animation.
            Vector2 animationPosition = target == null ? position : new Vector2(target.Position.X + target.Texture.Width / 2, target.Position.Y + target.Texture.Height / 8);
            AnimationManager.Instance.QueueAnimation(AnimationManager.AnimationType.Elixir, animationPosition);

            // Effects on caster.
            caster.Time = 0;

            // Effects on target.
            if (target != null)
            {
                target.Life = target.Attributes.MaximumLife;
                target.Mana = target.Attributes.MaximumMana;

                // Show the damage.
                game.DamageManager.Queue(new Damage("WOHOO", target.Position, Damage.DamageType.Benefit));
            }
        }

        #endregion
    }
}