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
        public Texture2D TitleTexture => JamGame.Content.Load<Texture2D>("Background/Title");
        public Texture2D GameOverTexture => JamGame.Content.Load<Texture2D>("Background/GameOver");
        public Texture2D GameOverAgainTexture => JamGame.Content.Load<Texture2D>("Background/GameOverAgain");
        public Texture2D MenuBackgroundTexture => JamGame.Content.Load<Texture2D>("Background/MenuBackground");
        public Texture2D DoorsOpendTexture => JamGame.Content.Load<Texture2D>("Background/DoorsOpend");
        public Texture2D DoorsClosedTexture => JamGame.Content.Load<Texture2D>("Background/DoorsClosed");
        public Texture2D PlayerIdleAnimation => JamGame.Content.Load<Texture2D>("Sprites/Player/Idle");
        public Texture2D PlayerWalkAnimation => JamGame.Content.Load<Texture2D>("Sprites/Player/Walk");

        public Texture2D ZombieIdleAnimation => JamGame.Content.Load<Texture2D>("Sprites/Enemy/Zombie/Idle");
        public Texture2D ZombieWalkAnimation => JamGame.Content.Load<Texture2D>("Sprites/Enemy/Zombie/Walk");

        public Texture2D CreeperIdleAnimation => JamGame.Content.Load<Texture2D>("Sprites/Enemy/Creeper/Idle");
        public Texture2D CreeperWalkAnimation => JamGame.Content.Load<Texture2D>("Sprites/Enemy/Creeper/Walk");

        public Texture2D SkeletonWalkAnimation => JamGame.Content.Load<Texture2D>("Sprites/Enemy/Skeleton/Walk");
        public Texture2D SkeletonIdleAnimation => JamGame.Content.Load<Texture2D>("Sprites/Enemy/Skeleton/Idle");
        public Texture2D BowTexture => JamGame.Content.Load<Texture2D>("Sprites/Enemy/Skeleton/Bow");
        public Texture2D SkeletonBulletTexture=> JamGame.Content.Load<Texture2D>("Sprites/Enemy/Skeleton/SkeletonBullet");

        public Texture2D BlazeIdleAnimation => JamGame.Content.Load<Texture2D>("Sprites/Enemy/Blaze/Idle");
        public Texture2D BlazeBulletTexture => JamGame.Content.Load<Texture2D>("Sprites/Enemy/Blaze/BlazeBullet");
        public SoundEffect BlazeShootSoundEffect => JamGame.Content.Load<SoundEffect>("Sprites/Enemy/Blaze/BlazeShoot");

        public Texture2D ButtonTexture => JamGame.Content.Load<Texture2D>("Sprites/Button/Button");
        public SoundEffect ButtonClickSoundEffect => JamGame.Content.Load<SoundEffect>("Sound/Button");
        public SpriteFont ButtonFont => JamGame.Content.Load<SpriteFont>("Fonts/ButtonFont");
        public SpriteFont KenneyMini => JamGame.Content.Load<SpriteFont>("Fonts/KenneyMini");

        public Texture2D PlayerBulletTexture => JamGame.Content.Load<Texture2D>("Sprites/Player/PlayerBullet");
        public Texture2D ArrowTexture => JamGame.Content.Load<Texture2D>("Sprites/General/Arrow");
        public Texture2D FloorExplosionTexture => JamGame.Content.Load<Texture2D>("Sprites/General/BoomFloor");
        public Texture2D GunTexture => JamGame.Content.Load<Texture2D>("Sprites/General/Gun");

        public Texture2D SpotShadowTexture => JamGame.Content.Load<Texture2D>("Sprites/General/Shadow");

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
        public SoundEffect ExplosionSoundEffect => JamGame.Content.Load<SoundEffect>("Sound/Explosion");
        public SoundEffect DoorOpenSoundEffect => JamGame.Content.Load<SoundEffect>("Sound/DoorOpen");

        public List<Texture2D> ObstacleHitParticle
        {
            get => new List<Texture2D>()
            {
                JamGame.Content.Load<Texture2D>("Sprites/Particle/ObstacleHitParticle1"),
                JamGame.Content.Load<Texture2D>("Sprites/Particle/ObstacleHitParticle2"),
                JamGame.Content.Load<Texture2D>("Sprites/Particle/ObstacleHitParticle3")
            };
        }

        public List<Texture2D> EntityHitParticle
        {
            get => new List<Texture2D>()
            {
                JamGame.Content.Load<Texture2D>("Sprites/Particle/EntityHitParticle1"),
                JamGame.Content.Load<Texture2D>("Sprites/Particle/EntityHitParticle2"),
                JamGame.Content.Load<Texture2D>("Sprites/Particle/EntityHitParticle3")
            };
        }

        public List<Texture2D> EntityDeathParticle
        {
            get => new List<Texture2D>()
            {
               JamGame.Content.Load<Texture2D>("Sprites/Particle/ObstacleHitParticle1"),
               JamGame.Content.Load<Texture2D>("Sprites/Particle/ObstacleHitParticle2"),
               JamGame.Content.Load<Texture2D>("Sprites/Particle/ObstacleHitParticle3"),
               JamGame.Content.Load<Texture2D>("Sprites/Particle/EntityHitParticle1"),
               JamGame.Content.Load<Texture2D>("Sprites/Particle/EntityHitParticle2"),
               JamGame.Content.Load<Texture2D>("Sprites/Particle/EntityHitParticle3")
            };
        }

        public List<Texture2D> ShootParticle
        {
            get => new List<Texture2D>()
            {
                JamGame.Content.Load<Texture2D>("Sprites/Particle/ShootParticle1"),
                JamGame.Content.Load<Texture2D>("Sprites/Particle/ShootParticle2"),
                JamGame.Content.Load<Texture2D>("Sprites/Particle/ShootParticle3")
            };
        }

        public List<Texture2D> ItemPickUpParticle
        {
            get => new List<Texture2D>()
            {
                JamGame.Content.Load<Texture2D>("Sprites/Particle/ShootParticle1"),
                JamGame.Content.Load<Texture2D>("Sprites/Particle/ShootParticle2"),
                JamGame.Content.Load<Texture2D>("Sprites/Particle/ShootParticle3"),
                JamGame.Content.Load<Texture2D>("Sprites/Particle/EntityHitParticle1"),
                JamGame.Content.Load<Texture2D>("Sprites/Particle/EntityHitParticle2"),
                JamGame.Content.Load<Texture2D>("Sprites/Particle/EntityHitParticle3")
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