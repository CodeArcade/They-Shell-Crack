using LessRoomyMoreShooty.Component.Effects;
using LessRoomyMoreShooty.Component.Sprites.Enemies;
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

            Random random = new Random();

            Direction = new Vector2(Direction.X + (random.Next(-parent.Spread, parent.Spread) / 100f), Direction.Y + (random.Next(-parent.Spread, parent.Spread) / 100f));
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

            if (!(Parent is Player) && sprite is Enemy) return;

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

            ((Entity)sprite).TakeDamage(((Entity)Parent).Damage);
            IsRemoved = true;
        }

    }
}