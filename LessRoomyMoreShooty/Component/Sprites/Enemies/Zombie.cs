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
            Size = new Size(56, 84);

            Texture = ContentManager.ButtonTexture;
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

        private void SetDirectionToPlayer()
        {

            Direction = DirectionToPlayer;
        }

        private void Move()
        {
            if (DistanceToPlayer.Length() > 65)
                Speed = 200;
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

                Player.CurrentHealth -= Damage;
                CurrentAmmo -= 1;
                AttackSpeedTimer = 0;
            }

            base.OnCollision(sprite, gameTime);
        }

    }
}
