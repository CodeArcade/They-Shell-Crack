using LessRoomyMoreShooty.Models;
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
        private bool DidExplode { get; set; }

        public Creeper(Player player) : base(player)
        {

            Animations = new Dictionary<string, Animation>
            {
                {"idle", new Animation(ContentManager.CreeperIdleAnimation, 1) { FrameSpeed = 0.05f } },
                {"walk", new Animation(ContentManager.CreeperWalkAnimation, 2) { FrameSpeed = 0.1f } },
                {"explosion", new Animation(ContentManager.FloorExplosionTexture, 6) { FrameSpeed = 0.01f, IsLooping = false} }
            };

            AnimationManager.Play(Animations["walk"]);
            AnimationManager.Parent = this;

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

            Texture = ContentManager.TransparentTexture;
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
            if (DidExplode) return;

            if (Direction.X > 0)
                AnimationManager.Flip = false;
            else if (Direction.X < 0)
                AnimationManager.Flip = true;

            Speed = MaxSpeed;
        }

        public override void OnCollision(Sprite sprite, GameTime gameTime)
        {
            if (DidExplode)
            {
                if (!AnimationManager.IsPlaying) IsRemoved = true;
                return;
            }

            if (sprite is Player)
            {
                if (!CanShoot || IsRemoved) return;
                CanShoot = false;
                DidExplode = true;

                AnimationManager.Play(Animations["explosion"]);

                ParticleManager.GenerateNewParticle(Microsoft.Xna.Framework.Color.White, MuzzlePoint, new List<Texture2D>() { ContentManager.BlazeBulletTexture }, 10, 20);
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
