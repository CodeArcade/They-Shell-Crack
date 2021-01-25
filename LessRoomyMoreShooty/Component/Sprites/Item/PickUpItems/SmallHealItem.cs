using System.Drawing;

namespace LessRoomyMoreShooty.Component.Sprites.Item.PickUpItems
{
    public class SmallHealItem : Item
    {
        public SmallHealItem() : base("Small Heal", "Better than nothing.")
        {
            Texture = ContentManager.SmallHealthPotionTexture;
            Size = new Size(50, 50);
        }

        public override void OnPickup(Player player)
        {
            if (player.CurrentHealth < player.MaxHealth - 1)
                player.CurrentHealth += 1;
        }
    }
}
