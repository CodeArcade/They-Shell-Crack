using Microsoft.Xna.Framework;

namespace LessRoomyMoreShooty.Component.Sprites.Enemies
{
    public class TestDummy : Enemy
    {
        public TestDummy(Player player) : base(player)
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
            Texture = ContentManager.PlayerIdleAnimation;
        }

        public override void Update(GameTime gameTime)
        {
            if (IsActive) Shoot(gameTime, new Vector2(1, 0), 5);

            base.Update(gameTime);
        }

    }
}
