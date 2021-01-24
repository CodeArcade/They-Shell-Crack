using Microsoft.Xna.Framework;
using System;

namespace LessRoomyMoreShooty.Component.Sprites.Item
{
    public abstract class Item : Sprite
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public event EventHandler OnItemPickUp;

        public Item(string name, string description = "")
        {
            Name = name;
            Description =  description;
        }

        public abstract void OnPickup(Player player);

        public override void OnCollision(Sprite sprite, GameTime gameTime)
        {
            if (!(sprite is Player)) return;

            OnItemPickUp?.Invoke(this, null);
        }
    }
}
