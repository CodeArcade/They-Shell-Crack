using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;

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
            ReloadTimeInSeconds = 0.5;
            ProjectileSpeed = 300;
            RangeInSeconds = 1;
            Spread = 3;
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
                Speed = 200;
            else
                Speed = 0;
        }

    }
}
