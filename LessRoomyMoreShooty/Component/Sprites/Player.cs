using LessRoomyMoreShooty.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LessRoomyMoreShooty.Component.Sprites
{
    public class Player : Entity
    {
        private KeyboardState CurrentKeyboard { get; set; }

        public Keys Left { get; set; } = Keys.A;
        public Keys Right { get; set; } = Keys.D;
        public Keys Up { get; set; } = Keys.W;
        public Keys Down { get; set; } = Keys.S;
        public Keys Reload { get; set; } = Keys.R;
        public Keys ShootLeft { get; set; } = Keys.Left;
        public Keys ShootRight { get; set; } = Keys.Right;
        public Keys ShootUp { get; set; } = Keys.Up;
        public Keys ShootDown { get; set; } = Keys.Down;

        public Player()
        {
            Texture = ContentManager.PlayerTexture;
            MaxHealth = 10;
            CurrentHealth = MaxHealth;
            MaxSpeed = 200;
            Acceleration = 100;
            MaxAmmo = 10;
            CurrentAmmo = MaxAmmo;
            ReloadTimeInSeconds = 0.8;
            AttackSpeedInSeconds = 0.25;
            AttackSpeedTimer = AttackSpeedInSeconds;
            Spread = 3;
            RangeInSeconds = 2;
            ProjectileSpeed = 300;
            Damage = 1;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            CurrentKeyboard = Keyboard.GetState();

            Move();

            if ((IsKeyDown(Reload) && CanShoot) && CurrentAmmo != MaxAmmo)
            {
                CurrentAmmo = 0;
                DoReload(gameTime);
            }

            Shoot(gameTime);

            base.Update(gameTime);
        }

        protected override void DoReload(GameTime gameTime)
        {
            if (CurrentReloadTimeSeconds >= ReloadTimeInSeconds)
            {
                AudioManager.PlayEffect(ContentManager.ReloadSoundEffect, 0.25f);
            }

            base.DoReload(gameTime);
        }

        private void Move()
        {
            if (!IsKeyDown(Left) && !IsKeyDown(Right) && !IsKeyDown(Up) && !IsKeyDown(Down))
            {
                Decelerate();
                return;
            }

            if (IsKeyDown(Left) && !IsKeyDown(Right))
            {
                Direction = new Vector2(-1, Direction.Y);
            }
            else if (IsKeyDown(Right) && !IsKeyDown(Left))
            {
                Direction = new Vector2(1, Direction.Y);
            }
            else
            {
                Direction = new Vector2(0, Direction.Y);
            }

            if (IsKeyDown(Up) && !IsKeyDown(Down))
            {
                Direction = new Vector2(Direction.X, -1);
            }
            else if (IsKeyDown(Down) && !IsKeyDown(Up))
            {
                Direction = new Vector2(Direction.X, 1);
            }
            else
            {
                Direction = new Vector2(Direction.X, 0);
            }

            if (Speed < MaxSpeed) Speed += Acceleration;
        }

        private void Decelerate()
        {
            if (Speed <= 0) Direction = new Vector2(0, 0);
            if (Speed > 0) Speed -= Acceleration / 3;
        }

        private void Shoot(GameTime gameTime)
        {
            if (IsKeyDown(ShootLeft))
            {
                Shoot(gameTime, new Vector2(-1, 0));
            }
            else if (IsKeyDown(ShootRight))
            {
                Shoot(gameTime, new Vector2(1, 0));
            }
            else if (IsKeyDown(ShootUp))
            {
                Shoot(gameTime, new Vector2(0, -1));
            }
            else if (IsKeyDown(ShootDown))
            {
                Shoot(gameTime, new Vector2(0, 1));
            }
            else { return; }
        }

        protected override void Shoot(GameTime gameTime, Vector2 direction, int bulletCount = 1)
        {
            if (!CanShoot) return;

            if(CurrentAmmo == 1) AudioManager.PlayEffect(ContentManager.LastBulletSoundEffect, 0.25f);

            base.Shoot(gameTime, direction, bulletCount);
        }

        public override void OnCollision(Sprite sprite, GameTime gameTime)
        {
            if (sprite is Item.Item && !sprite.IsRemoved)
            {
                sprite.IsRemoved = true;
                ((Item.Item)sprite).OnPickup(this);
            }

            base.OnCollision(sprite, gameTime);
        }

        private bool IsKeyDown(Keys key) => CurrentKeyboard.IsKeyDown(key);

    }
}