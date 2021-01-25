using System.Drawing;

namespace LessRoomyMoreShooty.Component.Sprites.Item.PickUpItems
{
    public class MediumHealItem : Item
    {
        public MediumHealItem() : base("Medium Heal", "Healthy.")
        {
            Texture = ContentManager.MediumHealthPotionTexture;
            Size = new Size(50, 50);
        }

        public override void OnPickup(Player player)
        {
            if (player.CurrentHealth < player.MaxHealth - 2)
                player.CurrentHealth += 2;
        }
    }
}
