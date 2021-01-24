using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LessRoomyMoreShooty.Component.Sprites.Enemies
{
    public class TestDummy : Entity
    {
        public TestDummy()
        {
            MaxHealth = 100;
            CurrentHealth = 100;
            Damage = 1;
            AttackSpeedInSeconds = 0.5;
            ProjectileSpeed = 300;
            MaxAmmo = 5;
            CurrentAmmo = 5;
            ReloadTimeInSeconds = 1;
            RangeInSeconds = 3;
            Spread = 5;
        }

        public override void Update(GameTime gameTime)
        {


            base.Update(gameTime);
        }

    }
}
