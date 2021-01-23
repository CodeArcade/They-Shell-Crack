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
        public Keys ShootLeft { get; set; } = Keys.Left;
        public Keys ShootRight { get; set; } = Keys.Right;
        public Keys ShootUp { get; set; } = Keys.Up;
        public Keys ShootDown { get; set; } = Keys.Down;

        public Player()
        {
            Texture = ContentManager.PlayerTexture;
            CurrentHealth = 10;
            MaxSpeed = 200;
            Acceleration = 100;
            MaxAmmo = 10;
            CurrentAmmo = MaxAmmo;
            ReloadTimeInSeconds = 0.8;
            AttackSpeedInSeconds = 0.25;
            Spread = 3;
            RangeInSeconds = 2;
            ProjectileSpeed = 300;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            CurrentKeyboard = Keyboard.GetState();

            MuzzlePoint = Position;

            CheckHealth();
            Move();
            if (CurrentAmmo <= 0)
                Reload(gameTime);
            else
                Shoot(gameTime);

            base.Update(gameTime);
        }

        private void Reload(GameTime gameTime)
        {
            if (CurrentReloadTimeSeconds >= ReloadTimeInSeconds)
            {
                CurrentAmmo = MaxAmmo;
                CurrentReloadTimeSeconds = 0;
                return;
            }

            CurrentReloadTimeSeconds += gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void CheckHealth()
        {
            if (CurrentHealth > 0) return;
            IsRemoved = true;
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
            if (AttackSpeedTimer < AttackSpeedInSeconds)
            {
                AttackSpeedTimer += gameTime.ElapsedGameTime.TotalSeconds;
                return;
            }

            Projectile projectile;

            if (IsKeyDown(ShootLeft))
            {
                projectile = new Projectile(new Vector2(-1, 0), this);
            }
            else if (IsKeyDown(ShootRight))
            {
                projectile = new Projectile(new Vector2(1, 0), this);
            }
            else if (IsKeyDown(ShootUp))
            {
                projectile = new Projectile(new Vector2(0, -1), this);
            }
            else if (IsKeyDown(ShootDown))
            {
                projectile = new Projectile(new Vector2(0, 1), this);
            }
            else { return; }

            CurrentAmmo -= 1;
            AttackSpeedTimer = 0;

            CurrentState.AddComponent(projectile);
        }

        public override void OnCollision(Sprite sprite, GameTime gameTime)
        {
            if(sprite is Item.Item && !sprite.IsRemoved)
            {
                sprite.IsRemoved = true;
                ((Item.Item)sprite).OnPickup(this);
            }
        }

        private bool IsKeyDown(Keys key) => CurrentKeyboard.IsKeyDown(key);

    }
}