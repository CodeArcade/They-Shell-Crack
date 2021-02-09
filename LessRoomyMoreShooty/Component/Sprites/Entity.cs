using LessRoomyMoreShooty.Component.Effects;
using LessRoomyMoreShooty.Component.Sprites.Enemies;
using LessRoomyMoreShooty.Component.Sprites.Environment;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;

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
        public int ProjectileCount { get; set; }
        public int MaxAmmo { get; set; }
        public int CurrentAmmo { get; set; }
        public double ReloadTimeInSeconds { get; set; }
        public double CurrentReloadTimeSeconds { get; set; }
        public double RangeInSeconds { get; set; }
        public int Spread { get; set; }
        public float Acceleration { get; set; }
        public Vector2 MuzzlePoint { get; set; }
        public bool CanShoot { get; set; } = true;
        public bool ShootAll { get; set; }

        protected virtual Texture2D SpotShadow => ContentManager.SpotShadowTexture; 


        public override void Update(GameTime gameTime)
        {
            CheckHealth();

            if (CurrentAmmo <= 0)
                DoReload(gameTime);
            else
                 if (AttackSpeedTimer < AttackSpeedInSeconds)
            {
                AttackSpeedTimer += gameTime.ElapsedGameTime.TotalSeconds;
                CanShoot = false;
            }
            else CanShoot = true;

            MuzzlePoint = new Vector2(Position.X + Size.Width / 2, Position.Y + Size.Height / 2);
            Position += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpotShadow, Position, Color.White);
            base.Draw(gameTime, spriteBatch);
        }

        protected virtual void DoReload(GameTime gameTime)
        {
            if (CurrentReloadTimeSeconds >= ReloadTimeInSeconds)
            {
                CurrentAmmo = MaxAmmo;
                CurrentReloadTimeSeconds = 0;
                AttackSpeedTimer = AttackSpeedInSeconds;
                CanShoot = true;
                return;
            }

            CanShoot = false;
            CurrentReloadTimeSeconds += gameTime.ElapsedGameTime.TotalSeconds;
        }

        protected void CheckHealth()
        {
            if (CurrentHealth > 0) return;

            ParticleManager.GenerateNewParticle(Color.White, Position, ContentManager.EntityDeathParticle, 5, 10);
            AudioManager.PlayEffect(ContentManager.EntityDeathSoundEffect, 0.25f);
            IsRemoved = true;
        }

        protected virtual void Shoot(GameTime gameTime, Vector2 direction, int bulletCount = -1, Texture2D texture = null, Size? size = null, SoundEffect soundEffect = null)
        {
            if (!CanShoot) return;

            CurrentAmmo -= 1;
            AttackSpeedTimer = 0;

            ParticleManager.GenerateNewParticle(Color.White, MuzzlePoint, ContentManager.ShootParticle, 3, 5);
            
            if(soundEffect is null)
                AudioManager.PlayEffect(ContentManager.ShootSoundEffect, 0.15f);
            else
                AudioManager.PlayEffect(soundEffect, 0.15f);

            for (int i = 0; i < (bulletCount <= 0 ? ProjectileCount : bulletCount); i++)
                CurrentState.AddComponent(new Projectile(direction, this, texture, size), 0);
        }

        public override void OnCollision(Sprite sprite, GameTime gameTime)
        {
            if (sprite == this) return;
            if (sprite is Item.Item) return;
            if (sprite is Projectile) return;
            if (this is Player && sprite is Enemy) return;

            if (IsTouchingLeft(sprite))
            {
                Position = new Vector2(Position.X - (float)(Speed * gameTime.ElapsedGameTime.TotalSeconds), Position.Y);
            }
            if (IsTouchingRight(sprite))
            {
                Position = new Vector2(Position.X + (float)(Speed * gameTime.ElapsedGameTime.TotalSeconds), Position.Y);
            }

            if (IsTouchingBottom(sprite))
            {
                Position = new Vector2(Position.X, Position.Y + (float)(Speed * gameTime.ElapsedGameTime.TotalSeconds));
            }
            if (IsTouchingTop(sprite))
            {
                Position = new Vector2(Position.X, Position.Y - (float)(Speed * gameTime.ElapsedGameTime.TotalSeconds));
            }

        }

        public virtual void TakeDamage(int damage)
        {
            CurrentState.AddComponent(new DamageNumber(damage, Position), 0);

            CurrentHealth -= damage;
        }

    }
}