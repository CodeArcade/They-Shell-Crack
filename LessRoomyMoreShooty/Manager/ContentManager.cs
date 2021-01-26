using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Unity;

namespace LessRoomyMoreShooty.Manager
{
    public class ContentManager
    {
        [Dependency]
        public JamGame JamGame { get; set; }

        public Texture2D BackgroundTexture => JamGame.Content.Load<Texture2D>("Background/Background");
        public Texture2D PlayerIdleAnimation => JamGame.Content.Load<Texture2D>("Sprites/Player/Idle");
        public Texture2D PlayerWalkAnimation => JamGame.Content.Load<Texture2D>("Sprites/Player/Walk");

        public Texture2D ButtonTexture => JamGame.Content.Load<Texture2D>("Sprites/Button/Button");
        public SoundEffect ButtonClickSoundEffect => JamGame.Content.Load<SoundEffect>("Sound/Button");
        public SpriteFont ButtonFont => JamGame.Content.Load<SpriteFont>("Fonts/ButtonFont");
        public SpriteFont KenneyMini => JamGame.Content.Load<SpriteFont>("Fonts/KenneyMini");

        public Texture2D BulletTexture => JamGame.Content.Load<Texture2D>("Sprites/General/Bullet");

        public Texture2D HeartTexture => JamGame.Content.Load<Texture2D>("Sprites/Ui/Heart");
        public Texture2D BulletsTexture => JamGame.Content.Load<Texture2D>("Sprites/Ui/Bullets");
        public Texture2D ArrowUpTexture => JamGame.Content.Load<Texture2D>("Sprites/Ui/Arrow");
        public Texture2D ClockTexture => JamGame.Content.Load<Texture2D>("Sprites/Ui/Clock");
        public Texture2D TransparentTexture => JamGame.Content.Load<Texture2D>("Sprites/Misc/Transparent");

        public SoundEffect ObstacleHitSoundEffect => JamGame.Content.Load<SoundEffect>("Sound/ObstacleHit");
        public SoundEffect EntityHitSoundEffect => JamGame.Content.Load<SoundEffect>("Sound/EntityHit");
        public SoundEffect EntityDeathSoundEffect => JamGame.Content.Load<SoundEffect>("Sound/EntityDeath");
        public SoundEffect ShootSoundEffect => JamGame.Content.Load<SoundEffect>("Sound/Shoot");
        public SoundEffect ReloadSoundEffect => JamGame.Content.Load<SoundEffect>("Sound/Reload");
        public SoundEffect LastBulletSoundEffect => JamGame.Content.Load<SoundEffect>("Sound/LastBullet");
        public SoundEffect ItemPickUpSoundEffect => JamGame.Content.Load<SoundEffect>("Sound/ItemPickUp");
        public SoundEffect ExplosionSoundEffect => JamGame.Content.Load<SoundEffect>("Sound/EntityDeath");

        public List<Texture2D> ObstacleHitParticle
        {
            get => new List<Texture2D>()
            {
                JamGame.Content.Load<Texture2D>("Sprites/Ui/Heart"),
                JamGame.Content.Load<Texture2D>("Sprites/Ui/Bullets"),
                JamGame.Content.Load<Texture2D>("Sprites/Ui/Arrow"),
                JamGame.Content.Load<Texture2D>("Sprites/Ui/Clock")
            };
        }

        public List<Texture2D> EntityHitParticle
        {
            get => new List<Texture2D>()
            {
                JamGame.Content.Load<Texture2D>("Sprites/Ui/Heart"),
                JamGame.Content.Load<Texture2D>("Sprites/Ui/Bullets"),
                JamGame.Content.Load<Texture2D>("Sprites/Ui/Arrow"),
                JamGame.Content.Load<Texture2D>("Sprites/Ui/Clock")
            };
        }

        public List<Texture2D> EntityDeathParticle
        {
            get => new List<Texture2D>()
            {
                JamGame.Content.Load<Texture2D>("Sprites/Ui/Heart"),
                JamGame.Content.Load<Texture2D>("Sprites/Ui/Bullets"),
                JamGame.Content.Load<Texture2D>("Sprites/Ui/Arrow"),
                JamGame.Content.Load<Texture2D>("Sprites/Ui/Clock")
            };
        }

        public List<Texture2D> ShootParticle
        {
            get => new List<Texture2D>()
            {
                JamGame.Content.Load<Texture2D>("Sprites/Ui/Heart"),
                JamGame.Content.Load<Texture2D>("Sprites/Ui/Bullets"),
                JamGame.Content.Load<Texture2D>("Sprites/Ui/Arrow"),
                JamGame.Content.Load<Texture2D>("Sprites/Ui/Clock")
            };
        }

        public List<Texture2D> ItemPickUpParticle
        {
            get => new List<Texture2D>()
            {
                JamGame.Content.Load<Texture2D>("Sprites/Ui/Heart"),
                JamGame.Content.Load<Texture2D>("Sprites/Ui/Bullets"),
                JamGame.Content.Load<Texture2D>("Sprites/Ui/Arrow"),
                JamGame.Content.Load<Texture2D>("Sprites/Ui/Clock")
            };
        }

        public Texture2D SmallHealthPotionTexture => JamGame.Content.Load<Texture2D>("Sprites/Items/SmallHealthPotion");
        public Texture2D MediumHealthPotionTexture => JamGame.Content.Load<Texture2D>("Sprites/Items/MediumHealthPotion");
        public Texture2D LargeHealthPotionTexture => JamGame.Content.Load<Texture2D>("Sprites/Items/LargeHealthPotion");
        public Texture2D HealthUpTexture => JamGame.Content.Load<Texture2D>("Sprites/Items/HealthUp");
        public Texture2D SpeedUpTexture => JamGame.Content.Load<Texture2D>("Sprites/Items/SpeedUp");
        public Texture2D AttackSpeedUpTexture => JamGame.Content.Load<Texture2D>("Sprites/Items/AttackSpeedUp");
        public Texture2D AmmoUpTexture => JamGame.Content.Load<Texture2D>("Sprites/Items/AmmoUp");
        public Texture2D DamageUpTexture => JamGame.Content.Load<Texture2D>("Sprites/Items/DamageUp");
    }
}