using Microsoft.Xna.Framework;

namespace LessRoomyMoreShooty.Component.Sprites
{

    public class Projectile : Sprite
    {

        public Projectile(Vector2 direction, Entity parent)
        {
            Parent = parent;
            Position = parent.MuzzlePoint;
            Speed = 200;
            Direction = direction;
            Texture = ContentManager.ButtonTexture;
            Size = new System.Drawing.Size(10, 10);
        }

        public override void Update(GameTime gameTime)
        {
            Position += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }

        public override void OnCollision(Sprite sprite, GameTime gameTime)
        {
            if (sprite == Parent) return;
            if (sprite == this) return;
            if (!(sprite is Entity)) return;

            ((Entity)sprite).CurrentHealth -= ((Entity)Parent).Damage;
            IsRemoved = true;
        }

    }
}