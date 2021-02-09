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

        public List<Component.Component>[] Layers { get; set; }
        public List<Component.Component> Components { get; set; }

        [Dependency]
        public ContentManager ContentManager { get; set; }

        [Dependency]
        public StateManager StateManager { get; set; }

        [Dependency]
        public JamGame JamGame { get; set; }

        [Dependency]
        public AudioManager AudioManager { get; set; }

        public bool HasLoaded { get;  set; }

        #endregion

        #region Methods

        public void Load() { Layers = new List<Component.Component>[] { new List<Component.Component>(), new List<Component.Component>() } ; LoadComponents(); OnLoad(); HasLoaded = true; }

        protected virtual void LoadComponents() { }
        protected virtual void OnLoad() { }

        public void AddComponent(Component.Component component, int layer)
        {
            component.CurrentState = this;
            Layers[layer].Add(component);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Layers[0] is null && Layers[1] is null) return;
            // draw components from top to bottom
            List<Component.Component> DrawOrder = Layers[0].OrderByDescending(c => c.Position.Y).ToList();
            for (int i = DrawOrder.Count - 1; i >= 0; i--)
            {
                DrawOrder[i].Draw(gameTime, spriteBatch);
            }

            DrawOrder = Layers[1].OrderByDescending(c => c.Position.Y).ToList();
            for (int i = DrawOrder.Count - 1; i >= 0; i--)
            {
                DrawOrder[i].Draw(gameTime, spriteBatch);
            }
        }

        public virtual void PostUpdate(GameTime gameTime)
        {
            if (Layers[0] is null && Layers[1] is null) return;
            foreach(List<Component.Component> components in Layers)
            {
                for (int i = components.Count - 1; i >= 0; i--)
                {
                    if (components[i].IsRemoved) components.RemoveAt(i);
                }
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            AudioManager.Update();
            if (Layers[0] is null && Layers[1] is null) return;

            foreach(List<Component.Component> components in Layers)
            {
                for (int i = components.Count - 1; i >= 0; i--)
                {
                    components[i].Update(gameTime);
                }
            }

            CollisionCheck(gameTime);
        }

        private void CollisionCheck(GameTime gameTime)
        {
            IEnumerable<Sprite> sprites = Layers[0].Where(x => x is Sprite).Select(x => x as Sprite).ToList();
            foreach (Sprite sprite in sprites)
            {
                foreach (Sprite sprite2 in sprites)
                {
                    if (!sprite.Collide || !sprite2.Collide) continue;
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