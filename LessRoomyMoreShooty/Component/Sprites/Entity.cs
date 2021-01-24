using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace LessRoomyMoreShooty.Component.Sprites
{
    public class Entity : Sprite
    {
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int Damage { get; set; }
        public double AttackSpeedInSeconds { get; set; }
        public double AttackSpeedTimer { get; set; }
        public int ProjectileSpeed { get; set; }
        public int MaxAmmo { get; set; }
        public int CurrentAmmo { get; set; }
        public double ReloadTimeInSeconds { get; set; }
        public double CurrentReloadTimeSeconds { get; set; }
        public double RangeInSeconds { get; set; }
        public int Spread { get; set; }

        public float Acceleration { get; set; }

        public Vector2 MuzzlePoint { get; set; }

        public override void Update(GameTime gameTime)
        {
            MuzzlePoint = Position;
            Position += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }

    }
}