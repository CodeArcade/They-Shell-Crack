using Microsoft.Xna.Framework;
using System;

namespace LessRoomyMoreShooty.Component.Sprites
{

    public class Projectile : Sprite
    {
        private double TTL { get; set; }
        private double TimeLived { get; set; }

        public Projectile(Vector2 direction, Entity parent)
        {
            Parent = parent;
            Position = parent.MuzzlePoint;
            Speed = parent.ProjectileSpeed;
            Direction = direction;
            Texture = ContentManager.ButtonTexture;
            Size = new System.Drawing.Size(10, 10);
            TTL = parent.RangeInSeconds;

            int spreadFactor = new Random().Next(-parent.Spread, parent.Spread);

            if (Direction.Y == 0)
                Direction = new Vector2(Direction.X, spreadFactor / 100f);
            else
                Direction = new Vector2(spreadFactor / 100f, Direction.Y);
        }

        public override void Update(GameTime gameTime)
        {
            TimeLived += gameTime.ElapsedGameTime.TotalSeconds;

            if (TimeLived >= TTL)
            {
                IsRemoved = true;
                return;
            }

            Position += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }

        public override void OnCollision(Sprite sprite, GameTime gameTime)
        {
            if (sprite == Parent) return;
            if (sprite == this) return;
            if (sprite is Projectile) return;
            if (!(sprite is Entity)) return;
            if (IsRemoved) return;

            ((Entity)sprite).CurrentHealth -= ((Entity)Parent).Damage;
            IsRemoved = true;
        }

    }
}