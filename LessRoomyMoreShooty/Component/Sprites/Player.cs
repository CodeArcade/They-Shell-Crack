using LessRoomyMoreShooty.Manager;
using LessRoomyMoreShooty.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;

namespace LessRoomyMoreShooty.Component.Sprites
{
    public class Player : Entity
    {
        public readonly AnimationManager GunAnimationManager;

        private KeyboardState CurrentKeyboard { get; set; }
        private Dictionary<string, Animation> Animations { get; set; }

        public double IFrames { get; } = 0.8;
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
        public bool useController = false;

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
            RangeInSeconds = 1.6;
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
                Scale = 0.5f,
                IsPlaying = true
            };
            GunAnimationManager.Play(new Animation(ContentManager.GunTexture, 1));
            GunAnimationManager.Position = new Vector2(-20, -20);
            UpdateMuzzlePoint();
        }

        public override void Update(GameTime gameTime)
        {
            useController = Controller.IsInUse();
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
            if (useController)
            {
                ControllerMove();
            } 
            else
            {
                KeyboardMove();
            }

            if (Speed < MaxSpeed) Speed += Acceleration;
            if (Direction == Vector2.Zero) AnimationManager.Play(Animations["idle"]);
        }

        private void ControllerMove()
        {
            Vector2 leftStick = GamePad.GetState(0).ThumbSticks.Left;
            Direction = leftStick;
            
            // moving primarily left or right
            if (Math.Abs(leftStick.X) > Math.Abs(leftStick.Y))
            {
                if (leftStick.X < 0)
                {
                    UpdateGunPosition(Sprites.Direction.Right);
                    return;
                }

                UpdateGunPosition(Sprites.Direction.Left);
                return;
            }
            
            // moving primarily up or down
            if (leftStick.Y < 0)
            {
                UpdateGunPosition(Sprites.Direction.Up);
                return;
            }

            UpdateGunPosition(Sprites.Direction.Down);
        }

        private void KeyboardMove()
        {
            if (!IsKeyDown(Left) && !IsKeyDown(Right) && !IsKeyDown(Up) && !IsKeyDown(Down))
            {
                AnimationManager.Play(Animations["idle"]);
                Decelerate();
                return;
            }

            if (IsKeyDown(Left) && !IsKeyDown(Right))
            {
                UpdateGunPosition(Sprites.Direction.Left);
                AnimationManager.Flip = true;
                AnimationManager.Play(Animations["walk"]);
                Direction = new Vector2(-1, Direction.Y);
            }
            else if (IsKeyDown(Right) && !IsKeyDown(Left))
            {
                UpdateGunPosition(Sprites.Direction.Right);
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
                UpdateGunPosition(Sprites.Direction.Up);
                AnimationManager.Play(Animations["walk"]);
                Direction = new Vector2(Direction.X, -1);
            }
            else if (IsKeyDown(Down) && !IsKeyDown(Up))
            {
                UpdateGunPosition(Sprites.Direction.Down);
                AnimationManager.Play(Animations["walk"]);
                Direction = new Vector2(Direction.X, 1);
            }
            else
            {
                Direction = new Vector2(Direction.X, 0);
            }
        }

        private void Decelerate()
        {
            if (Speed <= 0) Direction = Vector2.Zero;
            if (Speed > 0) Speed -= Acceleration / 3;
        }

        private void Shoot(GameTime gameTime)
        {
            if (IsKeyDown(ShootLeft))
            {
                UpdateGunPosition(Sprites.Direction.Left);
                UpdateMuzzlePoint();
                Shoot(gameTime, new Vector2(-1, 0));
            }
            else if (IsKeyDown(ShootRight))
            {
                UpdateGunPosition(Sprites.Direction.Right);
                UpdateMuzzlePoint();
                Shoot(gameTime, new Vector2(1, 0));
            }
            else if (IsKeyDown(ShootUp))
            {
                UpdateGunPosition(Sprites.Direction.Up);
                UpdateMuzzlePoint();
                Shoot(gameTime, new Vector2(0, -1));
            }
            else if (IsKeyDown(ShootDown))
            {
                UpdateGunPosition(Sprites.Direction.Down);
                UpdateMuzzlePoint();
                Shoot(gameTime, new Vector2(0, 1));
            }
            else { return; }
        }

        public void UpdateGunPosition(Direction direction)
        {
            switch(direction)
            {
                case Sprites.Direction.Up:
                    GunAnimationManager.Position = new Vector2(Position.X + Size.Width / 2, Position.Y + 20);
                    GunAnimationManager.Rotation = -90;
                    GunAnimationManager.Flip = false;
                    break;
                case Sprites.Direction.Down:
                    GunAnimationManager.Position = new Vector2(Position.X + Size.Width / 2, Position.Y + Size.Height - 20);
                    GunAnimationManager.Rotation = 90;
                    GunAnimationManager.Flip = false;
                    break;
                case Sprites.Direction.Left:
                    GunAnimationManager.Position = new Vector2(Position.X - 20, Position.Y + Size.Height / 1.8f);
                    GunAnimationManager.Rotation = 0;
                    GunAnimationManager.Flip = true;
                    break;
                case Sprites.Direction.Right:
                    GunAnimationManager.Position = new Vector2(Position.X + Size.Width - 20, Position.Y + Size.Height / 1.8f);
                    GunAnimationManager.Rotation = 0;
                    GunAnimationManager.Flip = false;
                    break;
            }
        }

        protected override void Shoot(GameTime gameTime, Vector2 direction, int bulletCount = -1, Texture2D texture = null, Size? size = null, SoundEffect soundEffect = null)
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

        private void UpdateMuzzlePoint()
        {
            if (GunAnimationManager.Rotation == 0)
            {
                MuzzlePoint = GunAnimationManager.Flip ?
                GunAnimationManager.Position :
                new Vector2(GunAnimationManager.Position.X + GunAnimationManager.AnimationRectangle.Width, GunAnimationManager.Position.Y + 6);
            } else
            {
                MuzzlePoint = GunAnimationManager.Rotation > 0 ? 
                    new Vector2(GunAnimationManager.Position.X - 20, GunAnimationManager.Position.Y + GunAnimationManager.AnimationRectangle.Height) :
                    new Vector2(GunAnimationManager.Position.X - 10, GunAnimationManager.Position.Y - GunAnimationManager.AnimationRectangle.Height - 10);
            }
        }
    }
} 