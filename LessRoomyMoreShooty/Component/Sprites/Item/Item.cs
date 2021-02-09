using LessRoomyMoreShooty.Component.Effects;
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
            Description = description;
        }

        public abstract void OnPickup(Player player);

        public override void OnCollision(Sprite sprite, GameTime gameTime)
        {
            if (!(sprite is Player) || IsRemoved) return;

            CurrentState.AddComponent(new ItemPickUp(this, Position), 0);

            IsRemoved = true;
            AudioManager.PlayEffect(ContentManager.ItemPickUpSoundEffect, 0.25f);
            ParticleManager.GenerateNewParticle(Color.White, new Vector2(sprite.Position.X + (sprite.Size.Width / 2), sprite.Position.Y + (sprite.Size.Height / 2)), ContentManager.ItemPickUpParticle, 10, 30);

            OnPickup((Player)sprite);
            OnItemPickUp?.Invoke(this, null);
        }
    }
}
