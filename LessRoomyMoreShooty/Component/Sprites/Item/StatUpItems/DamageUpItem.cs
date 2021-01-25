using System.Drawing;

namespace LessRoomyMoreShooty.Component.Sprites.Item
{
    public class DamageUpItem : Item
    {
        public DamageUpItem() : base("Damage Up", "Damage Up | Range Down")
        {
            Texture = ContentManager.DamageUpTexture;
            Size = new Size(50, 50);
        }

        public override void OnPickup(Player player)
        {
            player.Damage += 1;
            player.RangeInSeconds -= 0.25f;

            if (player.RangeInSeconds <= 0.25f) player.RangeInSeconds = 0.25f;
        }
    }
}
