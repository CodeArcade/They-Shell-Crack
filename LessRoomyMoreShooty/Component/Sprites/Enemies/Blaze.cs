using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LessRoomyMoreShooty.Component.Sprites.Enemies
{
    public class Blaze : Enemy
    {
        public Blaze(Player player) : base(player)
        {
            MaxHealth = 4;
            CurrentHealth = 4;
            Damage = 1;
            AttackSpeedInSeconds = 0.2;
            MaxAmmo = 3;
            CurrentAmmo = 3;
            ReloadTimeInSeconds = 1.5;
            ProjectileSpeed = 200;
            ProjectileCount = 1;
            RangeInSeconds = 3;
            Spread = 25;
            MaxSpeed = 100;
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
            if (DistanceToPlayer <= 500)
                Shoot(gameTime, Direction);
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
            MaxHealth += (int)(level * 0.2);
            CurrentHealth = MaxHealth;

            Damage += (int)(level * 0.1); 
        }
    }
}
