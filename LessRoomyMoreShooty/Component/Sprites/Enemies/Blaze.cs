using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using LessRoomyMoreShooty.Models;

namespace LessRoomyMoreShooty.Component.Sprites.Enemies
{
    public class Blaze : Enemy
    {
        public Blaze(Player player) : base(player)
        {
            Animations = new Dictionary<string, Animation>
            {
                {"idle", new Animation(ContentManager.BlazeIdleAnimation, 12) { FrameSpeed = 0.1f } }
            };

            AnimationManager.Play(Animations["idle"]);
            AnimationManager.Parent = this;

            MaxHealth = 4;
            CurrentHealth = 4;
            Damage = 2;
            AttackSpeedInSeconds = 0.2;
            MaxAmmo = 3;
            CurrentAmmo = 3;
            ReloadTimeInSeconds = 2;
            ProjectileSpeed = 100;
            ProjectileCount = 1;
            RangeInSeconds = 4;
            Spread = 20;
            MaxSpeed = 50;
            Size = new Size(56, 84);

            Texture = ContentManager.ButtonTexture;
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
        }

        private void Shoot(GameTime gameTime)
        {
            if (DistanceToPlayer <= 500)
                Shoot(gameTime, Direction, texture: ContentManager.BlazeBulletTexture, size: new Size(30, 30), soundEffect: ContentManager.BlazeShootSoundEffect);
        }

        private void Move()
        {
            if (DistanceToPlayer > 500)
                Speed = MaxSpeed;
            else
                Speed = 0;
        }

        protected override void OnLevelUp(int level)
        {
            MaxHealth += (int)(level * 0.1);
            CurrentHealth = MaxHealth;
        }
    }
}
