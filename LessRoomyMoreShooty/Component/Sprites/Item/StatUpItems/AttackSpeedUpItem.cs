using System.Drawing;

namespace LessRoomyMoreShooty.Component.Sprites.Item
{
    public class AttackSpeedUpItem : Item
    {
        public AttackSpeedUpItem() : base("Attack Speed Up", "Attack Speed Up | Increased Spray")
        {
            Texture = ContentManager.AttackSpeedUpTexture;
            Size = new Size(50, 50);
        }

        public override void OnPickup(Player player)
        {
            player.AttackSpeedInSeconds -= 0.05;
            player.Spread += 5;

            if (player.AttackSpeedInSeconds <= 0.05f) player.AttackSpeedInSeconds = 0.05f;
        }
    }
}
