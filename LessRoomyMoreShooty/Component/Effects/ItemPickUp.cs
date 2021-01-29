using LessRoomyMoreShooty.Component.Controls;
using LessRoomyMoreShooty.Component.Sprites.Item;
using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace LessRoomyMoreShooty.Component.Effects
{
    public  class ItemPickUp: Label
    {
        private double TTL { get; set; }
        private double TimeLived { get; set; }

        public ItemPickUp(Item item, Vector2 position) : base(null)
        {
            Font = ContentManager.KenneyMini;
            Text = item.Name; 
            FontColor = Color.White;
            TTL = 1.5;
            Position = position;
            FontScale = 2;
        }

        public override void Update(GameTime gameTime)
        {
            TimeLived += gameTime.ElapsedGameTime.TotalSeconds;

            if (TimeLived >= TTL)
            {
                IsRemoved = true;
                return;
            }

            base.Update(gameTime);
        }

    }
}
