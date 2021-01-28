using LessRoomyMoreShooty.Manager;
using LessRoomyMoreShooty.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;

namespace LessRoomyMoreShooty.Component.Sprites
{
    public class Player : Entity
    {
        private KeyboardState CurrentKeyboard { get; set; }
        private Dictionary<string, Animation> Animations { get; set; }

        public double IFrames { get; } = 2;
        public double IFramesTimer { get; set; }
        public bool CanTakeDamage => IFramesTimer > IFrames;

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
            Animations = new Dictionary<string, Animation>
            {
                { "walk", new Animation(ContentManager.PlayerWalkAnimation, 8) { FrameSpeed = 0.05f } },
                { "idle", new Animation(ContentManager.PlayerIdleAnimation, 4) { FrameSpeed = 0.1f } }
            };

            Texture = ContentManager.TransparentTexture;
            Size = new Size((int)(56 * 0.5), (int)(84 * 0.8));

            AnimationManager.Scale = 0.5f;
            AnimationManager.Parent = this;
            AnimationManager.Play(Animations["idle"]);

            MaxHealth = 10;
            CurrentHealth = MaxHealth;
            MaxSpeed = 200;
            Acceleration = 100;
            MaxAmmo = 10;
            CurrentAmmo = MaxAmmo;
            ReloadTimeInSeconds = 0.8;
            AttackSpeedInSeconds = 0.3;
            AttackSpeedTimer = AttackSpeedInSeconds;
            Spread = 3;
            RangeInSeconds = 2.5;
            ProjectileSpeed = 300;
            ProjectileCount = 1;
            Damage = 1;
            ShootAll = false;

            IFramesTimer = IFrames;

            HitboxSize = new Size((int)(45 * 0.5), (int)(60 * 0.5));
            HitBoxXOffSet = (int)(5 * 0.5);
            HitBoxYOffSet = (int)(24 * 0.5);
        }

        public override void Update(GameTime gameTime)
        {
            IFramesTimer += gameTime.ElapsedGameTime.TotalSeconds;

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

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!CanTakeDamage)
            {
                if (AnimationManager.IsPlaying)
                    AnimationManager.Draw(spriteBatch, Color.Red);
                else
                    spriteBatch.Draw(Texture, Rectangle, Color.Red);

                ParticleManager.Draw(gameTime, spriteBatch);
                return;
            }

            base.Draw(gameTime, spriteBatch);

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
                AnimationManager.Play(Animations["idle"]);
                Decelerate();
                return;
            }

            if (IsKeyDown(Left) && !IsKeyDown(Right))
            {
                AnimationManager.Flip = false;
                AnimationManager.Play(Animations["walk"]);
                Direction = new Vector2(-1, Direction.Y);
            }
            else if (IsKeyDown(Right) && !IsKeyDown(Left))
            {
                AnimationManager.Flip = true;
                AnimationManager.Play(Animations["walk"]);
                Direction = new Vector2(1, Direction.Y);
            }
            else
            {
                Direction = new Vector2(0, Direction.Y);
            }

            if (IsKeyDown(Up) && !IsKeyDown(Down))
            {
                AnimationManager.Play(Animations["walk"]);
                Direction = new Vector2(Direction.X, -1);
            }
            else if (IsKeyDown(Down) && !IsKeyDown(Up))
            {
                AnimationManager.Play(Animations["walk"]);
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

        protected override void Shoot(GameTime gameTime, Vector2 direction, int bulletCount = -1)
        {
            if (!CanShoot) return;

            if (CurrentAmmo == 1) AudioManager.PlayEffect(ContentManager.LastBulletSoundEffect, 0.25f);

            int projectileCount = ShootAll ? CurrentAmmo : ProjectileCount;
            base.Shoot(gameTime, direction, projectileCount);
            if (ShootAll) CurrentAmmo = 0;
        }

        private bool IsKeyDown(Keys key) => CurrentKeyboard.IsKeyDown(key);

        public override void TakeDamage(int damage)
        {
            if (!CanTakeDamage) return;

            IFramesTimer = 0;
            base.TakeDamage(damage);
        }

    }
}