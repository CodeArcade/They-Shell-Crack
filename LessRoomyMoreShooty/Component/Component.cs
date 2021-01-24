using LessRoomyMoreShooty.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Unity;

namespace LessRoomyMoreShooty.Component
{
    public abstract class Component
    {

        #region Dependencies

        public AnimationManager AnimationManager { get; set; } = Program.UnityContainer.Resolve<AnimationManager>();
        public ContentManager ContentManager => Program.UnityContainer.Resolve<ContentManager>();
        public AudioManager AudioManager => Program.UnityContainer.Resolve<AudioManager>();
        public ParticleManager ParticleManager { get; } = Program.UnityContainer.Resolve<ParticleManager>();

        #endregion

        #region Private Properties
        private Vector2 InternalPosition { get; set; }
        #endregion

        #region Public Properties
        
        public States.State CurrentState { get; set; }

        public Vector2 Position
        {
            get => InternalPosition;
            set
            {
                InternalPosition = value;
            }
        }

        public bool IsRemoved { get; set; }
        public Component Parent { get; set; }

        #endregion

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);
    }
}
