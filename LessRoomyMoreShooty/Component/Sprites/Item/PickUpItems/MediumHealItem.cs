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
            player.CurrentHealth += 4;
            if (player.CurrentHealth > player.MaxHealth) player.CurrentHealth = player.MaxHealth;
        }
    }
}
