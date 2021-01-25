using System.Drawing;

namespace LessRoomyMoreShooty.Component.Sprites.Item
{
    public class HealthUpItem : Item
    {
        public HealthUpItem() : base("Health Up", "An apple a day...")
        {
            Texture = ContentManager.HealthUpTexture;
            Size = new Size(50, 50);
        }

        public override void OnPickup(Player player)
        {
            player.MaxHealth += 1;
            player.CurrentHealth += 1;
        }
    }
}
