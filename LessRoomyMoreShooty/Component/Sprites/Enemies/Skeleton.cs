﻿using Microsoft.Xna.Framework;
using System;
using System.Drawing;

namespace LessRoomyMoreShooty.Component.Sprites.Enemies
{
    public class Skeleton : Enemy
    {
        public Skeleton(Player player) : base(player)
        {
            MaxHealth = 5;
            CurrentHealth = 5;
            Damage = 1;
            AttackSpeedInSeconds = 0.5;
            MaxAmmo = 1;
            CurrentAmmo = 1;
            ReloadTimeInSeconds = 0.75;
            ProjectileSpeed = 300;
            ProjectileCount = 1;
            RangeInSeconds = 1;
            Spread = 5;
            MaxSpeed = 180;
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

        private void SetDirectionToPlayer()
        {
            Direction = DirectionToPlayer;
        }

        private void Shoot(GameTime gameTime)
        {
            if (DistanceToPlayer <= 250)
                Shoot(gameTime, Direction);
        }

        private void Move()
        {
            if (DistanceToPlayer > 250)
                Speed = MaxSpeed;
            else
                Speed = 0;
        }

        protected override void OnLevelUp(int level)
        {
            MaxHealth += (int)(level * 0.2);
            CurrentHealth = MaxHealth;

            Damage += (int)(level * 0.1);
        }
    }
}
