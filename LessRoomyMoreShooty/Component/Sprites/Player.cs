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
        private readonly AnimationManager GunAnimationManager;

        private KeyboardState CurrentKeyboard { get; set; }
        private Dictionary<string, Animation> Animations { get; set; }

        public double IFrames { get; } = 2;
        public double IFramesTimer { get; set; }
        public bool CanTakeDamage => IFramesTimer > IFrames;

        public float PlayerScale { get; set; } = 1;

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
                { "walk", new Animation(ContentManager.PlayerWalkAnimation, 2) { FrameSpeed = 0.15f } },
                { "idle", new Animation(ContentManager.PlayerIdleAnimation, 1) { FrameSpeed = 0.1f } }
            };

            Texture = ContentManager.TransparentTexture;
            Size = new Size((int)(56 * PlayerScale), (int)(84 * PlayerScale));

            AnimationManager.Scale = PlayerScale;
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
            RangeInSeconds = 2;
            ProjectileSpeed = 300;
            ProjectileCount = 1;
            Damage = 1;
            ShootAll = false;

            IFramesTimer = IFrames;

            HitboxSize = new Size((int)(45 * PlayerScale), (int)(60 * PlayerScale));
            HitBoxXOffSet = (int)(5 * PlayerScale);
            HitBoxYOffSet = (int)(24 * PlayerScale);

            GunAnimationManager = new AnimationManager()
            {
                Scale = 0.5f
            };
            GunAnimationManager.Play(new Animation(ContentManager.GunTexture, 1));
            GunAnimationManager.Position = new Vector2(-20, -20);
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

            if (GunAnimationManager.IsPlaying) GunAnimationManager.Update(gameTime);
            base.Update(gameTime);
            MuzzlePoint = GunAnimationManager.Position;
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
                if (GunAnimationManager.IsPlaying) GunAnimationManager.Draw(spriteBatch);
                return;
            }

            base.Draw(gameTime, spriteBatch);
            if (GunAnimationManager.IsPlaying) GunAnimationManager.Draw(spriteBatch);

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
                GunAnimationManager.Rotation = 0;
                GunAnimationManager.Flip = true;
                GunAnimationManager.Position = new Vector2(Position.X - 20, Position.Y + Size.Height / 1.8f);
                AnimationManager.Flip = true;
                AnimationManager.Play(Animations["walk"]);
                Direction = new Vector2(-1, Direction.Y);
            }
            else if (IsKeyDown(Right) && !IsKeyDown(Left))
            {
                GunAnimationManager.Rotation = 0;
                GunAnimationManager.Flip = false;
                GunAnimationManager.Position = new Vector2(Position.X + Size.Width - 20, Position.Y + Size.Height / 1.8f);
                AnimationManager.Flip = false;
                AnimationManager.Play(Animations["walk"]);
                Direction = new Vector2(1, Direction.Y);
            }
            else
            {
                Direction = new Vector2(0, Direction.Y);
            }

            if (IsKeyDown(Up) && !IsKeyDown(Down))
            {
                GunAnimationManager.Rotation = -90;
                GunAnimationManager.Flip = false;
                GunAnimationManager.Position = new Vector2(Position.X + Size.Width / 2, Position.Y + 20);
                AnimationManager.Play(Animations["walk"]);
                Direction = new Vector2(Direction.X, -1);
            }
            else if (IsKeyDown(Down) && !IsKeyDown(Up))
            {
                GunAnimationManager.Rotation = 90;
                GunAnimationManager.Flip = false;
                GunAnimationManager.Position = new Vector2(Position.X + Size.Width / 2, Position.Y + Size.Height - 20);
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
                GunAnimationManager.Position = new Vector2(Position.X - 20, Position.Y + Size.Height / 1.8f);
                GunAnimationManager.Rotation = 0;
                GunAnimationManager.Flip = true;
                Shoot(gameTime, new Vector2(-1, 0));
            }
            else if (IsKeyDown(ShootRight))
            {
                GunAnimationManager.Position = new Vector2(Position.X + Size.Width - 20, Position.Y + Size.Height / 1.8f);
                GunAnimationManager.Rotation = 0;
                GunAnimationManager.Flip = false;
                Shoot(gameTime, new Vector2(1, 0));
            }
            else if (IsKeyDown(ShootUp))
            {
                GunAnimationManager.Position = new Vector2(Position.X + Size.Width / 2, Position.Y + 20);
                GunAnimationManager.Rotation = -90;
                GunAnimationManager.Flip = false;
                Shoot(gameTime, new Vector2(0, -1));
            }
            else if (IsKeyDown(ShootDown))
            {
                GunAnimationManager.Position = new Vector2(Position.X + Size.Width / 2, Position.Y + Size.Height - 20);
                GunAnimationManager.Rotation = 90;
                GunAnimationManager.Flip = false;
                Shoot(gameTime, new Vector2(0, 1));
            }
            else { return; }
        }

        protected override void Shoot(GameTime gameTime, Vector2 direction, int bulletCount = -1, Texture2D texture = null, Size? size = null)
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