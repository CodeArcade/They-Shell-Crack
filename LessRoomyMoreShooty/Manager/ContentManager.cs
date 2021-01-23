using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using Unity;

namespace LessRoomyMoreShooty.Manager
{
    public class ContentManager
    {
        [Dependency]
        public JamGame JamGame { get; set;  }

        public Texture2D PlayerTexture => JamGame.Content.Load<Texture2D>("Sprites/Player/Player");

        public Texture2D ButtonTexture => JamGame.Content.Load<Texture2D>("Sprites/Button/Button");
        public SoundEffect ButtonClickSoundEffect => JamGame.Content.Load<SoundEffect>("Sound/Button");
        public SpriteFont ButtonFont => JamGame.Content.Load<SpriteFont>("Fonts/ButtonFont");
        public SpriteFont KenneyMini => JamGame.Content.Load<SpriteFont>("Fonts/KenneyMini");

        //public Texture2D ButtonTexture => JamGame.Content.Load<Texture2D>("Sprites/Button");
        //public SoundEffect HurtSoundEffect => JamGame.Content.Load<SoundEffect>("SoundEffects/Hurt");
        //public Song MenuMusic => JamGame.Content.Load<Song>("Music/MenuMusic");

        //public List<Texture2D> HurtParticles
        //{
        //    get
        //    {
        //        List<Texture2D> hurtParticles = new List<Texture2D>
        //        {
        //            JamGame.Content.Load<Texture2D>("Particle/HurtParticle1"),
        //            JamGame.Content.Load<Texture2D>("Particle/HurtParticle1"),
        //            JamGame.Content.Load<Texture2D>("Particle/HurtParticle2"),
        //            JamGame.Content.Load<Texture2D>("Particle/HurtParticle2"),
        //            JamGame.Content.Load<Texture2D>("Particle/HurtParticle3"),
        //            JamGame.Content.Load<Texture2D>("Particle/HurtParticle3"),
        //            JamGame.Content.Load<Texture2D>("Particle/HurtParticle4")
        //        };
        //        return hurtParticles;
        //    }
        //}

    }
}