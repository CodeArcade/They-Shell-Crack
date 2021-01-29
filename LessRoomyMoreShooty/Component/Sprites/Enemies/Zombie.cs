using LessRoomyMoreShooty.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LessRoomyMoreShooty.Component.Sprites.Enemies
{
    public class Zombie : Enemy
    {

        public Zombie(Player player) : base(player)
        {
            Animations = new Dictionary<string, Animation>
            {
                { "walk", new Animation(ContentManager.ZombieWalkAnimation, 2) { FrameSpeed = 0.15f } },
                { "idle", new Animation(ContentManager.ZombieIdleAnimation, 1) { FrameSpeed = 0.1f } }
            };

            MaxHealth = 5;
            CurrentHealth = 5;
            Damage = 1;
            AttackSpeedInSeconds = 1;
            MaxAmmo = 1;
            CurrentAmmo = 1;
            ReloadTimeInSeconds = 0.75;
            ProjectileSpeed = 300;
            RangeInSeconds = 1;
            Spread = 45;
            MaxSpeed = 100;
            Size = new Size(28, 42);

            Texture = ContentManager.ZombieIdleAnimation;
        }

        public override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                SetDirectionToPlayer();
                Move();
            }

            base.Update(gameTime);
        }

        private void Move()
        {
            if (DistanceToPlayer > 65)
                Speed = MaxSpeed;
            else
                Speed = 0;
        }

        public override void OnCollision(Sprite sprite, GameTime gameTime)
        {
            if (sprite is Player)
            {
                if (!CanShoot) return;
                CanShoot = false;

                ParticleManager.GenerateNewParticle(Microsoft.Xna.Framework.Color.White, MuzzlePoint, ContentManager.ShootParticle, 3, 5);
                AudioManager.PlayEffect(ContentManager.ShootSoundEffect, 0.15f);

                Player.TakeDamage(Damage);
                CurrentAmmo -= 1;
                AttackSpeedTimer = 0;
            }

            base.OnCollision(sprite, gameTime);
        }

        protected override void OnLevelUp(int level)
        {
            MaxHealth += (int)(level * 0.1);
            CurrentHealth = MaxHealth;
        }

    }
}
