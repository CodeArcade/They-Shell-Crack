using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace LessRoomyMoreShooty.Component.Sprites
{
    public class Entity : Sprite
    {
        public List<Component> Components { get; set; }

        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int Damage { get; set; }
        public int AttackSpeed { get; set; }
        public int MaxAmmo { get; set; }
        public int CurrentAmmo { get; set; }
        public int ReloadSpeed { get; set; }
        public int Range { get; set; }
        public int Spread { get; set; }

        public float Acceleration { get; set; }

        public Vector2 MuzzlePoint { get; set; }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Components != null)
                foreach (Component component in Components)
                    component.Draw(gameTime, spriteBatch);

            base.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (Components != null)
                foreach (Component component in Components)
                    component.Update(gameTime);

            Position += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }

    }
}