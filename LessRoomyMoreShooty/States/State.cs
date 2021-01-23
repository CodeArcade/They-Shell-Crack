using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Unity;
using System.Collections.Generic;
using LessRoomyMoreShooty.Manager;
using System.Linq;
using LessRoomyMoreShooty.Component.Sprites;

namespace LessRoomyMoreShooty.States
{
    public class State
    {
        #region Fields

        protected List<Component.Component> Components { get; set; }

        [Dependency]
        public ContentManager ContentManager { get; set; }

        [Dependency]
        public StateManager StateManager { get; set; }

        [Dependency]
        public JamGame JamGame { get; set; }

        [Dependency]
        public AudioManager AudioManager { get; set; }

        public bool HasLoaded { get; protected set; }

        #endregion

        #region Methods

        public void Load() { Components = new List<Component.Component>(); LoadComponents(); OnLoad(); HasLoaded = true; }

        protected virtual void LoadComponents() { }
        protected virtual void OnLoad() {  }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Components is null) return;
            foreach (Component.Component component in Components)
            {
                component.Draw(gameTime, spriteBatch);
            }
        }

        public virtual void PostUpdate(GameTime gameTime)
        {
            if (Components is null) return;
            for (int i = Components.Count - 1; i >= 0; i--)
            {
                if (Components[i].IsRemoved) Components.RemoveAt(i);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            AudioManager.Update();
            if (Components is null) return;
            foreach (Component.Component component in Components)
            {
                component.Update(gameTime);
            }

            CollisionCheck(gameTime);
        }

        private void CollisionCheck(GameTime gameTime)
        {
            IEnumerable<Sprite> sprites = Components.Where(x => x is Sprite).Select(x => x as Sprite);
            foreach (Sprite sprite in sprites)
            {
                foreach (Sprite sprite2 in sprites)
                {
                    if (sprite == sprite2) continue;
                    if (sprite.Rectangle.Intersects(sprite2.Rectangle))
                    {
                        sprite.OnCollision(sprite2, gameTime);
                        sprite2.OnCollision(sprite, gameTime);
                    }
                }
            }
        }

        #endregion
    }
}