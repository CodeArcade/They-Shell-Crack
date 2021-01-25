using System.Drawing;

namespace LessRoomyMoreShooty.Component.Sprites.Item
{
    public class MovementSpeedUpItem : Item
    {
        public MovementSpeedUpItem() : base("Movement Speed Up", "Gotta go fast!")
        {
            Texture = ContentManager.SpeedUpTexture;
            Size = new Size(50, 50);
        }

        public override void OnPickup(Player player)
        {
            player.MaxSpeed += 50;
        }
    }
}
