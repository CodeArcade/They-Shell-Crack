using LessRoomyMoreShooty.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;

namespace LessRoomyMoreShooty.Component.Sprites.Enemies
{
    public class Skeleton : Enemy
    {
        private Texture2D BowTexture { get; set; }

        public Skeleton(Player player) : base(player)
        {
            Animations = new Dictionary<string, Animation>
            {
                {"idle", new Animation(ContentManager.SkeletonIdleAnimation, 1) { FrameSpeed = 0.05f } },
                {"walk", new Animation(ContentManager.SkeletonWalkAnimation, 2) { FrameSpeed = 0.1f } },
            };

            BowTexture = ContentManager.BowTexture;
            AnimationManager.Play(Animations["walk"]);
            AnimationManager.Parent = this;

            MaxHealth = 4;
            CurrentHealth = 4;
            Damage = 2;
            AttackSpeedInSeconds = 0.5;
            MaxAmmo = 1;
            CurrentAmmo = 1;
            ReloadTimeInSeconds = 0.75;
            ProjectileSpeed = 150;
            ProjectileCount = 1;
            RangeInSeconds = 1.5;
            Spread = 5;
            MaxSpeed = 100;
            Size = new Size(56, 84);

            Texture = ContentManager.ButtonTexture;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            spriteBatch.Draw(BowTexture, new Vector2((int)Position.X + (Size.Width / 2), (int)Position.Y + (Size.Height / 2) + 20), null, Color.White, AngleToPlayer, new Vector2((BowTexture.Width / 2f) * 0.8f, (BowTexture.Height / 2f) * 0.8f), 0.8f, SpriteEffects.None, 0);
        }

        public override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                SetDirectionToPlayer();
                Move();
                Shoot(gameTime);
            }

            base.Update(gameTime);
            MuzzlePoint = new Vector2((int)Position.X + (Size.Width / 2), (int)Position.Y + (Size.Height / 2) + 20);
        }

        private void Shoot(GameTime gameTime)
        {
            if (DistanceToPlayer <= 270)
                Shoot(gameTime, Direction, texture: ContentManager.SkeletonBulletTexture);
        }

        private void Move()
        {
            if (DistanceToPlayer > 220)
            {
                AnimationManager.Play(Animations["walk"]);

                if (Direction.X > 0)
                    AnimationManager.Flip = false;
                else if (Direction.X < 0)
                    AnimationManager.Flip = true;

                Speed = MaxSpeed;
            }
            else
            {
                Speed = 0;
                AnimationManager.Play(Animations["idle"]);
            }
        }

        protected override void OnLevelUp(int level)
        {
            MaxHealth += (int)(level * 0.1);
            CurrentHealth = MaxHealth;
        }
    }
}
