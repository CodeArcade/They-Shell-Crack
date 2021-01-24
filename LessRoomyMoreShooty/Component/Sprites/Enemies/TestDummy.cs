using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LessRoomyMoreShooty.Component.Sprites.Enemies
{
    public class TestDummy : Enemy
    {
        public TestDummy()
        {
            MaxHealth = 5;
            CurrentHealth = 5;
            Damage = 1;
            AttackSpeedInSeconds = 1;
            ProjectileSpeed = 300;
            MaxAmmo = 5;
            CurrentAmmo = 5;
            ReloadTimeInSeconds = 5;
            RangeInSeconds = 0.5;
            Spread = 45;
            Texture = ContentManager.PlayerTexture;
        }

        public override void Update(GameTime gameTime)
        {
            Shoot(gameTime, new Vector2(1,0), 5);

            base.Update(gameTime);
        }

    }
}
