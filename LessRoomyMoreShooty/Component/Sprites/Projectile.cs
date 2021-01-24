using LessRoomyMoreShooty.Component.Sprites.Environment;
using LessRoomyMoreShooty.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

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
            Texture = ContentManager.BulletTexture;
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
            if (IsRemoved) return;

            if (sprite is Obstacle)
            {
                ParticleManager.GenerateNewParticle(Color.White, Position, ContentManager.ObstacleHitParticle, 5, 10);
                AudioManager.PlayEffect(ContentManager.ObstacleHitSoundEffect, 0.25f);
                IsRemoved = true;
                return;
            }

            if (!(sprite is Entity)) return;

            ParticleManager.GenerateNewParticle(Color.White, Position, ContentManager.EntityHitParticle, 5, 10);
            AudioManager.PlayEffect(ContentManager.EntityHitSoundEffect, 0.25f);
            ((Entity)sprite).CurrentHealth -= ((Entity)Parent).Damage;
            IsRemoved = true;
        }

    }
}