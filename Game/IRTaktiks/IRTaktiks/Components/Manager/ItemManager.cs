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
        { }

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
        /// Use the item.
        /// </summary>
        /// <param name="command">Item used.</param>
        /// <param name="caster">The caster of the item.</param>
        /// <param name="target">The target of the item.</param>
        /// <param name="position">The position of the target.</param>
        private void Potion(Command command, Unit caster, Unit target, Vector2 position)
        {
            Item item = command as Item;
        }

        /// <summary>
        /// Use the item.
        /// </summary>
        /// <param name="command">Item used.</param>
        /// <param name="caster">The caster of the item.</param>
        /// <param name="target">The target of the item.</param>
        /// <param name="position">The position of the target.</param>
        private void Ether(Command command, Unit caster, Unit target, Vector2 position)
        {
            Item item = command as Item;
        }

        /// <summary>
        /// Use the item.
        /// </summary>
        /// <param name="command">Item used.</param>
        /// <param name="caster">The caster of the item.</param>
        /// <param name="target">The target of the item.</param>
        /// <param name="position">The position of the target.</param>
        private void Elixir(Command command, Unit caster, Unit target, Vector2 position)
        {
            Item item = command as Item;
        }

        #endregion
    }
}
