using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LessRoomyMoreShooty.Component.Sprites.Enemies
{
    public class Creeper : Enemy
    {
        public Creeper(Player player) : base(player)
        {
            MaxHealth = 3;
            CurrentHealth = 3;
            Damage = 3;
            AttackSpeedInSeconds = 1;
            MaxAmmo = 1;
            CurrentAmmo = 1;
            ReloadTimeInSeconds = 0.75;
            ProjectileSpeed = 300;
            RangeInSeconds = 1;
            Spread = 45;
            MaxSpeed = 150;
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

        private void Move()
        {
            Speed = MaxSpeed;
        }

        public override void OnCollision(Sprite sprite, GameTime gameTime)
        {
            if (sprite is Player)
            {
                if (!CanShoot || IsRemoved) return;
                CanShoot = false;
                IsRemoved = true;

                ParticleManager.GenerateNewParticle(Microsoft.Xna.Framework.Color.White, MuzzlePoint, new List<Texture2D>(){ ContentManager.DamageUpTexture}, 30, 20);
                AudioManager.PlayEffect(ContentManager.ExplosionSoundEffect, 0.15f);

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
