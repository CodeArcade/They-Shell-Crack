using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LessRoomyMoreShooty.Component
{
    public class Particle : Component
    {
        public Vector2 Velocity { get; set; }
        public float Angle { get; set; }
        public float AngularVelocity { get; set; }
        public float Size { get; set; }
        public int TTL { get; set; }
        public Color Color { get; set; }
        public Texture2D Texture { get; set; }

        public Particle(Vector2 velocity, Vector2 position, float angle, float angularVelocity, float size, int tTL, Color color, Texture2D texture)
        {
            Velocity = velocity;
            Position = position;
            Angle = angle;
            AngularVelocity = angularVelocity;
            Size = size;
            TTL = tTL;
            Color = color;
            Texture = texture;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            spriteBatch.Draw(Texture, Position, sourceRectangle, Color,
                Angle, origin, Size, SpriteEffects.None, 0f);
        }

        public override void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }
    }
}
